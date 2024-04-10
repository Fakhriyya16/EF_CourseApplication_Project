using Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<List<Group>> GetAllAsync();
        public Task<Group> GetByIdAsync(int? id);
        public Task DeleteAsync(Group group);
        public Task<Group> UpdateAsync(int? id);
        public Task<List<Group>> SearchAsync(string searchName);
        public Task<List<Group>> FilterByEducationNameAsync(string groupName);
        public Task<List<Group>> GetAllWithEducationIdAsync(int? id);
        public Task<List<Group>> SortWithCapacityAsync(int? orderType);
        public Task CreateAsync(Group group);

    }
}
