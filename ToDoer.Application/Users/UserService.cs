using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDoer.Application.Users.Repositories;
using ToDoer.Application.Users.Requests;
using ToDoer.Application.Users.Responses;
using ToDoer.Domain.Users;

namespace ToDoer.Application.Users
{
    public class UserService : IUserService
    {
        const string SECRET_KEY = "secKey";
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> AuthenticateAsync(CancellationToken cancellationToken, string username, string password)
        {
            var hashed = GenerateHash(password);
            var userEntity = await _repository.GetByUsernameAndPassword(cancellationToken, username, hashed);

            if (userEntity == null)
                throw new Exception("username or password is incorrect");

            return userEntity.UserName;
        }


        private string GenerateHash(string input)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + SECRET_KEY);
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

    
        public async Task<UserResponseModel> CreateAsync(CancellationToken cancellationToken, UserCreateModel userModel)
        {
            var exists = await _repository.ExistsByUsername(cancellationToken, userModel.Username);

            if (exists)
                throw new Exception("user already exists");
            userModel.Password = GenerateHash(userModel.Password);
            
            var userEntity = userModel.Adapt<User>();

            var existedUser = await _repository.CreateAsync(cancellationToken, userEntity);

            return existedUser.Adapt<UserResponseModel>();
        }


        public async Task<int> GetUserIdByUsername(CancellationToken cancellationToken, string name)
        {
            try
            {
                return await _repository.GetUserIdByUsername(cancellationToken, name);
            }
            catch (Exception)
            {
                throw new Exception("User not found");
            }
        }
    }

}
