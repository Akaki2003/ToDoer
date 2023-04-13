using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ToDoer.Application.ToDos.Repositories;
using ToDoer.Domain.ToDos;
using ToDoer.Domain.Users;
using ToDoer.Persistence.Context;

namespace ToDoer.Infrastructure.ToDos
{
    public class ToDoRepository : BaseRepository<ToDo>, IToDoRepository
    {
        public ToDoRepository(ToDoerContext context) : base(context)
        {

        }
        public async Task<List<ToDo>> GetAllFullAsync(CancellationToken cancellationToken, int userId,StatusChecking? status)
        {
            //return await _context.Set<ToDo>().Where(x => x.UserId == userId).Include(x => x.Subtasks).ToListAsync();
            var todos = await _context.Set<ToDo>().Where(x => x.UserId == userId).Include(x => x.Subtasks).ToListAsync();
            return todos.Where(x=>x.Status.ToString()==status.ToString()).ToList();
        }

        public async Task<ToDo> GetToDoByIdAsync(CancellationToken cancellationToken, int id)
        {
            return await _context.Set<ToDo>().Include(x=>x.Subtasks).SingleOrDefaultAsync(x => x.Id == id,cancellationToken);
        }


        public async Task<int> CreateAsync(CancellationToken cancellationToken, ToDo toDo,int userId)
        {
            toDo.ModifiedAt = DateTime.UtcNow;
            toDo.CreatedAt = DateTime.UtcNow;
            toDo.Status = Status.Active;
            toDo.UserId = userId;
          
            await AddAsync(cancellationToken, toDo);
            return toDo.Id;
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id,int userId)
        {
            return await AnyAsync(cancellationToken, x => x.Id == id && x.UserId == userId && x.Status != Status.Deleted);
        }


        public async Task UpdateToDoAsync(CancellationToken cancellationToken, ToDo toDo)
        {
            toDo.ModifiedAt = DateTime.UtcNow;
            await UpdateAsync(cancellationToken, toDo);
        }

        public async Task UpdateToDoPatchAsync(CancellationToken cancellationToken, int id, JsonPatchDocument toDo)
        {
            var toDoToUpdate = await _context.Set<ToDo>().FindAsync(id); 
            if(toDoToUpdate != null)
            {
                toDo.ApplyTo(toDoToUpdate);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<DateTime> GetCreatedDate(CancellationToken cancellationToken, int id)
        {
            var toDo = await _context.Set<ToDo>().SingleOrDefaultAsync(x => x.Id == id);
            var date = toDo.CreatedAt;
            if (toDo != null)
            {
                _context.Entry(toDo).State = EntityState.Detached;
            }
            _context.SaveChanges();

            return date;
        }

        public async Task<int> DeleteByStatus(CancellationToken cancellationToken, int id)
        {
            var toDo = await GetToDoByIdAsync(cancellationToken, id);
            toDo.Status = Status.Deleted;
            
            await UpdateAsync(cancellationToken, toDo);
            return id;
        }
    }
}
