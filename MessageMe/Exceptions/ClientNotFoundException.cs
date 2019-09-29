using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageMe.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string message)
            : base(message)
        {
        }

        public ClientNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ClientNotFoundException()
        {
        }
    }
}
