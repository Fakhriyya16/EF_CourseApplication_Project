using Domain.Models;
using Repositories.Repositories;
using Repositories.Repositories.Interfaces;
using Service.Helpers.Enums;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        public GroupService()
        {
            _repository = new GroupRepository();
        }
        public async Task CreateAsync(Group group)
        {
            await _repository.CreateAsync(group);
        }

        public async Task DeleteAsync(int? id)
        {
            var foundGroup = await _repository.GetByExpressionAsync(m => m.Id == id);
            await _repository.DeleteAsync(foundGroup);
        }

        public async Task<List<Group>> FilterByEducationNameAsync(string groupName)
        {
            return await _repository.GetAllByExpressionAsync(m=> m.Education.Name == groupName);
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<Group>> GetAllWithEducationIdAsync(int? id)
        {
            return await _repository.GetAllByExpressionAsync(m=>m.EducationId == id);
        }

        public async Task<Group> GetByIdAsync(int? id)
        {
            return await _repository.GetByExpressionAsync(m=>m.Id == id);
        }

        public async Task<List<Group>> SearchAsync(string searchName)
        {
            return await _repository.GetAllByExpressionAsync(m => m.Name == searchName);
        }

        public async Task<List<Group>> SortWithCapacityAsync(int? orderType)
        {
            var groups = await _repository.GetAllAsync();
            if (orderType == (int)OrderTypes.Ascending)
            {
                return groups.OrderBy(m => m.Capacity).ToList();
            }
            else if (orderType == (int)OrderTypes.Descending)
            {
                return groups.OrderByDescending(m => m.Capacity).ToList();
            }
            return groups;
        }

        public async Task<Group> UpdateAsync(int? id)
        {
            Group group = await _repository.UpdateAsync(id, await _repository.GetByExpressionAsync(m => m.Id == id));
            return group;
        }
    }
}
