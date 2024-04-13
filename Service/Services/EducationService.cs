using Domain.Models;
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
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _repository;
        public EducationService()
        {
            _repository = new EducationRepository();
        }
        public async Task CreateAsync(Education education)
        {
            try
            {
                await _repository.CreateAsync(education);
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
                var foundEducation = await _repository.GetByExpressionAsync(m => m.Id == id);
                if (foundEducation is null) throw new NotFoundException("Course was not found");
                await _repository.DeleteAsync(foundEducation);
                await ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }
        }

        public async Task<List<Education>> GetAllAsync()
        {
            var educations = await _repository.GetAllAsync();
            if (educations.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);
            return educations;
        }

        public async Task<List<EducationGroupDTO>> GetAllWithGroupsAsync()
        {
            var data = await _repository.GetAllWithGroups();
            if (data.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);

            var result = data.Select(m => new EducationGroupDTO
            {
                Education = m.Name,
                Groups = m.Groups.Select(m => m.Name).ToList()
            });
            return result.ToList();
        }

        public async Task<Education> GetByIdAsync(int? id)
        {
            var result = await _repository.GetByExpressionAsync(m=>m.Id == id);
            if (result is null) throw new NotFoundException(ResponseMessages.NotFound);
            return result;
        }

        public async Task<List<EducationDTO>> SearchAsync(string searchText)
        {
            List<EducationDTO> educationDTOs = new();
            var educations = await _repository.SearchAsync(m => m.Name.Trim().ToLower().Contains(searchText.Trim().ToLower()));
            if (educations.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);
            foreach(var education in educations)
            {
                educationDTOs.Add(new EducationDTO { Name = education.Name, Color = education.Color });
            }
            return educationDTOs;
        }

        public async Task<List<Education>> SortWithCreatedDate(int orderType)
        {
            var educations = await _repository.GetAllAsync();
            if (educations.Count == 0) throw new NotFoundException(ResponseMessages.NotFound);
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
            if (education is null) throw new NotFoundException(ResponseMessages.NotFound + "Update Failed");
            return education;
        }
    }
}
