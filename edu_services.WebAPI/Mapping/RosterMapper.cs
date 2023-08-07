
using edu_services.Domain.Entities;
using edu_services.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace edu_services.WebAPI.Mapping
{
    public class RosterMapper
    {
        public static RosterModel ToModel(Teacher teacher, List<Student> students)
        {
            return new RosterModel
            {
                TeacherName = teacher.ToString(),
                StudentNames = students.Select(student => student.ToString()).ToList()
            };
        }
    }
}
