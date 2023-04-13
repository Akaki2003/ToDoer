using Mapster;
using ToDoer.Application.Users.Requests;
using ToDoer.Domain.Users;

namespace ToDoer.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<UserCreateModel, User>
            .NewConfig().Map(dest => dest.PasswordHash, src => src.Password);
        }
    }
}
