using Library.Data;
using Library.Services.BookService;
using Library.Services.UserService;
using LibraryManagement.Services.MessageService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Program
    {
        //TODO: dependency injection, prekablovanie servis

        private DataContext context = new DataContext();
        public static Menu menu = new Menu("Main Menu");
        public static Menu userMenu = new Menu("User Menu");
        public static Menu bookMenu = new Menu("Book Menu");
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        private readonly IBookService bookService;

        public Program(IMessageService messageService, IUserService userService, IBookService bookService)
        {
            this.messageService = messageService;
            this.userService = userService;
            this.bookService = bookService;
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<Program>();
                    services.AddTransient<IUserService, UserService>();
                    services.AddTransient<IBookService, BookService>();
                    services.AddTransient<IMessageService, MessageService>();
                });
        }

        public async Task Run()
        {
            menu.AddMenuItem(1, "Book Service");
            menu.AddMenuItem(2, "User Service");
            menu.AddMenuItem(7, "Exit");

            userMenu.AddMenuItem(1, "Add user");
            userMenu.AddMenuItem(2, "Delete user");
            userMenu.AddMenuItem(3, "Show users");
            userMenu.AddMenuItem(4, "Show borrowed books for user");
            userMenu.AddMenuItem(5, "Search user");
            userMenu.AddMenuItem(7, "Back");

            bookMenu.AddMenuItem(1, "Add book");
            bookMenu.AddMenuItem(2, "Delete book");
            bookMenu.AddMenuItem(3, "Show all books");
            bookMenu.AddMenuItem(4, "Borrow book");
            bookMenu.AddMenuItem(5, "Return book");
            bookMenu.AddMenuItem(6, "Search book");
            bookMenu.AddMenuItem(7, "Back");

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
                                    await bookService.AddBookAsync();
                                    break;
                                case 2:
                                    var bookModel = await bookService.FindBookOrCreateNewAsync("delete Book");
                                    var userModel = await context.Users.FirstOrDefaultAsync();

                                    if (bookModel.Id == default)
                                    {
                                        messageService.NotExist(bookModel.BookName);
                                    }
                                    else
                                    {
                                        if ((await bookService.GetBorrowedBooksAsync(userModel)).Count == 0)
                                        {
                                            context.Books.Remove(bookModel);
                                            context.SaveChanges();
                                            Console.WriteLine("\nYou already removed book with name " + bookModel.BookName + " from " + bookModel.AuthorName);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"\n {bookModel.BookName} book is'nt possible to delete, cause is already borrowed.");
                                        }
                                    }
                                    messageService.PressAny();
                                    break;
                                case 3:
                                    await bookService.GetAllBooksAsync();
                                    break;
                                case 4:
                                    bookModel = await bookService.BorrowBookAsync();
                                    userModel = await userService.FindUserOrCreateNewAsync("borrow the book");

                                    if (userModel == null)
                                    {
                                        bookModel.User = userModel;
                                        context.SaveChanges();
                                        Console.WriteLine(userModel.UserName + " is borrowing " + bookModel.BookName);
                                        messageService.PressAny();
                                    }
                                    else
                                    {
                                        messageService.NotExist(bookModel.BookName);
                                        messageService.PressAny();
                                    }
                                    break;
                                case 5:
                                    userModel = await userService.FindUserOrCreateNewAsync("return the Book");
                                    if (userModel.Id != default)
                                    {
                                        await bookService.ReturnBookAsync();                                        
                                    }
                                    else
                                    {
                                        messageService.NotExist(userModel.UserName);
                                        messageService.PressAny();
                                    }
                                    break;
                                case 6:
                                    await bookService.GetBookAsync();
                                    break;
                                case 7:
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
                                    await userService.AddUserAsync();
                                    break;
                                case 2:
                                    var userModel = await userService.FindUserOrCreateNewAsync("delete User");

                                    if (userModel.Id == default)
                                    {
                                        messageService.NotExist(userModel.UserName);
                                    }
                                    else
                                    {
                                        if ((await bookService.GetBorrowedBooksAsync(userModel)).Count == 0)
                                        {
                                            context.Users.Remove(userModel);
                                            context.SaveChanges();
                                            Console.WriteLine("\nYou already removed an user with name " + userModel.FirstName + " " + userModel.LastName);
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nUser is not possible to delete, cause is already borrowing a book.");
                                        }
                                    }
                                    messageService.PressAny();
                                    break;
                                case 3:
                                    await userService.GetAllUsersAsync();
                                    break;
                                case 4:
                                    userModel = await userService.FindUserOrCreateNewAsync("Booklist");
                                    var bookList = await bookService.GetBorrowedBooksAsync(userModel);
                                    if (!bookList.Any())
                                    {
                                        messageService.NotExist("Books for " + userModel.UserName);
                                        messageService.PressAny();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nBorrowed Books of " + userModel.UserName + ": \n");
                                        bookList.ForEach(i => Console.Write("{0}", i.BookName));
                                        messageService.PressAny();
                                    }
                                    break;
                                case 5:
                                    await userService.GetUserAsync();
                                    break;
                                case 7:
                                    userDone = true;
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case 7:
                        done = true;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build(); 
            _ = host.Services.GetRequiredService<Program>().Run();
        }
    }
}
