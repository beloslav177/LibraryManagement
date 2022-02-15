using Library.Data;
using Library.Model;
using Library.Services.BookService;
using Library.Services.UserService;
using System;

namespace LibraryManagement
{
    internal class Program
    {
        public static BookService bookService = new BookService(userService);
        public static UserService userService = new UserService(bookService);

        public static Menu menu = new Menu("Main Menu");
        public static Menu userMenu = new Menu("User Menu");
        public static Menu bookMenu = new Menu("Book Menu");

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
                                    bookService.AddBook();
                                    break;
                                case 2:
                                    bookService.DeleteBook();
                                    break;
                                case 3:
                                    bookService.GetAllBooks();
                                    break;
                                case 4:
                                    bookService.BorrowBook();
                                    break;
                                case 5:
                                    bookService.ReturnBook();
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
                                    bookService.GetBorrowedBooks();
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
    }
}
