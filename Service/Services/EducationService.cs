using Domain.Models;
using Repositories.Repositories;
using Repositories.Repositories.Interfaces;
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

        public async Task DeleteAsync(Education education)
        {
            await _repository.DeleteAsync(education);
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

        public async Task<List<Education>> SortWithCreatedDate()
        {
            return await _repository.GetAllAsync();
        }

        public Task UpdateAsync(Education education)
        {
            throw new NotImplementedException();
        }
    }
}
