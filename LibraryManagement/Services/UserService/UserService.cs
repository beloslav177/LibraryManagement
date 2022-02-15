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

        public string firstName;
        public string lastName;
        public User userModel;

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
            }                       
        }

        public async Task<User> AddUser()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\nPlease enter a first name and last name of user.");

                FindUser();

                userModel = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);

                if (userModel != null)
                {
                    Console.Clear();
                    Console.WriteLine("\nUser is already exist.");
                    return null;
                }
                var user = new User { FirstName = firstName, LastName = lastName };
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("\nUser " + firstName + " " + lastName + " is added. ");
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
                Console.Clear();
                Console.WriteLine("\nPlease enter a first name and last name of user you want to delete.");

                FindUser();

                userModel = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);

                if (userModel == null)
                {
                    Console.Clear();
                    Console.WriteLine("\nThe user is does'nt exist in Library.");
                    return null;
                }
                else if (userModel.IsBorrowing == true)
                {
                    Console.Clear();
                    Console.WriteLine("\nThis user borrowing a book, so it's not possible delete user.");
                    return null;
                }
                else
                {
                    Console.Clear();
                    context.Users.Remove(userModel);
                    context.SaveChanges();
                    Console.WriteLine("\nYou already removed an user with name " + firstName + " " + lastName);
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
                Console.Clear();
                var users = await context.Users.ToListAsync();
                Console.WriteLine("\nYour library of users:\n");
                users.ForEach(i => Console.WriteLine("{0}", i.FirstName + " " + i.LastName));
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

                FindUser();

                userModel = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);

                if (userModel == null)
                {
                    Console.Clear();
                    Console.WriteLine("\nThe user is does'nt exist in Library.");
                    return null;
                }
                if (userModel != null)
                {
                    Console.Clear();
                    Console.WriteLine("\nYour requested user: " + firstName + " " + lastName);
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
