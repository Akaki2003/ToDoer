using ToDoer.Domain.ToDos;
using ToDoer.Domain.Users;

namespace ToDoer.Domain.Subtasks
{
    public class Subtask : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TodoItemId { get; set; }


        public ToDo ToDo { get; set; }
    }
}