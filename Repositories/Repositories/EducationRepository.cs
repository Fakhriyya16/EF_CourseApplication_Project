using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext _appDbContext;
        public EducationRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Education>> GetAllWithGroups(string msg = null)
        {
            return await _appDbContext.Educations.Include(m=>m.Groups).ToListAsync();
        }
        public async Task<Education> GetEducationByExpressionAsync(Func<Education, bool> predicate)
        {
            return _appDbContext.Educations.Include(m=>m.Groups).FirstOrDefault(predicate);
        }
        public async Task<List<Education>> GetAllAsync()
        {
            return await _appDbContext.Educations.Include(m=>m.Groups).ToListAsync();
        }

    }
}
