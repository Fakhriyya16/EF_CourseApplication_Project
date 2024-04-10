using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Helpers.Constants;
using Service.Helpers.Enums;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CourseApplication_Project.Controllers
{
    public class EducationController
    {
        private readonly IEducationService _educationService;
        public EducationController()
        {
            _educationService = new EducationService();
        }
        public async void CreateAsync()
        {
            ConsoleColor.Cyan.ConsoleMessage("Create name: ");
            string educationName = Console.ReadLine();

            ConsoleColor.Cyan.ConsoleMessage("Add color: ");
            string educationColor = Console.ReadLine();

            Education newEducation = new Education { Name = educationName, Color = educationColor };
            await _educationService.CreateAsync(newEducation);

            ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
        }

        public async void DeleteAsync()
        {
        EducationId: ConsoleColor.Cyan.ConsoleMessage("Add id of the education course,you want to delete:");
            string educationIdStr = Console.ReadLine();
            int educationId;
            
            bool isCorrectFormat = int.TryParse(educationIdStr, out educationId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto EducationId;
            }

            await _educationService.DeleteAsync(educationId);
            ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
        }
        public async void GetAllAsync()
        {
            var educations = await _educationService.GetAllAsync();
            foreach(var education in educations)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay,education.Id,education.Name,education.Color, education.CreatedDate));
            }
        }
        //--------------work on DTOs
        public async void GetAllWithGroups()
        {
            var educations = await _educationService.GetAllWithGroupsAsync();
            foreach(var education in educations)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color,education.CreatedDate));
            }
        }
        //---------------
        public async void GetByIdAsync()
        {
        EducationId: ConsoleColor.Cyan.ConsoleMessage("Add id of the education course:");
            string educationIdStr = Console.ReadLine();
            int educationId;

            bool isCorrectFormat = int.TryParse(educationIdStr, out educationId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto EducationId;
            }
            Education education = await _educationService.GetByIdAsync(educationId);
            ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color,education.CreatedDate));
        }
        public async void SearchAsync()
        {
            ConsoleColor.Cyan.ConsoleMessage("Add search text:");
            string searchText = Console.ReadLine();

            var educationsFound = await _educationService.SearchAsync(searchText);
            foreach (var education in educationsFound)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color,education.CreatedDate));
            }
        }
        public async void SortWithCreatedDate()
        {
        OrderNumber: ConsoleColor.Cyan.ConsoleMessage("Choose the sorting format:\nType 1 for ascending order\nType 2 for descending order");
            string orderNumStr = Console.ReadLine();
            int orderNum;

            bool isCorrectFormat = int.TryParse(orderNumStr, out orderNum);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto OrderNumber;
            }
            
            var educationsSorted = await _educationService.SortWithCreatedDate(orderNum);
            foreach (var education in educationsSorted)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color,education.CreatedDate));
            }
        }
        public async void UpdateAsync()
        {
        EducationId: ConsoleColor.Cyan.ConsoleMessage("Enter id of the education course, you want to update:");
            string educationIdStr = Console.ReadLine();
            int educationId;

            bool isCorrectFormat = int.TryParse(educationIdStr, out educationId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto EducationId;
            }
            Education educationFound = await _educationService.GetByIdAsync(educationId);

            ConsoleColor.Cyan.ConsoleMessage("Update name:");
            string educationNewName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(educationNewName))
            {
                educationNewName = _educationService.UpdateAsync(educationId).Result.Name;
            }
            else
            {
                educationFound.Name = educationNewName;
            }

            ConsoleColor.Cyan.ConsoleMessage("Update color: ");
            string educationNewColor = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(educationNewColor))
            {
                educationNewColor = _educationService.UpdateAsync(educationId).Result.Color;
            }
            else
            {
                educationFound.Color = educationNewColor;
            }
            ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
        }
    }
}
