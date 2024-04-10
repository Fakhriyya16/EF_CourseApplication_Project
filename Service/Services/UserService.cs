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
        public async Task LoginAsync(User user)
        {
            await _repository.LoginAsync(user);
        }

        public async Task RegisterAsync(User user)
        {
            await _repository.RegisterAsync(user);
        }
    }
}
