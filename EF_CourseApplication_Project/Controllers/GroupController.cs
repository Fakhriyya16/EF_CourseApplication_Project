﻿using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using Group = Domain.Models.Group;

namespace EF_CourseApplication_Project.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        private readonly IEducationService _educationService;
        public GroupController()
        {
            _groupService = new GroupService();
            _educationService = new EducationService();
        }
        public async Task CreateAsync()
        {
            try
            {
            Name: ConsoleColor.Cyan.ConsoleMessage("Create name: ");
                string groupName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(groupName))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto Name;
                }

            GroupCapacity: ConsoleColor.Cyan.ConsoleMessage("Add capacity: ");
                string groupCapacityStr = Console.ReadLine();
                int groupCapacity;
                if (string.IsNullOrWhiteSpace(groupCapacityStr))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto GroupCapacity;
                }

                bool isCorrect = int.TryParse(groupCapacityStr, out groupCapacity);
                if (!isCorrect)
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                    goto GroupCapacity;
                }

            EducationId: ConsoleColor.Cyan.ConsoleMessage("Add education Id: ");
                ConsoleColor.Cyan.ConsoleMessage("Available educations: ");
                var availableEducations = await _educationService.GetAllAsync();
                if (availableEducations.Count == 0) throw new NotFoundException("Data was not found");

                foreach (var education in availableEducations)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color, education.CreatedDate));
                }
                string educationIdStr = Console.ReadLine();
                int educationId;

                isCorrect = int.TryParse(educationIdStr, out educationId);
                if (!isCorrect)
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                    goto EducationId;
                }
                var chosenEducation = await _educationService.GetByIdAsync(educationId);
                if (chosenEducation is null) throw new NotFoundException(ResponseMessages.NotFound);

                Group group = new Group { Name = groupName, Capacity = groupCapacity, EducationId = educationId };
                await _groupService.CreateAsync(group);
                await ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task DeleteAsync()
        {
        GroupId: ConsoleColor.Cyan.ConsoleMessage("Enter id of the group, you want to delete:");
            string groupIdStr = Console.ReadLine();
            int groupId;
            if (string.IsNullOrWhiteSpace(groupIdStr))
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                goto GroupId;
            }

            bool isCorrectFormat = int.TryParse(groupIdStr, out groupId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                goto GroupId;
            }

            await _groupService.DeleteAsync(groupId);
            await ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
        }
        public async Task GetAllAsync()
        {
            try
            {
                var groups = await _groupService.GetAllAsync();
                foreach (var group in groups)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.GroupDisplay, group.Id, group.Name, group.Capacity, group.Education.Name, group.CreatedDate));
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task GetByIdAsync()
        {
            try
            {
            GroupId: ConsoleColor.Cyan.ConsoleMessage("Add id of the education course:");
                string groupIdStr = Console.ReadLine();
                int groupId;
                if (string.IsNullOrWhiteSpace(groupIdStr))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto GroupId;
                }

                bool isCorrectFormat = int.TryParse(groupIdStr, out groupId);
                if (!isCorrectFormat)
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                    goto GroupId;
                }
                Group group = await _groupService.GetByIdAsync(groupId);
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.GroupDisplay, group.Id, group.Name, group.Capacity, group.Education.Name, group.CreatedDate));
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task SearchAsync()
        {
            try
            {
            SearchText: ConsoleColor.Cyan.ConsoleMessage("Add search text:");
                string searchText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto SearchText;
                }

                var groupsFound = await _groupService.SearchAsync(searchText);
                foreach (var group in groupsFound)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.GroupDTO, group.Name, group.Capacity));
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task SortByCapacity()
        {
            try
            {
            OrderNumber: ConsoleColor.Cyan.ConsoleMessage("Choose the sorting format:\nType 1 for ascending order\nType 2 for descending order");
                string orderNumStr = Console.ReadLine();
                int orderNum;
                if (string.IsNullOrWhiteSpace(orderNumStr))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto OrderNumber;
                }


                bool isCorrectFormat = int.TryParse(orderNumStr, out orderNum);
                if (isCorrectFormat)
                {
                    if (orderNum != 1 || orderNum != 2)
                    {
                        ConsoleColor.Red.ConsoleMessage("Please choose again:");
                        goto OrderNumber;
                    }
                }
                else
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                    goto OrderNumber;
                }

                var groupsSorted = await _groupService.SortWithCapacityAsync(orderNum);
                foreach (var group in groupsSorted)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.GroupDTO, group.Name, group.Capacity));
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task UpdateAsync()
        {

        GroupId: ConsoleColor.Cyan.ConsoleMessage("Enter id of the group, you want to update:");
            string groupIdStr = Console.ReadLine();
            int groupId;
            if (string.IsNullOrWhiteSpace(groupIdStr))
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                goto GroupId;
            }

            bool isCorrectFormat = int.TryParse(groupIdStr, out groupId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                goto GroupId;
            }
            try
            {
                Group groupFound = await _groupService.UpdateAsync(groupId);

                ConsoleColor.Cyan.ConsoleMessage("Update name:");
                string groupNewName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(groupNewName))
                {
                    groupFound.Name = _groupService.UpdateAsync(groupId).Result.Name;
                }
                else
                {
                    groupFound.Name = groupNewName;
                }

            GroupCapacity: ConsoleColor.Cyan.ConsoleMessage("Update capacity: ");
                string groupCapacityStr = Console.ReadLine();
                int groupCapacity;

                if (string.IsNullOrWhiteSpace(groupCapacityStr))
                {
                    groupFound.Capacity = _groupService.UpdateAsync(groupId).Result.Capacity;
                }
                else
                {
                    isCorrectFormat = int.TryParse(groupCapacityStr, out groupCapacity);
                    if (!isCorrectFormat)
                    {
                        await ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                        goto GroupCapacity;
                    }
                    else
                    {
                        groupFound.Capacity = groupCapacity;
                    }
                }

            EducationId: ConsoleColor.Cyan.ConsoleMessage("Update education Id: ");
                ConsoleColor.Cyan.ConsoleMessage("Available Educations: ");
                var availableEducations = await _educationService.GetAllAsync();
                if (availableEducations.Count == 0) throw new NotFoundException("Data was not found");

                foreach (var education in availableEducations)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color, education.CreatedDate));
                }

                string groupEducationIdStr = Console.ReadLine();
                int groupEducationId;
                if (string.IsNullOrWhiteSpace(groupEducationIdStr))
                {
                    groupFound.EducationId = _groupService.UpdateAsync(groupId).Result.EducationId;
                }
                else
                {
                    isCorrectFormat = int.TryParse(groupEducationIdStr, out groupEducationId);
                    if (!isCorrectFormat)
                    {
                        await ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                        goto EducationId;
                    }
                    else
                    {
                        var chosenEducation = await _educationService.UpdateAsync(groupEducationId);
                        if (chosenEducation is null) throw new NotFoundException(ResponseMessages.NotFound);
                        groupFound.EducationId = chosenEducation.Id;
                        groupFound.Education.Name = chosenEducation.Name;
                        
                    }
                    await ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task FilterByEducationName()
        {
            try
            {
            EducationName: ConsoleColor.Cyan.ConsoleMessage("Enter education name:");
                string educationName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(educationName))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto EducationName;
                }

                var groups = await _groupService.FilterByEducationNameAsync(educationName);
                foreach (var group in groups)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.GroupDTO, group.Name, group.Capacity));
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task GetAllByEducationId()
        {
            try
            {
            EducationId: ConsoleColor.Cyan.ConsoleMessage("Enter education id:");
                string educationIdStr = Console.ReadLine();
                int educationId;
                if (string.IsNullOrWhiteSpace(educationIdStr))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto EducationId;
                }

                bool isCorrect = int.TryParse(educationIdStr, out educationId);
                if (!isCorrect)
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                    goto EducationId;
                }
                var groups = await _groupService.GetAllWithEducationIdAsync(educationId);
                foreach (var group in groups)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.GroupDTO, group.Name, group.Capacity));
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
