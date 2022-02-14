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

        public static Menu menu = new Menu("MainMenu");
        public static Menu userMenu = new Menu("UserMenu");
        public static Menu bookMenu = new Menu("BookMenu");

        static void Main(string[] args)
        {
            menu.AddMenuItem(1, "Book Service");
            menu.AddMenuItem(2, "User Service");
            menu.AddMenuItem(6, "Exit");

            userMenu.AddMenuItem(1, "Add user");
            userMenu.AddMenuItem(2, "Delete user");
            userMenu.AddMenuItem(3, "Show users");
            userMenu.AddMenuItem(4, "Show borrowed books for user");
            userMenu.AddMenuItem(5, "Search user");
            userMenu.AddMenuItem(6, "Back");

            bookMenu.AddMenuItem(1, "Add book");
            bookMenu.AddMenuItem(2, "Delete book");
            bookMenu.AddMenuItem(3, "Show all books");
            bookMenu.AddMenuItem(4, "Borrow book");
            bookMenu.AddMenuItem(5, "Return book");
            bookMenu.AddMenuItem(6, "Back");           

            //TODO: no replication, borrow

            bool done = false;
            while (!done)
            {
                var execute = menu.Execute();
                switch (execute)
                {
                    case 1:
                        bool bookDone = false;
                        while (!bookDone)
                        {
                            var book = bookMenu.Execute();
                            switch (book)
                            {
                                case 1:
                                    bookservice.AddBook();
                                    break;
                                case 2:
                                    bookservice.DeleteBook();
                                    break;
                                case 3:
                                    bookservice.GetAllBooks();
                                    break;
                                case 4:
                                    bookservice.BorrowBook();
                                    break;
                                case 5:
                                    bookservice.ReturnBook();
                                    break;
                                case 6:
                                    bookDone = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        bool userDone = false;
                        while (!userDone)
                        {
                            var user = userMenu.Execute();
                            switch (user)
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
                                    userDone = true;
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case 6:
                        done = true;                        
                        break;
                    default:
                        break;
                }
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
