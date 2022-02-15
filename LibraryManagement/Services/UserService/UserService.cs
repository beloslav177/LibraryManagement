using Library.Data;
using Library.Model;
using Library.Services.BookService;
using LibraryManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Services.UserService
{
    public class UserService : IUserService
    {
        private DataContext context = new DataContext();

        private string firstName;
        private string lastName;
        private User userModel;

        public IBookService BookService { get; }

        public UserService(IBookService bookService)
        {
            BookService = bookService;
        }

        public async void FindUser()
        {
            string name = Console.ReadLine();
            if (int.TryParse(name, out int id))
            {
                Console.WriteLine("Wrong choose.");
                Thread.Sleep(2000);
            }
            else
            {
                firstName = name.Split(' ')[0];
                lastName = name.Split(' ')[1];
                name = firstName + " " + lastName;
                if (firstName == null) firstName = " ";
                if (lastName == null) lastName = " ";
                userModel = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
            }                       
        }

        public async void FindUserChecked()
        {
            FindUser();
            if (userModel == null)
            {
                Console.WriteLine("\nThe user is does'nt exist in Library.");
                BookService.PressEnter();
            }
        }

        public async Task<User> AddUser()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\nPlease enter a first name and last name of user.");

                FindUser();
                if (userModel != null)
                {
                    Console.WriteLine("\nUser is already exist.");
                    BookService.PressEnter();
                    return null;
                }
                var user = new User { FirstName = firstName, LastName = lastName };
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("\nUser " + firstName + " " + lastName + " is added. ");
                BookService.PressEnter();
                return user;                
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }
        }

        public async Task<User> DeleteUser()
        {
            try
            {
                Console.WriteLine("\nPlease enter a first name and last name of user you want to delete.");

                FindUser();

                if (userModel == null)
                {
                    Console.WriteLine("\nThe user is does'nt exist in Library.");
                    BookService.PressEnter();
                    return null;
                }
                else if (userModel.IsBorrowing == true)
                {
                    Console.WriteLine("\nThis user borrowing a book, so it's not possible delete user.");
                    BookService.PressEnter();
                    return null;
                }
                else
                {
                    context.Users.Remove(userModel);
                    context.SaveChanges();
                    Console.WriteLine("\nYou already removed an user with name " + firstName + " " + lastName);
                    BookService.PressEnter();
                    return userModel;
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }            
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var users = await context.Users.ToListAsync();
                Console.WriteLine("\nYour library of users:\n");
                users.ForEach(i => Console.WriteLine("{0}\n", i.FirstName + " " + i.LastName));
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }           
        }       

        public async Task<User> GetUser()
        {
            try
            {
                Console.WriteLine("\nPlease enter a first name and last name of user");

                FindUserChecked();

                if (userModel != null)
                {
                    Console.WriteLine("\nYour requested user: " + firstName + " " + lastName);
                    BookService.PressEnter();
                    return userModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }
        }        
    }
}
