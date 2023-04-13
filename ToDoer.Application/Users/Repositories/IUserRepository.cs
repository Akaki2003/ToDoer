using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoer.Domain.Users;

namespace ToDoer.Application.Users.Repositories
{
    //public interface IUserRepository:IBaseRepository<User>
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> GetByUsernameAndPassword(CancellationToken cancellationToken, string username, string password);
        Task<int> CreateAsync(CancellationToken cancellationToken, User user);
        Task UpdateAsync(CancellationToken cancellationToken, User user);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
        Task<bool> ExistsByUsername(CancellationToken cancellationToken, string username);
        Task<int> GetUserIdByUsername(CancellationToken cancellationToken, string name);
    }
}
