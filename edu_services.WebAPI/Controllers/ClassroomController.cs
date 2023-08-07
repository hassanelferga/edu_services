using AutoMapper;
using edu_services.Domain.Entities;
using edu_services.Domain.Exceptions;
using edu_services.Services;
using edu_services.WebAPI.Mapping;
using edu_services.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace edu_services.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService<Teacher, Student> _classroomService;
        private readonly ILogger<ClassroomController> _logger;
        private readonly IMapper _mapper;

        public ClassroomController(IClassroomService<Teacher, Student> classroomService, ILogger<ClassroomController> logger, IMapper mapper)
        {
            _classroomService = classroomService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("roster")]
        public IActionResult GetRoster()
        {
            try
            {
                var (teacher, students) = _classroomService.GetRoster();
                // Map roster to ResponseModel
                RosterModel model = RosterMapper.ToModel(teacher, students);

                return Ok(model);
            }
            catch (ClassRoomDomainException ex)
            {
                // Return the exception message for Domain Excptions, otherwise hide technical excpetion details
                var errorResponse = new ResponseModel { HasError = true, Message = ex.Message };
                return StatusCode(500, errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting classroom roster.");
                var errorResponse = new ResponseModel { HasError = true, Message = "An error occurred while adding the student." };
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPost("assign-teacher")]
        public IActionResult AssignTeacher(TeacherModel teacherModel)
        {
            try
            {
                var teacher = _mapper.Map<Teacher>(teacherModel);
                _classroomService.AddTeacher(teacher);                
                return Ok(new ResponseModel { HasError = false, Message= "Teacher assigned successfully." });
            }
            catch (ClassRoomDomainException ex)
            {
                var errorResponse = new ResponseModel { HasError = true, Message = ex.Message };
                return StatusCode(500, errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning teacher.");
                return StatusCode(500, "An error occurred while adding the student.");
            }
        }

        [HttpPost("add-student")]
        public IActionResult AddStudent(List<StudentModel> students)
        {
            try
            {                
                foreach (var studentModel in students)
                {
                    var student = _mapper.Map<Student>(studentModel);
                    _classroomService.AddStudent(student);
                }
                return Ok(new ResponseModel { HasError = false, Message = "Students added successfully." });
            }
            catch (ClassRoomDomainException ex)
            {
                var errorResponse = new ResponseModel { HasError = true, Message = ex.Message };
                return StatusCode(500, errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student.");
                var errorResponse = new ResponseModel { HasError = true, Message = "An error occurred while adding the student." };
                return StatusCode(500, errorResponse);
            }
        }
    }
}
