using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoer.Application.Exceptions
{
    public class SubtaskNotFoundException : Exception
    {

        public SubtaskNotFoundException() : base() { }
        public SubtaskNotFoundException(string message) : base(message) { }
        public SubtaskNotFoundException(string message, Exception e) : base(message, e) { }
    }
}
