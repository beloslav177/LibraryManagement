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
            userService.Close = true;
            bookservice.Close = true;
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
            while (userService.Close == true || bookservice.Close == true)
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
                
                string option = Console.ReadLine();
                if (option == "1")
                {
                    userService.AddUser();
                }
                else if (option == "2")
                {
                    userService.DeleteUser();
                }
                else if (option == "3")
                {
                    userService.GetAllUsers();
                }
                else if (option == "4")
                {
                    userService.GetBorrowedBooks();
                }
                else if (option == "5")
                {
                    userService.GetUser();
                }
                else if (option == "6")
                {
                    bookservice.AddBook();
                }
                else if (option == "7")
                {
                    bookservice.DeleteBook();
                }
                else if (option == "8")
                {
                    bookservice.GetAllBooks();
                }
                else if (option == "9")
                {
                    bookservice.BorrowBook();
                }
                else if (option == "10")
                {
                    bookservice.ReturnBook();
                }
                else if (option == "11")
                {
                    Environment.Exit(0);
                }         
                else
                {
                    Console.WriteLine("Invalid option\nRetry !!!");
                }
            }
        }
    }
}
