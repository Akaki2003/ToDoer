using ToDoer.Domain.ToDos;

namespace ToDoer.Domain.Users
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public List<ToDo> ToDos { get; set; }
    }
}