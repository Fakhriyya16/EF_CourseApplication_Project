using Domain.Models;
using Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IEducationRepository : IBaseRepository<Education>
    {
        public Task<List<Education>> GetAllWithGroups();
        public Task<Education> GetEducationByExpressionAsync(Func<Education, bool> predicate);
        public Task<List<Education>> GetAllAsync();

    }
}
