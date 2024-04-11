using Domain.Models;
using Service.DTOs;
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
        public Task DeleteAsync(int? id);
        public Task<Group> UpdateAsync(int? id);
        public Task<List<GroupDTO>> SearchAsync(string searchName); 
        public Task<List<GroupDTO>> FilterByEducationNameAsync(string groupName); 
        public Task<List<GroupDTO>> GetAllWithEducationIdAsync(int? id);
        public Task<List<GroupDTO>> SortWithCapacityAsync(int? orderType); 
        public Task CreateAsync(Group group);

    }
}
