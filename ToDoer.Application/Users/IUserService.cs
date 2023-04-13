using ToDoer.Application.Users.Requests;
using ToDoer.Application.Users.Responses;

namespace ToDoer.Application.Users
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(CancellationToken cancellation, string username, string password);
        Task<UserResponseModel> CreateAsync(CancellationToken cancellation, UserCreateModel user);
        Task<int> GetUserIdByUsername(CancellationToken cancellationToken, string Name);
    }
}
