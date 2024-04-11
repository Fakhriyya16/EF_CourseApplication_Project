using Domain.Models;
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
    }
}
