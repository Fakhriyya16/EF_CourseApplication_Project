using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public async Task Register()
        {
            try
            {
            Name: ConsoleColor.Cyan.ConsoleMessage("Add your full name:");
                string fullName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto Name;
                }
                string namePattern = @"^[A-Z][a-zA-Z'-]*(?: [a-zA-Z'-]+)*$";

                if (!Regex.IsMatch(fullName, namePattern))
                {
                    ConsoleColor.Red.ConsoleMessage("Invalid Full Name.");
                    goto Name;
                }

            UserName: ConsoleColor.Cyan.ConsoleMessage("Create username:");
                string username = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(username))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto UserName;
                }

            Email: ConsoleColor.Cyan.ConsoleMessage("Create email:");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto Email;
                }
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                // Check if the email matches the pattern
                if (!Regex.IsMatch(email, emailPattern))
                {
                    ConsoleColor.Red.ConsoleMessage("Invalid email address.");
                    goto Email;
                }


            Password: ConsoleColor.Cyan.ConsoleMessage("Create password:");
                string password = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(password))
                {
                    ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                    goto Password;
                }
                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

                if (!Regex.IsMatch(password, passwordPattern))
                {
                    ConsoleColor.Red.ConsoleMessage("Invalid password.");
                    goto Password;
                }

                User user = new User { FullName = fullName, UserName = username, Email = email, Password = password };
                await _userService.RegisterAsync(user);
                await ConsoleColor.Green.ConsoleMessage(ResponseMessages.RegisterSuccess);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task Login()
        {
        Email: ConsoleColor.Cyan.ConsoleMessage("Enter your email or username:");
            string usernameOrEmail = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(usernameOrEmail))
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                goto Email;
            }

        Password: ConsoleColor.Cyan.ConsoleMessage("Enter your password:");
            string password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                ConsoleColor.Red.ConsoleMessage(ResponseMessages.EmptyInput);
                goto Password;
            }

            var result = await _userService.LoginAsync(usernameOrEmail, password);
            if (result)
            {
                await ConsoleColor.Green.ConsoleMessage(ResponseMessages.LoginSuccess);
            }
            else
            {
                throw new LoginFailedException("Login Failed");
            }
        }
    }
}
