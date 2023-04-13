using Mapster;
using ToDoer.Application.Exceptions;
using ToDoer.Application.Subtasks.Repositories;
using ToDoer.Application.Subtasks.Requests;
using ToDoer.Application.Subtasks.Responses;
using ToDoer.Application.ToDos.Repositories;
using ToDoer.Domain.Subtasks;

namespace ToDoer.Application.Subtasks
{
    public class SubtaskService : ISubtaskService
    {
        private readonly ISubtaskRepository _repository;
        private readonly IToDoRepository _toDorepository;


        public SubtaskService(ISubtaskRepository repository,IToDoRepository toDoRepository)
        {
            _repository = repository;
            _toDorepository = toDoRepository;
        }
        public async Task CreateAsync(CancellationToken cancellationToken, SubtaskRequestModel subtask, int todoId)
        {
            var subtaskToInsert = subtask.Adapt<Subtask>();

            await _repository.CreateAsync(cancellationToken, subtaskToInsert, todoId);
        }

        public async Task<SubtaskResponseModel> GetByIdAsync(CancellationToken cancellationToken, int subtaskId,int userId)
        {
            var subtask = await _repository.GetSubtaskByIdAsync(cancellationToken, subtaskId);
            try
            {
                var currentTodo = (await _toDorepository.GetAllAsync(cancellationToken)).Find(x => x.Subtasks.Any(x => x.Id == subtaskId) && x.UserId == userId);

            }
            catch (Exception)
            {
                throw new SubtaskNotFoundException("Not found");
            }
            return subtask.Adapt<SubtaskResponseModel>();
        }

        public async Task<int> DeleteAsync(CancellationToken cancellationToken, int subtaskId,int userId)
        {
            if (!await _repository.Exists(cancellationToken, subtaskId,userId))
                throw new SubtaskNotFoundException("Not Found");
           
           
            return await _repository.DeleteByStatus(cancellationToken, subtaskId);
        }

        public async Task UpdateSubtaskAsync(CancellationToken cancellationToken, SubtaskRequestPutModel subtask, int userId)
        {
            if (!await _repository.Exists(cancellationToken, subtask.Id,userId))
                throw new SubtaskNotFoundException("Not Found");

            var subtaskToUpdate = subtask.Adapt<Subtask>();
            var created = await _repository.GetCreatedDate(cancellationToken, subtask.Id);
            subtaskToUpdate.CreatedAt = created;
            await _repository.UpdateSubtaskAsync(cancellationToken, subtaskToUpdate);
        }

    }
}
