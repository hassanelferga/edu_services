using edu_services.Domain.Entities;
using edu_services.Domain.Exceptions;
using edu_services.Infrastructure;
using edu_services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace edu_services.Tests.Services
{
    public class ClassroomServiceTests
    {
        private readonly Mock<IClassroomRepository<Teacher, Student>> _mockRepository;
        private readonly Mock<ILogger<ClassroomService<Teacher, Student>>> _mockLogger;
        private readonly ClassroomService<Teacher, Student> _classroomService;

        public ClassroomServiceTests()
        {
            _mockRepository = new Mock<IClassroomRepository<Teacher, Student>>();
            _mockLogger = new Mock<ILogger<ClassroomService<Teacher, Student>>>();

            _classroomService = new ClassroomService<Teacher, Student>(
                _mockRepository.Object,
                _mockLogger.Object
            );
        }


        [Fact]
        public void AddStudent_StudentExists_Throws()
        {
            // Arrange
            var existingStudent = new Student("Alice", "Johnson");
            _mockRepository.Setup(repository => repository.ContainsStudent(existingStudent))
                .Throws<ClassRoomDomainException>();
            // Act & Assert
            Assert.Throws<ClassRoomDomainException>(() => _classroomService.AddStudent(existingStudent));
        }


        [Fact]
        public void GetRoster_ClassroomHasTeacherAndStudents_ReturnsRoster()
        {
            // Arrange
            var teacher = new Teacher("John", "Doe");
            var students = new List<Student>
            {
                new Student("Alice", "Johnson"),
                new Student("Bob", "Smith"),
                new Student("Eve", "Williams")
            };
            _mockRepository.Setup(repository => repository.GetRoster())
                .Returns((teacher, students));

            // Act
            var (actualTeacher, actualStudents) = _classroomService.GetRoster();

            // Assert
            Assert.Equal(teacher, actualTeacher);
            Assert.Equal(students, actualStudents);
        }

        [Fact]
        public void GetRoster_ValidRoster_ReturnsRoster()
        {
            // Arrange
            var teacher = new Teacher("John", "Doe");
            var students = new List<Student>
            {
                new Student("Alice", "Johnson"),
                new Student("Bob", "Smith"),
                new Student("Eve", "Williams")
            };
            _mockRepository.Setup(repository => repository.GetRoster())
                .Returns((teacher, students));

            // Act
            var (actualTeacher, actualStudents) = _classroomService.GetRoster();

            // Assert
            Assert.Equal(teacher, actualTeacher);
            Assert.Equal(students, actualStudents);
        }


    }
}
