using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoer.Application.Exceptions
{

    public class ToDoNotFoundException : Exception
    {

        public ToDoNotFoundException() : base() { }
        public ToDoNotFoundException(string message) : base(message) { }
        public ToDoNotFoundException(string message, Exception e) : base(message, e) { }
    }
}
