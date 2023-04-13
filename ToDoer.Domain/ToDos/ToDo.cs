using ToDoer.Domain.Subtasks;
using ToDoer.Domain.Users;

namespace ToDoer.Domain.ToDos
{
    public enum Status
    {
        Active,
        Done,
        Deleted
    }

    public enum StatusChecking
    {
        Active,
        Done,
    }
    public class ToDo 
    {
        public ToDo()
        {
            CreatedAt = DateTime.Now;
            Status = Status.Active;
            ModifiedAt = DateTime.Now;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<Subtask> Subtasks { get; set; }
        public User User { get; set; }
    }
}