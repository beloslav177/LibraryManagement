using Library.Data;
using Library.Model;
using Library.Services.BookService;
using Library.Services.UserService;
using System;

namespace LibraryManagement
{
    internal class Program
    {
        public static UserService userService = new UserService();
        public static BookService bookservice = new BookService();

        static void Main(string[] args)
        {
            Console.WriteLine("For start a library press 1 or 2 for close the application.");
            string option = (Console.ReadLine());
            if (option == "1")
            {
                StartLibrary();
            }
            else if (option == "2")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid option\nRetry !!!");
            }           
        }
        public static void StartLibrary()
        {
            while (true)
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
               "11)End of application\n");
                Console.Write("Choose your option from menu :");

                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        userService.AddUser();
                        break;
                    case 2:
                        userService.DeleteUser();
                        break;
                    case 3:
                        userService.GetAllUsers();
                        break;
                    case 4:
                        userService.GetBorrowedBooks();
                        break;
                    case 5:
                        userService.GetUser();
                        break;
                    case 6:
                        bookservice.AddBook();
                        break;
                    case 7:
                        bookservice.DeleteBook();
                        break;
                    case 8:
                        bookservice.GetAllBooks();
                        break;
                    case 9:
                        bookservice.BorrowBook();
                        break;
                    case 10:
                        bookservice.ReturnBook();
                        break;
                    case 11:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("#################################");
                        break;
                }
            }              
        }
    }
}
