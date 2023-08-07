using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_services.Services
{
    public interface IClassroomService<T, S>
    {
        void AddTeacher(T teacher);
        void AddStudent(S student);
        (T, List<S>) GetRoster();
    }
}
