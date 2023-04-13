
using ToDoer.Application.Subtasks.Requests;
using ToDoer.Application.Subtasks.Responses;

namespace ToDoer.Application.Subtasks
{
    public interface ISubtaskService
    {
        Task CreateAsync(CancellationToken cancellationToken, SubtaskRequestModel subtask, int todoId);
        Task<SubtaskResponseModel> GetByIdAsync(CancellationToken cancellationToken, int subtaskId,int userId);
        Task<int> DeleteAsync(CancellationToken cancellationToken, int subtaskId, int userId);
        Task UpdateSubtaskAsync(CancellationToken cancellationToken, SubtaskRequestPutModel subtask, int userId);

    }
}
