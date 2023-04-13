using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoer.Application.ToDos.Requests
{
    public class ToDoRequestPutModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        //public List<Subtasks>
        //public int UserId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime ModifiedAt { get; set; }
    }
}
