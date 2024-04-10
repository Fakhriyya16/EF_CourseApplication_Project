using Domain.Models;
using Repositories.Repositories;
using Repositories.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService()
        {
            _repository = new UserRepository();
        }
        public async Task<bool> LoginAsync(string usernameOrEmail,string password)
        {
            var users = await _repository.LoginAsync();
            foreach (var user in users)
            {
                if(user.Email == usernameOrEmail || user.UserName == usernameOrEmail)
                {
                    if (user.Password == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task RegisterAsync(User user)
        {
            await _repository.RegisterAsync(user);
        }
    }
}
