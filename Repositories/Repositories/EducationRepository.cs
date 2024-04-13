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
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext _appDbContext;
        public EducationRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task<List<Education>> GetAllWithGroups()
        {
            _appDbContext.ChangeTracker.AcceptAllChanges();
            return await _appDbContext.Educations.Include(m => m.Groups).ToListAsync();
        }
    }
}
