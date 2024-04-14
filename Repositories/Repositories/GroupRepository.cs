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
        public GroupRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Group>> GetAllWithEducationName()
        {
            return await _appDbContext.Groups.Include(m => m.Education).ToListAsync();
        }
        public async Task<Group> GetByExpressionAsync(Func<Group, bool> predicate)
        {
            var datas = await _appDbContext.Groups.Include(m => m.Education).ToListAsync();
            var result = datas.FirstOrDefault(predicate);
            return result;
        }
        public async Task<List<Group>> GetAllAsync()
        {          
            return await _appDbContext.Groups.Include(m=>m.Education).ToListAsync();
        }

        public async Task<List<Group>> GetAllByExpressionAsync(Func<Group, bool> predicate)
        {
            return _appDbContext.Groups.Include(m => m.Education).Where(predicate).ToList();
        }
        public async Task<List<Group>> SearchAsync(Func<Group, bool> predicate)
        {
            var datas = await _appDbContext.Groups.Include(m => m.Education).ToListAsync();
            var result = datas.Where(predicate).ToList();
            return result;
        }

    }
}
