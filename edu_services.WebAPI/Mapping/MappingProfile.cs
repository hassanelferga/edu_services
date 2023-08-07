using AutoMapper;
using edu_services.Domain.Entities;
using edu_services.WebAPI.Models;

namespace edu_services.WebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeacherModel, Teacher>();
            CreateMap<StudentModel, Student>();
        }
    }
}
