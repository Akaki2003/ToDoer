using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoer.Application.Subtasks.Requests;

namespace ToDoer.Application.ToDos.Requests
{
    public class ToDoCreateModel
    {
        public string Title { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public List<SubtaskCreateModel> Subtasks { get; set; }
    }
}
