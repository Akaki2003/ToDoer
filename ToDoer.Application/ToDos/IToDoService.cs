using Microsoft.AspNetCore.JsonPatch;
using ToDoer.Application.ToDos.Requests;
using ToDoer.Application.ToDos.Responses;
using ToDoer.Domain.ToDos;

namespace ToDoer.Application.ToDos
{
    public interface IToDoService
    {
        Task<List<ToDoResponseModel>> GetAllAsync(CancellationToken cancellationToken, int userId,StatusChecking? status);
        Task<ToDoResponseModel> GetByIdAsync(CancellationToken cancellationToken, int toDoId,int userId);
        Task CreateAsync(CancellationToken cancellationToken, ToDoCreateModel toDo,int userId);
        Task UpdateToDoAsync(CancellationToken cancellationToken, ToDoRequestPutModel toDo, int userId);
        Task UpdateToDoPatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument toDo,int userId);
        Task<int> DeleteAsync(CancellationToken cancellationToken, int toDoId,int userId);


    }
}
