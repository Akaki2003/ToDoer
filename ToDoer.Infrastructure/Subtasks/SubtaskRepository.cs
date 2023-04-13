using Microsoft.EntityFrameworkCore;
using ToDoer.Application.Subtasks.Repositories;
using ToDoer.Application.ToDos.Repositories;
using ToDoer.Domain.Subtasks;
using ToDoer.Domain.ToDos;
using ToDoer.Infrastructure.ToDos;
using ToDoer.Persistence.Context;
using Status = ToDoer.Domain.Status;

namespace ToDoer.Infrastructure.Subtasks
{
    public class SubtaskRepository : BaseRepository<Subtask>, ISubtaskRepository
    {
        private readonly IToDoRepository _todoRepository;
        public SubtaskRepository(ToDoerContext context,IToDoRepository toDoRepository) : base(context)
        {
            _todoRepository = toDoRepository;
        }
        public async Task<int> CreateAsync(CancellationToken cancellationToken, Subtask subtask, int toDoId)
        {
            subtask.ModifiedAt = DateTime.UtcNow;
            subtask.CreatedAt = DateTime.UtcNow;
            subtask.Status = Status.Active;
            subtask.TodoItemId = toDoId;
            await AddAsync(cancellationToken, subtask);
            return subtask.Id;
        }

        public async Task<Subtask> GetSubtaskByIdAsync(CancellationToken cancellationToken, int id)
        {
            return await _context.Set<Subtask>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id,int userId)
        {
            var toDos = await _todoRepository.GetAllFullAsync(cancellationToken,userId,null);
            return toDos.SelectMany(x => x.Subtasks.Select(x => x.Id)).Contains(id);
        }

        public async Task<int> DeleteByStatus(CancellationToken cancellationToken, int id)
        {
            var subtask = await GetSubtaskById(cancellationToken, id);
            subtask.Status = Status.Deleted;

            await UpdateAsync(cancellationToken, subtask);
            return id;
        }


        public async Task<Subtask> GetSubtaskById(CancellationToken cancellationToken, int id)
        {
            return await _context.Set<Subtask>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }


        public async Task UpdateSubtaskAsync(CancellationToken cancellationToken, Subtask subtask)
        {
            subtask.ModifiedAt = DateTime.UtcNow;
            await UpdateAsync(cancellationToken, subtask);
        }


        public async Task<DateTime> GetCreatedDate(CancellationToken cancellationToken, int id)
        {
            var subtask = await _context.Set<Subtask>().SingleOrDefaultAsync(x => x.Id == id);
            var date = subtask.CreatedAt;
            if (subtask != null)
            {
                _context.Entry(subtask).State = EntityState.Detached;
            }
            _context.SaveChanges();

            return date;
        }


    }

}

