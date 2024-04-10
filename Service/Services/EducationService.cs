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
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _repository;
        public EducationService()
        {
            _repository = new EducationRepository();
        }
        public async Task CreateAsync(Education education)
        {
            await _repository.CreateAsync(education);
        }

        public async Task DeleteAsync(int? id)
        {
            var foundEducation = await _repository.GetByExpressionAsync(m=>m.Id == id);
            await _repository.DeleteAsync(foundEducation);
        }

        public async Task<List<Education>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<Education>> GetAllWithGroupsAsync()
        {
            return await _repository.GetAllWithRelationsAsync();
        }

        public async Task<Education> GetByIdAsync(int? id)
        {
            return await _repository.GetByExpressionAsync(m=>m.Id == id);
        }

        public async Task<List<Education>> SearchAsync(string searchText)
        {
            return await _repository.SearchAsync(m => m.Name == searchText);
        }

        public async Task<List<Education>> SortWithCreatedDate(int orderType)
        {
            var educations = await _repository.GetAllAsync();
            if(orderType == (int)OrderTypes.Ascending)
            {
                return educations.OrderBy(m=>m.CreatedDate).ToList();
            }
            else if(orderType == (int)OrderTypes.Descending)
            {
                return educations.OrderByDescending(m => m.CreatedDate).ToList();
            }
            return educations;
        }

        public async Task<Education> UpdateAsync(int? id)
        {
            Education education = await _repository.UpdateAsync(id, await _repository.GetByExpressionAsync(m => m.Id == id));
            return education;
        }
    }
}
