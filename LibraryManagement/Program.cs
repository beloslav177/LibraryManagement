using Library.Services.BookService;
using Library.Services.UserService;
using LibraryManagement.Services.MessageService;
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

        //public static BookService bookService = new BookService(userService);
        //public static UserService userService = new UserService(bookService);

        public static Menu menu = new Menu("Main Menu");
        public static Menu userMenu = new Menu("User Menu");
        public static Menu bookMenu = new Menu("Book Menu");
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        private readonly IBookService bookService;

        public Program(IMessageService messageService, IUserService userService, IBookService bookService)
        {
            this.userService = userService;
            this.bookService = bookService;
            this.messageService = messageService;
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((services) =>
                    services.AddTransient<Program>()
                            .AddTransient<UserService>()
                            .AddTransient<BookService>()
                            .AddTransient<MessageService>());
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
                                    await bookService.DeleteBookAsync();
                                    break;
                                case 3:
                                    await bookService.GetAllBooksAsync();
                                    break;
                                case 4:
                                    await bookService.BorrowBookAsync();
                                    break;
                                case 5:
                                    await bookService.ReturnBookAsync();
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
                                    await userService.DeleteUserAsync();
                                    break;
                                case 3:
                                    await userService.GetAllUsersAsync();
                                    break;
                                case 4:
                                    var userModel = await userService.FindUserOrCreateNewAsync("Booklist");
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
