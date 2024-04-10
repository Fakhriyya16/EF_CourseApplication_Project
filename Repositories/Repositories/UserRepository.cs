using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task<List<User>> LoginAsync()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task RegisterAsync(User user)
        {
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
