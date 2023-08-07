using edu_services.Domain.Entities;
using edu_services.Domain.Exceptions;
using edu_services.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_services.Services
{
    public class ClassroomService<T, S> : IClassroomService<T, S>
    {
        private readonly ILogger<ClassroomService<T, S>> _logger;
        private readonly IClassroomRepository<T, S> _repository;

        public ClassroomService(IClassroomRepository<T, S> repository, ILogger<ClassroomService<T, S>> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void AddTeacher(T teacher)
        {
            _repository.AddTeacher(teacher);
            _logger.LogInformation($"Teacher {teacher} added to the classroom.");
        }

        public void AddStudent(S student)
        {
            if (_repository.ContainsStudent(student))
            {
                _logger.LogWarning($"Student {student.ToString()} already exists in the classroom.");
                throw new ClassRoomDomainException($"Student {student.ToString()} already exists in the classroom.");
            }
            _repository.AddStudent(student);
            _logger.LogInformation($"Student {student} added to the classroom.");
        }

        public (T, List<S>) GetRoster()
        {
            var (teacher, students) = _repository.GetRoster();
            if (teacher == null)
            {
                _logger.LogWarning("Classroom contains no teacher.");
                throw new ClassRoomDomainException("Classroom contains no teacher.");
            }

            if (students == null || students.Count < 3)
            {
                _logger.LogWarning("Classroom does not contain 3 students.");
                throw new ClassRoomDomainException("Classroom does not contain 3 students.");
            }

            _logger.LogInformation("Returning the roster.");
            return (teacher, students);
        }
    }
}
