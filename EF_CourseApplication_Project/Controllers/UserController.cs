using Domain.Models;
using Service.Helpers.Constants;
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
    public class UserController
    {
        private readonly IUserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }
        public async void Register()
        {
            ConsoleColor.Cyan.ConsoleMessage("Add your full name:");
            string fullName = Console.ReadLine();

            ConsoleColor.Cyan.ConsoleMessage("Create username:");
            string username = Console.ReadLine();

            ConsoleColor.Cyan.ConsoleMessage("Create email:");
            string email = Console.ReadLine();

            ConsoleColor.Cyan.ConsoleMessage("Create password:");
            string password = Console.ReadLine();

            User user = new User { FullName = fullName, UserName = username, Email = email, Password = password };
            await _userService.RegisterAsync(user);
            await ConsoleColor.Green.ConsoleMessage(ResponseMessages.RegisterSuccess);
        }
        public async void Login()
        {
            ConsoleColor.Cyan.ConsoleMessage("Enter your email or username:");
            string usernameOrEmail = Console.ReadLine();

            ConsoleColor.Cyan.ConsoleMessage("Enter your password:");
            string password = Console.ReadLine();

            var result = await _userService.LoginAsync(usernameOrEmail, password);
            if (result)
            {
                await ConsoleColor.Green.ConsoleMessage(ResponseMessages.LoginSuccess);
            }
            else
            {
                await ConsoleColor.Red.ConsoleMessage("Login Failed");
            }
        }
    }
}
