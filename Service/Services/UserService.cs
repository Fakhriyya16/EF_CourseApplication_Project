using Domain.Models;
using Repositories.Repositories;
using Repositories.Repositories.Interfaces;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
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
            _repository = new UserRepository(new Repositories.Data.AppDbContext());
        }
        public async Task<bool> LoginAsync(string usernameOrEmail,string password)
        {
            try
            {
                var users = await _repository.LoginAsync();
                if (users.Count == 0) return false;
                foreach (var user in users)
                {
                    if (user.Email == usernameOrEmail || user.UserName == usernameOrEmail)
                    {
                        if (user.Password == password)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
                return false;
            }
        }

        public async Task RegisterAsync(User user)
        {
            try
            {
                await _repository.RegisterAsync(user);
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }

        }
    }
}
