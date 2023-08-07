using AutoMapper;
using edu_services.Domain.Entities;
using edu_services.Domain.Exceptions;
using edu_services.Services;
using edu_services.WebAPI.Controllers;
using edu_services.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace edu_services.Tests.Controllers
{
    public class ClassroomControllerTests
    {
        private readonly Mock<IClassroomService<Teacher, Student>> _mockClassroomService;
        private readonly Mock<ILogger<ClassroomController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        private readonly ClassroomController _controller;

        public ClassroomControllerTests()
        {
            _mockClassroomService = new Mock<IClassroomService<Teacher, Student>>();
            _mockLogger = new Mock<ILogger<ClassroomController>>();
            _mockMapper = new Mock<IMapper>();

            _controller = new ClassroomController(
                _mockClassroomService.Object,
                _mockLogger.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public void GetRoster_ShouldReturnRosterModel()
        {
            var teacher = new Teacher ( "John","Doe" );
            var students = new List<Student>
            {
                new Student ("Alice", "Smith"),
                new Student ("Bob", "Johnson"),
                new Student ("John", "Wick")
            };

            _mockClassroomService.Setup(c => c.GetRoster()).Returns((teacher, students));
            var result = _controller.GetRoster();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<RosterModel>(okResult.Value);

            Assert.Equal(teacher.ToString(), model.TeacherName);
            Assert.Equal(students.Count, model.StudentNames.Count);
        }

        [Fact]
        public void GetRoster_ShouldReturnInternalServerErrorOnClassRoomDomainException()
        {
            _mockClassroomService.Setup(c => c.GetRoster()).Throws(new ClassRoomDomainException("Classroom contains no teacher."));
            var result = _controller.GetRoster();
            var errorResponseResults = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResponseResults.StatusCode);
        }

        [Fact]
        public void AssignTeacher_ShouldReturnOkResultOnSuccess()
        {
            var teacher = new TeacherModel { FirstName = "John", LastName = "Doe" };
            var result = _controller.AssignTeacher(teacher);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void AddStudent_ShouldReturnOkResultOnSuccess()
        {
            var student = new StudentModel { FirstName = "Alice", LastName = "Smith" };
            List<StudentModel> studentList = new List<StudentModel> { student };
            var result = _controller.AddStudent(studentList);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
