using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        private readonly AppDbContext _appDbContext;
        public GroupRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task<List<Group>> GetAllWithEducationName()
        {
            return await _appDbContext.Groups.Include(m=>m.Education).ToListAsync();
        }
    }
}
