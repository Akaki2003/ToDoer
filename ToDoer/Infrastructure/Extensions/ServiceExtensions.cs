using ToDoer.Application.Subtasks;
using ToDoer.Application.Subtasks.Repositories;
using ToDoer.Application.ToDos;
using ToDoer.Application.ToDos.Repositories;
using ToDoer.Application.Users;
using ToDoer.Application.Users.Repositories;
using ToDoer.Infrastructure.ToDos;
using ToDoer.Infrastructure.Users;
using ToDoer.Infrastructure.Subtasks;

namespace ToDoer.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoService, ToDoService>();
            services.AddScoped<ISubtaskService, SubtaskService>();


            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ISubtaskRepository, SubtaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
