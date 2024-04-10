using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = Domain.Models.Group;

namespace EF_CourseApplication_Project.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        public GroupController()
        {
            _groupService = new GroupService();
        }
        public async void CreateAsync()
        {
            ConsoleColor.Cyan.ConsoleMessage("Create name: ");
            string groupName = Console.ReadLine();

        GroupCapacity: ConsoleColor.Cyan.ConsoleMessage("Add capacity: ");
            string groupCapacityStr = Console.ReadLine();
            int groupCapacity;

            bool isCorrect = int.TryParse(groupCapacityStr, out groupCapacity);
            if(!isCorrect)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat); // id to int
                goto GroupCapacity;
            }

        EducationId: ConsoleColor.Cyan.ConsoleMessage("Add capacity: ");
            string educationIdStr = Console.ReadLine();
            int educationId;

            isCorrect = int.TryParse(educationIdStr, out educationId);
            if (!isCorrect)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto EducationId;
            }

            Group group = new Group {Name = groupName,Capacity = groupCapacity, EducationId = educationId};
            await _groupService.CreateAsync(group);
            ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
        }
        public async void DeleteAsync()
        {
        GroupId: ConsoleColor.Cyan.ConsoleMessage("Enter if of the group, you want to delete:");
            string groupIdStr = Console.ReadLine();
            int groupId;

            bool isCorrectFormat = int.TryParse(groupIdStr, out groupId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto GroupId;
            }

            await _groupService.DeleteAsync(groupId);
            ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
        }
        public async void GetAllAsync()
        {
            var groups = await _groupService.GetAllAsync();
            foreach (var group in groups)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, group.Id, group.Name, group.Capacity,group.Education, group.CreatedDate));
            }
        }
        public async void GetByIdAsync()
        {
        GroupId: ConsoleColor.Cyan.ConsoleMessage("Add id of the education course:");
            string groupIdStr = Console.ReadLine();
            int groupId;

            bool isCorrectFormat = int.TryParse(groupIdStr, out groupId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto GroupId;
            }
            Group group = await _groupService.GetByIdAsync(groupId);
            ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, group.Id, group.Name, group.Capacity, group.Education, group.CreatedDate));
        }
        public async void SearchAsync()
        {
            ConsoleColor.Cyan.ConsoleMessage("Add search text:");
            string searchText = Console.ReadLine();

            var groupsFound = await _groupService.SearchAsync(searchText);
            foreach (var group in groupsFound)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, group.Id, group.Name, group.Capacity, group.Education, group.CreatedDate));
            }
        }
        public async void SortByCapacity()
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

            var groupsSorted = await _groupService.SortWithCapacityAsync(orderNum);
            foreach (var group in groupsSorted)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, group.Id, group.Name, group.Capacity, group.Education, group.CreatedDate));
            }
        }
        public async void UpdateAsync()
        {
        GroupId: ConsoleColor.Cyan.ConsoleMessage("Enter id of the group, you want to update:");
            string groupIdStr = Console.ReadLine();
            int groupId;

            bool isCorrectFormat = int.TryParse(groupIdStr, out groupId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto GroupId;
            }
            Group groupFound = await _groupService.GetByIdAsync(groupId);

            ConsoleColor.Cyan.ConsoleMessage("Update name:");
            string groupNewName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(groupNewName))
            {
                groupNewName = _groupService.UpdateAsync(groupId).Result.Name;
            }
            else
            {
                groupFound.Name = groupNewName;
            }

        GroupCapacity: ConsoleColor.Cyan.ConsoleMessage("Update capacity: ");
        
            string groupCapacityStr = Console.ReadLine();
            int groupCapacity;

            bool isCorrect = int.TryParse(groupCapacityStr, out groupCapacity);
            if (!isCorrect)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat); // id to int
                goto GroupCapacity;
            }
            if (groupCapacityStr is null) groupCapacity = _groupService.UpdateAsync(groupId).Result.Capacity;
            else
            {
                groupFound.Capacity = groupCapacity;
            }

        EducationId: ConsoleColor.Cyan.ConsoleMessage("Update capacity: ");

            string groupEducationIdStr = Console.ReadLine();
            int groupEducationId;

            isCorrect = int.TryParse(groupEducationIdStr, out groupEducationId);
            if (!isCorrect)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat); // id to int
                goto EducationId;
            }
            if (groupEducationIdStr is null) groupEducationId = _groupService.UpdateAsync(groupId).Result.EducationId;
            else
            {
                groupFound.EducationId = groupEducationId;
            }
            ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
        }
        public async void FilterByEducationName()
        {
            ConsoleColor.Cyan.ConsoleMessage("Enter education name:");
            string educationName = Console.ReadLine();

            var groups = await _groupService.FilterByEducationNameAsync(educationName);
            foreach (var group in groups)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, group.Id, group.Name, group.Capacity, group.Education, group.CreatedDate));
            }
        }
        public async void GetAllByEducationId()
        {
        EducationId: ConsoleColor.Cyan.ConsoleMessage("Enter education id:");
            string educationIdStr = Console.ReadLine();
            int educationId;

            bool isCorrect = int.TryParse(educationIdStr, out educationId);
            if(!isCorrect)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongIdFormat);
                goto EducationId;
            }
            var groups = await _groupService.GetAllWithEducationIdAsync(educationId);
            foreach (var group in groups)
            {
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, group.Id, group.Name, group.Capacity, group.Education, group.CreatedDate));
            }
        }
    }
}
