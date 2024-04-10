using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> LoginAsync(string usernameOrEmail, string password);
        public Task RegisterAsync(User user);
    }
}
