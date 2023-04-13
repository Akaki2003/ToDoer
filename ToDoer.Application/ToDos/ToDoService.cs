using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using ToDoer.Application.Exceptions;
using ToDoer.Application.Subtasks.Repositories;
using ToDoer.Application.ToDos.Repositories;
using ToDoer.Application.ToDos.Requests;
using ToDoer.Application.ToDos.Responses;
using ToDoer.Domain.ToDos;

namespace ToDoer.Application.ToDos
{
    public class ToDoService :IToDoService
    {
        private readonly IToDoRepository _repository;
        private readonly ISubtaskRepository _subtaskRepository;

        public ToDoService(IToDoRepository repository, ISubtaskRepository subtaskRepository)
        {
            _repository = repository;
            _subtaskRepository = subtaskRepository; 
        }
        public async Task<List<ToDoResponseModel>> GetAllAsync(CancellationToken cancellationToken,int userId, StatusChecking? status)
        {
            var result = await _repository.GetAllFullAsync(cancellationToken,userId,status);

            return result.Adapt<List<ToDoResponseModel>>().Where(x=>x.Status!=ToDoer.Application.ToDos.Responses.Status.Deleted).ToList();
        }
     


        public async Task CreateAsync(CancellationToken cancellationToken, ToDoCreateModel toDo, int userId)
        {
            var toDoToInsert = toDo.Adapt<ToDo>();

            await _repository.CreateAsync(cancellationToken, toDoToInsert,userId);
        }

        public async Task<ToDoResponseModel> GetByIdAsync(CancellationToken cancellationToken, int toDoId,int userId)
        {
            if (!await _repository.Exists(cancellationToken, toDoId,userId))
                throw new ToDoNotFoundException("Not Found");
            var toDo = await _repository.GetToDoByIdAsync(cancellationToken, toDoId);
            if (toDo.UserId != userId)
               throw new ToDoNotFoundException($"There is no toDo with id of {toDoId} connected to logged user");
            return toDo.Adapt<ToDoResponseModel>();
        }

        public async Task UpdateToDoAsync(CancellationToken cancellationToken, ToDoRequestPutModel toDo,int userId)
        {
            if (!await _repository.Exists(cancellationToken, toDo.Id,userId))
                throw new ToDoNotFoundException("Not Found");

            var toDoToUpdate = toDo.Adapt<ToDo>();
            var created = await _repository.GetCreatedDate(cancellationToken,toDo.Id);
            toDoToUpdate.CreatedAt = created;
            toDoToUpdate.UserId = userId;
            await _repository.UpdateToDoAsync(cancellationToken, toDoToUpdate);
        }


        public async Task<int> DeleteAsync(CancellationToken cancellationToken, int id,int userId)
        {
            if (!await _repository.Exists(cancellationToken, id,userId))
                throw new ToDoNotFoundException("Not Found");
            return await _repository.DeleteByStatus(cancellationToken, id);
        }

        public async Task UpdateToDoPatchAsync(CancellationToken cancellationToken,int toDoId, JsonPatchDocument toDo,int userId)
        {
            if (!await _repository.Exists(cancellationToken, toDoId,userId))
                throw new ToDoNotFoundException("Not Found");
            await _repository.UpdateToDoPatchAsync(cancellationToken, toDoId,  toDo);
        }

    }
}
