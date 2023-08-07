using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_services.Infrastructure
{
    public interface IClassroomRepository<T, S>
    {
        void AddTeacher(T teacher);
        void AddStudent(S student);
        bool ContainsStudent(S student);
        (T, List<S>) GetRoster();
    }
}
