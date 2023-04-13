
using ToDoer.Domain.Subtasks;

namespace ToDoer.Application.Subtasks.Repositories
{
    public interface ISubtaskRepository
    {
        Task<int> CreateAsync(CancellationToken cancellationToken, Subtask subtask, int toDoId);
        Task<Subtask> GetSubtaskByIdAsync(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id,int userId);
        Task<int> DeleteByStatus(CancellationToken cancellationToken, int userId);
        Task UpdateSubtaskAsync(CancellationToken cancellationToken, Subtask subtask);

        Task<DateTime> GetCreatedDate(CancellationToken cancellationToken, int id);


    }
}
