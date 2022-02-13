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
        private readonly DataContext context;
        private readonly Book book;

        public UserService(DataContext context, Book book)
        {
            this.context = context;
            this.book = book;
        }

        public async Task<User> AddUser(string firstName, string lastName)
        {
            try
            {
                Console.WriteLine("Please enter a first name and last name of user.");
                string name = Console.ReadLine();
                name = firstName + " " + lastName;
                var userName = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
                if (userName != null) Console.WriteLine("User is already exist.");
                var user = new User { FirstName = firstName, LastName = lastName };
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("User " + firstName + " " + lastName + " is added. ");
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
                return null;
            }            
        }

        public async Task<User> DeleteUser(string firstName, string lastName)
        {
            try
            {
                Console.WriteLine("Please enter a first name and last name of user you want to delete.");
                string name = Console.ReadLine();
                name = firstName + " " + lastName;
                var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
                if (user == null)
                {
                    Console.WriteLine("The user is does'nt exist in Library.");
                    return null;
                }
                if (user.IsBorrowing == true)
                {
                    Console.WriteLine("This user borrowing a book, so it's not possible delete user.");
                }
                context.Users.Remove(user);
                Console.WriteLine("You already removed an user with name " + firstName + " " + lastName);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
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
                Console.WriteLine("Exception thrown", ex);
                return null;
            }           
        }

        public async Task<Book> GetBorrowedBooks(string bookName, string firstName, string lastName)
        {
            Console.WriteLine("Please enter first name and last name you want to see the books have borrowed");
            string name = Console.ReadLine();
            name = firstName + " " + lastName;
            var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
            if (user == null)
            {
                Console.WriteLine("User is not exit in Library.");
            }
            var books = await context.Books.FirstOrDefaultAsync(b => b.UserNameOfBorrowed == firstName + " " + lastName);
            //foreach (var i in books)
            //{
            //    Console.WriteLine("Books of borrowed User:\n" + book.BookName);
            //}
            ///TODO: dorobit zobrazenie knih, kt. obsahuju meno a priezvisko.
            return null;
        }

        public async Task<User> GetUser(string firstName, string lastName)
        {
            try
            {
                Console.WriteLine("Please enter a first name and last name of user");
                string name = Console.ReadLine();
                name = firstName + " " + lastName;
                var user = await context.Users
                .FirstOrDefaultAsync(u => u.FirstName == firstName & u.LastName == lastName);
                if (user == null)
                {
                    Console.WriteLine("The user is does'nt exist in Library.");
                    return null;
                }
                Console.WriteLine("Your requested user: " + firstName + " " + lastName);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
                return null;
            }
        }
    }
}
