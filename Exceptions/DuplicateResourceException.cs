using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketHub.Exceptions
{
    public class DuplicateResourceException : Exception
    {
        
        public DuplicateResourceException(string message) : base(message)
        {
        }
    }
}