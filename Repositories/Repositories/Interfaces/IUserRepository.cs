using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task RegisterAsync(User user);
        public Task LoginAsync(User user);

    }
}
