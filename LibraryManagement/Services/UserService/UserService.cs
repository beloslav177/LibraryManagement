using Library.Data;
using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.UserService
{
    public class UserService : IUserService
    {
        private DataContext context = new DataContext();

        public async Task<User> AddUser()
        {
            try
            {
                string firstName;
                string lastName;
                Console.WriteLine("Please enter a first name and last name of user.");

                string name = Console.ReadLine();
                firstName = name.Split(' ')[0];
                lastName = name.Split(' ')[1];
                name = firstName + " " + lastName;
                if (firstName == null) firstName = "";
                if (lastName == null) lastName = "";
                var userName = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);

                if (userName != null)
                {
                    Console.WriteLine("User is already exist.");
                    return null;
                }
                else
                {
                    var user = new User { FirstName = firstName, LastName = lastName };
                    context.Users.Add(user);
                    context.SaveChanges();
                    Console.WriteLine("User " + firstName + " " + lastName + " is added. ");
                    return user;
                }   
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
                string firstName;
                string lastName;
                Console.WriteLine("Please enter a first name and last name of user you want to delete.");

                string name = Console.ReadLine();
                firstName = name.Split(' ')[0];
                lastName = name.Split(' ')[1];
                name = firstName + " " + lastName;
                if (firstName == null) firstName = "";
                if (lastName == null) lastName = "";
                var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);

                if (user == null)
                {
                    Console.WriteLine("The user is does'nt exist in Library.");
                    return null;
                }

                else if (user.IsBorrowing == true)
                {
                    Console.WriteLine("This user borrowing a book, so it's not possible delete user.");
                    return null;
                }
                else
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    Console.WriteLine("You already removed an user with name " + firstName + " " + lastName);
                    return user;
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
                Console.WriteLine("Your library of users:\n");
                users.ForEach(i => Console.WriteLine("{0}\n", i.FirstName + " " + i.LastName));
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }           
        }

        public async Task<User> GetBorrowedBooks()
        {
            try
            {
                string firstName;
                string lastName;
                int count = 0;
                Console.WriteLine("Please enter first name and last name you want to see the books have borrowed");

                string name = Console.ReadLine();
                firstName = name.Split(' ')[0];
                lastName = name.Split(' ')[1];
                name = firstName + " " + lastName;
                if (firstName == null) firstName = "";
                if (lastName == null) lastName = "";
                var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);

                if (user == null)
                {
                    Console.WriteLine("User is not exit in Library.");
                    return null;
                }
                else
                {
                    var books = await context.Books
                    .Where(b => b.UserNameOfBorrowed == name)
                    .Select(b => b.BookName).ToListAsync();
                    count = 1;

                    foreach (var book in books)
                    {
                        Console.WriteLine("Books of borrowed User:\n" + count + ".) " + book + "\n");
                        count++;
                    }
                    Console.ReadLine();
                    return user;
                }                
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
                string firstName;
                string lastName;
                Console.WriteLine("Please enter a first name and last name of user");

                string name = Console.ReadLine();
                firstName = name.Split(' ')[0];
                lastName = name.Split(' ')[1];
                name = firstName + " " + lastName;
                if (firstName == null) firstName = "";
                if (lastName == null) lastName = "";
                var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName & u.LastName == lastName);

                if (user == null)
                {
                    Console.WriteLine("The user is does'nt exist in Library.");
                    return null;
                }
                else
                {
                    Console.WriteLine("Your requested user: " + firstName + " " + lastName);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }
        }
    }
}
