namespace ToDoer.Domain
{
    public enum Status
    {
        Active,
        Deleted
    }
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            Status = Status.Active;
            ModifiedAt = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Status Status { get; set; }
    }
}