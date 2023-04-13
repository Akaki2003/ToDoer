using Microsoft.AspNetCore.JsonPatch;
using ToDoer.Domain.ToDos;

namespace ToDoer.Application.ToDos.Repositories
{
    //public interface IToDoRepository : IBaseRepository<ToDo>
    public interface IToDoRepository 
    {
        Task<List<ToDo>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<ToDo>> GetAllFullAsync(CancellationToken cancellationToken, int userId, StatusChecking? status);
        Task<int> CreateAsync(CancellationToken cancellationToken, ToDo toDo, int userId);
        Task<int> DeleteByStatus(CancellationToken cancellationToken, int userId);
        Task<ToDo> GetToDoByIdAsync(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id, int userId);
        Task UpdateToDoAsync(CancellationToken cancellationToken, ToDo toDo);
        Task UpdateToDoPatchAsync(CancellationToken cancellationToken,int id, JsonPatchDocument toDo);
        Task<DateTime> GetCreatedDate(CancellationToken cancellationToken, int Id);



    }
}
