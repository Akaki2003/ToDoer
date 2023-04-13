using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading;
using ToDoer.API.Infrastructure.Auth.JWT;
using ToDoer.Application.Users;
using ToDoer.Application.Users.Requests;
using ToDoer.Application.Users.Responses;

namespace ToDoer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<JWTConfiguration> _options;

        public AuthorizationController(IUserService userService, IOptions<JWTConfiguration> options)
        {
            _userService = userService;
            _options = options;
        }


        [Route("Register")]
        [HttpPost]
        public async Task Register(CancellationToken cancellation, UserCreateModel user)
        {
            _ = await _userService.CreateAsync(cancellation, user);
        }


        [Route("LogIn")]
        [HttpPost]
        public async Task<string> LogIn(CancellationToken cancellation, UserLoginReqest request)
        {
            var Id = await _userService.GetUserIdByUsername(cancellation,request.Username);
            var result = await _userService.AuthenticateAsync(cancellation, request.Username, request.Password);

            return JWTHelper.GenerateSecurityToken(result, Id, _options);
        }


    }
}
