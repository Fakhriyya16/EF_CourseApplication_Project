using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repositories;
using Repositories.Repositories.Interfaces;
using Service.DTOs;
using Service.Helpers.Constants;
using Service.Helpers.Enums;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
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
        private readonly AppDbContext _appDbContext;
        private readonly IEducationRepository _educationRepository;
        public GroupService()
        {
            _repository = new GroupRepository();
            _appDbContext = new AppDbContext();
            _educationRepository = new EducationRepository();
        }
        public async Task CreateAsync(Group group)
        {
            try
            {
                await _repository.CreateAsync(group);
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }
        }

        public async Task DeleteAsync(int? id)
        {
            try
            {
                var foundGroup = await _repository.GetByExpressionAsync(m => m.Id == id);
                if (foundGroup is null) throw new NotFoundException(ResponseMessages.NotFound);
                await _repository.DeleteAsync(foundGroup);
                await ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }
        }

        public async Task<List<GroupDTO>> FilterByEducationNameAsync(string groupName)
        {
            List<GroupDTO> groupDTOs = new();
            var result = await _repository.GetAllByExpressionAsync(m => m.Education.Name.Trim().ToLower() == groupName.Trim().ToLower());
            if (result.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);
            foreach (var item in result)
            {
                groupDTOs.Add(new GroupDTO { Name = item.Name, Capacity = item.Capacity });
            }
            return groupDTOs;
        }

        public async Task<List<Group>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            if (result.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);
            return result;
        }

        public async Task<List<GroupDTO>> GetAllWithEducationIdAsync(int? id)
        {
            List<GroupDTO> groupDTOs = new();
            var result = await _repository.GetAllByExpressionAsync(m => m.EducationId == id);
            if (result.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);
            foreach (var item in result)
            {
                groupDTOs.Add(new GroupDTO { Name = item.Name, Capacity = item.Capacity });
            }
            return groupDTOs;
        }

        public async Task<Group> GetByIdAsync(int? id)
        {
            var result = await _repository.GetByExpressionAsync(m => m.Id == id);
            if (result is null) throw new NotFoundException(ResponseMessages.NotFound);
            return result;
        }

        public async Task<List<GroupDTO>> SearchAsync(string searchName)
        {
            List<GroupDTO> groupDTOs = new();
            var result = await _repository.GetAllByExpressionAsync(m => m.Name.Trim().ToLower().Contains(searchName.Trim().ToLower()));
            if (result.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);
            foreach (var groupDTO in result)
            {
                groupDTOs.Add(new GroupDTO { Name = groupDTO.Name, Capacity = groupDTO.Capacity });
            }
            return groupDTOs;
        }

        public async Task<List<GroupDTO>> SortWithCapacityAsync(int? orderType)
        {
            List<GroupDTO> groupDTOs = new();
            var groups = await _repository.GetAllAsync();
            if (groups.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);
            foreach (var groupDTO in groups)
            {
                groupDTOs.Add(new GroupDTO { Name = groupDTO.Name, Capacity = groupDTO.Capacity });
            }
            if (orderType == (int)OrderTypes.Ascending)
            {
                return groupDTOs.OrderBy(m => m.Capacity).ToList();
            }
            else if (orderType == (int)OrderTypes.Descending)
            {
                return groupDTOs.OrderByDescending(m => m.Capacity).ToList();
            }
            return groupDTOs;
        }

        public async Task UpdateGroup(Group group)
        {
            await _repository.UpdateGroup(group);
        }
    }
}
