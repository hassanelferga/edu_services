using System.Collections.Generic;

namespace edu_services.WebAPI.Models
{
    public class RosterModel
    {
        public string TeacherName { get; set; }
        public List<string> StudentNames { get; set; }
    }
}
