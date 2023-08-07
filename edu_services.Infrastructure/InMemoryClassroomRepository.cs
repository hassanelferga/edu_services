using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_services.Infrastructure
{
    public class InMemoryClassroomRepository<T, S> : IClassroomRepository<T, S>
    {
        private T _teacher;
        private List<S> _students;

        public InMemoryClassroomRepository()
        {
            _students = new List<S>();
        }

        public void AddTeacher(T teacher)
        {
            _teacher = teacher;
        }

        public void AddStudent(S student)
        {
            _students.Add(student);
        }
        public bool ContainsStudent(S student)
        {
            return _students.Contains(student);
        }

        public (T, List<S>) GetRoster()
        {
            return (_teacher, _students);
        }
    }
}
