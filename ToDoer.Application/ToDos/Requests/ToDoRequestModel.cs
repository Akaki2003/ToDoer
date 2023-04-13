using ToDoer.Application.Subtasks.Requests;

namespace ToDoer.Application.ToDos.Requests
{
    public enum Status
    {
        Active,
        Done,
        Deleted
    }
    public class ToDoRequestModel
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        //public Status Status { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public List<SubtaskRequestModel> Subtasks {get;set;}
        //public int UserId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime ModifiedAt { get; set; }
    }
}