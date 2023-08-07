using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_services.Domain.Entities
{
    public record Student(string FirstName, string LastName)
    {
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
