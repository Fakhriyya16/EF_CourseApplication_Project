using ConsoleTables;
using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;


namespace EF_CourseApplication_Project.Controllers
{
    public class EducationController
    {
        private readonly IEducationService _educationService;
        public EducationController()
        {
            _educationService = new EducationService();
        }
        public async Task CreateAsync()
        {
            ConsoleColor.Cyan.ConsoleMessage("Create name: ");
            string educationName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(educationName))
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
            }

            ConsoleColor.Cyan.ConsoleMessage("Add color: ");
            string educationColor = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(educationColor))
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
            }

            Education newEducation = new Education { Name = educationName, Color = educationColor };
            await _educationService.CreateAsync(newEducation);

            await ConsoleColor.Green.ConsoleMessage(ResponseMessages.SuccessfullOperation);
        }

        public async Task DeleteAsync()
        {
        EducationId: ConsoleColor.Cyan.ConsoleMessage("Add id of the education course,you want to delete:");
            string educationIdStr = Console.ReadLine();
            int educationId;
            if (string.IsNullOrWhiteSpace(educationIdStr))
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                goto EducationId;
            }

            bool isCorrectFormat = int.TryParse(educationIdStr, out educationId);
            if (!isCorrectFormat)
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                goto EducationId;
            }

            await _educationService.DeleteAsync(educationId);
        }
        public async Task GetAllAsync()
        {
            try
            {
                var educations = await _educationService.GetAllAsync();
                if (educations.Count == 0) throw new NotFoundException("Data was not found");

                foreach (var education in educations)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color, education.CreatedDate));
                }
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }
        }

        public async Task GetAllWithGroups()
        {
            var result = await _educationService.GetAllWithGroupsAsync();
            foreach (var item in result)
            {
                string resultStr = item.Education + " - " + "Groups: " + string.Join(", ", item.Groups);
                ConsoleColor.Yellow.ConsoleMessage(resultStr);
            }
        }

        public async Task GetByIdAsync()
        {
            try
            {
            EducationId: ConsoleColor.Cyan.ConsoleMessage("Add id of the education:");
                string educationIdStr = Console.ReadLine();
                int educationId;
                if (string.IsNullOrWhiteSpace(educationIdStr))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto EducationId;
                }

                bool isCorrectFormat = int.TryParse(educationIdStr, out educationId);
                if (!isCorrectFormat)
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                    goto EducationId;
                }
                Education education = await _educationService.GetByIdAsync(educationId);
                if (education is null) throw new NotFoundException("Data was not found");
                ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color, education.CreatedDate));
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }
        }
        public async Task SearchAsync()
        {
            try
            {
            Search: ConsoleColor.Cyan.ConsoleMessage("Add search text:");
                string searchText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto Search;
                }

                var educationsFound = await _educationService.SearchAsync(searchText);
                if (educationsFound.Count == 0) throw new NotFoundException("Data was not found");
                foreach (var education in educationsFound)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDTO,education.Name,education.Color));
                }
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }
        }
        public async Task SortWithCreatedDate()
        {
            try
            {
            OrderNumber: ConsoleColor.Cyan.ConsoleMessage("Choose the sorting format:\nType 1 for ascending order\nType 2 for descending order");
                string orderNumStr = Console.ReadLine();
                int orderNum;
                if(string.IsNullOrWhiteSpace(orderNumStr))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto OrderNumber;
                }

                bool isCorrectFormat = int.TryParse(orderNumStr, out orderNum);
                if (isCorrectFormat)
                {
                    if (orderNum != 1 && orderNum != 2)
                    {
                        ConsoleColor.Red.ConsoleMessage("Please choose again");
                        goto OrderNumber;
                    }
                }
                else
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                    goto OrderNumber;
                }

                var educationsSorted = await _educationService.SortWithCreatedDate(orderNum);
                foreach (var education in educationsSorted)
                {
                    ConsoleColor.Yellow.ConsoleMessage(string.Format(ResponseMessages.EducationDisplay, education.Id, education.Name, education.Color, education.CreatedDate));
                }
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }
        }
        public async Task UpdateAsync()
        {
        EducationId: ConsoleColor.Cyan.ConsoleMessage("Enter id of the education course, you want to update:");
            string educationIdStr = Console.ReadLine();
            int educationId;
            if (string.IsNullOrWhiteSpace(educationIdStr))
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                goto EducationId;
            }

            bool isCorrectFormat = int.TryParse(educationIdStr, out educationId);
            if (!isCorrectFormat)
            {
                await ConsoleColor.Red.ConsoleMessage(ResponseMessages.WrongFormat);
                goto EducationId;
            }
            try
            {
                Education educationFound = await _educationService.GetByIdAsync(educationId);

                ConsoleColor.Cyan.ConsoleMessage("Update name:");
                string educationNewName = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(educationNewName))
                {
                    educationFound.Name = educationNewName;
                }
                
                ConsoleColor.Cyan.ConsoleMessage("Update color: ");
                string educationNewColor = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(educationNewColor))
                {
                    educationFound.Color = educationNewColor;
                }

                _educationService.UpdateAsync(educationFound);
            }
            catch (Exception ex)
            {
                await ConsoleColor.Red.ConsoleMessage(ex.Message);
            }
        }
    }
}
