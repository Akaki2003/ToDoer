using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoer.Application.PatchModel
{
    public class RequestPatchModel
    {
        public string path { get; set; }
        public string op { get; set; }
        public string value { get; set; }
    }
}
