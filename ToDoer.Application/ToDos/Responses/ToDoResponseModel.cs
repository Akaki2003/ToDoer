using ToDoer.Application.Subtasks.Requests;
using ToDoer.Application.Subtasks.Responses;

namespace ToDoer.Application.ToDos.Responses
{
    public enum Status
    {
        Active,
        Done,
        Deleted
    }
    public class ToDoResponseModel 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public List<SubtaskResponseModel> Subtasks { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}