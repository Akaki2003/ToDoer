using Microsoft.EntityFrameworkCore;
using System;
using ToDoer.Application;
using ToDoer.Application.Users.Repositories;
using ToDoer.Domain.Users;
using ToDoer.Persistence.Context;

namespace ToDoer.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        //private IBaseRepository<User> _repository;


        public UserRepository(ToDoerContext context) : base(context)
        {
 
        }


        public async Task<int> CreateAsync(CancellationToken cancellationToken, User user)
        {
            user.CreatedAt = DateTime.Now;
            user.ModifiedAt = DateTime.Now;
            await AddAsync(cancellationToken, user);
            return user.Id;
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id)
        {
            return await base.AnyAsync(cancellationToken, x => x.Id == id);
        }


        public async Task<bool> ExistsByUsername(CancellationToken cancellationToken, string username)
        {
            return await base.AnyAsync(cancellationToken, x => x.UserName == username);
        }
        public async Task<User> GetByUsernameAndPassword(CancellationToken cancellationToken, string username, string password)
        {
            return await _context.Set<User>().SingleOrDefaultAsync(x=>x.UserName==username&&password== x.PasswordHash, cancellationToken);
        }

        public async Task<int> GetUserIdByUsername(CancellationToken cancellationToken,string name)
        {
            var user = await _context.Set<User>().SingleOrDefaultAsync(x => x.UserName == name);
            return user.Id;
        }
    }
}
