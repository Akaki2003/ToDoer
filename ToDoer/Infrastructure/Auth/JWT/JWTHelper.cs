using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ToDoer.API.Infrastructure.Auth.JWT
{
    public static class JWTHelper
    {
        public static string GenerateSecurityToken(string userName,int Id, IOptions<JWTConfiguration> options)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(options.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim("UserId", Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpirationInMinutes),
                Audience = "localhost",
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            //var identity = new ClaimsIdentity();
            //identity.AddClaim(new Claim("UserId", Id.ToString())); // we need userid for getall todos method
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
