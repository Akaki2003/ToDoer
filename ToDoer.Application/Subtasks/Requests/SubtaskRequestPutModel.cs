using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoer.Application.Subtasks.Requests
{
    public class SubtaskRequestPutModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TodoItemId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime ModifiedAt { get; set; }
    }
}
