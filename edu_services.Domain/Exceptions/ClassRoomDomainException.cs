using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_services.Domain.Exceptions
{
    public class ClassRoomDomainException : Exception
    {
        public ClassRoomDomainException()
        {
        }

        public ClassRoomDomainException(string message)
            : base(message)
        {
        }

        public ClassRoomDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
