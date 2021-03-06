using Library.Data;
using Library.Model;
using Library.Services.BookService;
using LibraryManagement.Services.MessageService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Services.UserService
{
    public class UserService : IUserService
    {
        private DataContext context = new DataContext();
        private MessageService messageService = new MessageService();

        public async Task<User> FindUserOrCreateNewAsync(string message)
        {
            string firstName = "";
            string lastName =  "";

            Console.WriteLine($"\nPlease enter a first name and last name of user for {message}");
            string name = Console.ReadLine();
            var tokens = name.Split(' ');
            firstName = tokens[0];
            if (tokens.Length > 1)
            {
                lastName = tokens[1];
            }
            var u = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
            if (u != null)
            { 
                return u;
            }
            else
            {
                return new User { FirstName = firstName, LastName = lastName };
            }
        }       

        public async Task<User> AddUserAsync()
        {
            try
            {
                var user = await FindUserOrCreateNewAsync("new User");
                if (user.Id == default)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    Console.WriteLine("\nUser " + user.FirstName + " " + user.LastName + " is added. ");
                }
                else
                {
                    Console.WriteLine("User already exist.");
                }
                messageService.PressAny();
                return user;                       
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await context.Users.ToListAsync();
                if (!users.Any())
                {
                    messageService.NotExist("Users");
                    messageService.PressAny();
                    return null;
                }
                else
                {
                    Console.WriteLine("\nYour library of users:\n");
                    users.ForEach(i => Console.WriteLine("{0}", i.UserName));
                    messageService.PressAny();
                    return users;
                }
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }           
        }       

        public async Task<User> GetUserAsync()
        {
            try
            {
                var userModel = await FindUserOrCreateNewAsync("search");

                if (userModel.Id == default)
                {
                    messageService.NotExist(userModel.UserName);
                    messageService.PressAny();
                    return null;
                }
                else
                {
                    Console.WriteLine("\nYour requested user: " + userModel.FirstName + " " + userModel.LastName);
                    messageService.PressAny();
                    return userModel;
                }                
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }
        }
    }
}
