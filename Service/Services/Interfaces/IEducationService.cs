using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        public Task CreateAsync(Education education);
        public Task<List<Education>> GetAllAsync();
        public Task<Education> GetByIdAsync(int? id);
        public Task DeleteAsync(int? id);
        public Task<Education> UpdateAsync(int? id);
        public Task<List<Education>> SearchAsync(string searchText);
        public Task<List<Education>> GetAllWithGroupsAsync();
        public Task<List<Education>> SortWithCreatedDate(int orderType);
    }
}
