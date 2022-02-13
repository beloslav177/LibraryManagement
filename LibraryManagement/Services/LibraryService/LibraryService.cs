using Library.Services.BookService;
using Library.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.LibraryService
{
    public class LibraryService : ILibraryService
    {
        private readonly IUserService userService;
        private readonly IBookService bookService;

        public LibraryService(IUserService userService, IBookService bookService)
        {
            this.userService = userService;
            this.bookService = bookService;
        }
        public Task<LibraryService> StartLibrary()
        {
            Console.WriteLine("\nMenu\n" +
                    "1)Add user\n" +
                    "2)Delete user\n" +
                    "3)Show users\n" +
                    "4)Show borrowed books for user\n" +
                    "5)Search user\n" +
                    "6)Add book\n" +
                    "7)Delete book\n" +
                    "8)Show all books\n" +
                    "9)Borrow book\n" +
                    "10)Return book\n" +
                    "Esc)Close\n\n");
            Console.Write("Choose your option from menu :");

            int option = int.Parse(Console.ReadLine());
            if (option == 1)
            {
                //userService.AddUser(string firstName, string lastName);
            }
            //else if (option == 6)
            //{
            //    Console.WriteLine("Thank you");
            //    break;
            //}
            else
            {
                Console.WriteLine("Invalid option\nRetry !!!");
            }
            return null;
        }

        public Task<LibraryService> StopLibrary()
        {
            throw new NotImplementedException();
        }
    }
}
