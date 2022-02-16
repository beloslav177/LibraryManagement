using Library.Data;
using Library.Model;
using Library.Services.UserService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public class BookService : IBookService
    {
        private DataContext context = new DataContext();

        public string authorFirstName;
        public string authorLastName;
        public string bookName;
        public Book bookModel;
        public User userModel;

        public IUserService UserService { get; }

        public BookService(IUserService userService)
        {
            UserService = userService;
        }

        public async void AddAuthorOfBook()
        {
            string authorName = Console.ReadLine();
            authorFirstName = authorName.Split(' ')[0];
            authorLastName = authorName.Split(' ')[1];
            authorName = authorFirstName + " " + authorLastName;
            if (authorFirstName == null) authorFirstName = "";
            if (authorLastName == null) authorLastName = "";
        }

        public async Task<Book> AddBook()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Please enter a first name and last name of author a book.");

                AddAuthorOfBook();
                Console.WriteLine("Please enter a name of book.");

                bookName = Console.ReadLine();
                bookModel = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);

                if (bookModel.BookName != null)
                {
                    UserService.Exist();
                    return null;                    
                }
                else
                {
                    var book = new Book { AuthorFirstName = authorFirstName, AuthorLastName = authorLastName, BookName = bookName };
                    context.Books.Add(bookModel);
                    context.SaveChanges();
                    Console.WriteLine("Book " + bookModel.BookName + " is added by " + authorFirstName + authorLastName);
                    return book;
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }            
        }

        public async Task<Book> BorrowBook()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Please enter a name of Book you want to borrow.");
                bookName = Console.ReadLine();
                bookModel = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (bookModel.BookName == null)
                {
                    UserService.NotExist();
                    return null;
                }
                if (bookModel.IsBorrowed == true)
                {
                    UserService.IsBorrowing();
                    return null;
                }
                else
                {
                    Console.WriteLine("\nYou want a borrow book of name " + bookModel.BookName);
                    Console.WriteLine("\nPlease write a first name and last name of the person to whom you want to borrow the book");
                    UserService.FindUser();
                    userModel = await context.Users.FirstOrDefaultAsync(u => u.FirstName == userModel.FirstName && u.LastName == userModel.LastName);

                    if (userModel != null)
                    {
                        userModel.IsBorrowing = true;
                        bookModel.IsBorrowed = true;
                        context.SaveChanges();
                        Console.WriteLine(userModel.FirstName + " " + userModel.LastName + " is borrowing " + bookModel.BookName);
                        return bookModel;
                    }
                    return null;
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }            
        }

        public async Task<Book> DeleteBook()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\nPlease enter a name of the book you want to delete.");

                bookName = Console.ReadLine();
                bookModel = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (bookModel.BookName == null)
                {
                    UserService.NotExist();
                    return null;
                }

                if (bookModel.IsBorrowed == true)
                {
                    UserService.IsBorrowing();
                    return null;
                }
                else
                {
                    Console.Clear();
                    context.Books.Remove(bookModel);
                    context.SaveChanges();
                    Console.WriteLine("\nYou already removed a book with name " + bookModel.BookName);
                    return bookModel;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }            
        }

        public async Task<List<Book>> GetAllBooks()
        {
            try
            {
                Console.Clear();
                var books = await context.Books.ToListAsync();
                Console.WriteLine("Your library:\n");
                books.ForEach(i => Console.Write("{0}", i.BookName));
                return books;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }            
        }

        public async Task<Book> GetBook()
        {
            try
            {
                Console.WriteLine("\nPlease enter a name of the book you want to see.");

                bookName = Console.ReadLine();
                bookModel = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);

                if (bookModel.BookName == null)
                {
                    UserService.NotExist();
                    return null;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nYour book " + bookModel.BookName);
                    return bookModel;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }
        }

        public async Task<Book> ReturnBook()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\nPlease enter a first name and last name which want a return book.");

                UserService.FindUser();
                userModel = await context.Users.FirstOrDefaultAsync(u => u.FirstName == userModel.FirstName && u.LastName == userModel.LastName);

                if (userModel == null)
                {
                    UserService.NotExist();
                    return null;
                }

                Console.WriteLine("\nPlease enter a name of book you want a return.");

                bookName = Console.ReadLine();
                bookModel = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (bookModel.BookName == null)
                {
                    UserService.NotExist();
                    return null;
                }
                else if (bookModel.IsBorrowed == false)
                {
                    UserService.IsNotBorrowing();
                    return null;
                }
                else
                {
                    userModel.IsBorrowing = false;
                    bookModel.IsBorrowed = false;
                    context.SaveChanges();

                    Console.WriteLine("\nThe book " + bookName + " is returned");
                    return bookModel;
                }                    
                              
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }
        }

        public async Task<List<string>> GetBorrowedBooks()
        {
            try
            {
                int count = 0;
                Console.WriteLine("\nPlease enter first name and last name you want to see the books have borrowed");

                UserService.FindUser();
                userModel = await context.Users.FirstOrDefaultAsync(u => u.FirstName == userModel.FirstName && u.LastName == userModel.LastName);

                if (userModel == null)
                {
                    UserService.NotExist();
                    return null;
                }

                bookName = Console.ReadLine();
                bookModel = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (bookModel.BookName == null)
                {
                    UserService.NotExist();
                    return null;
                }

                if (userModel != null)
                {
                    var books = await context.Books.Include(u => u.User).Where(b => b.IsBorrowed).Select(b => b.BookName).ToListAsync();
                    count = 1;

                    if (books == null)
                    {
                        UserService.IsNotBorrowing();
                        return null;
                    }
                    foreach (var book in books)
                    {
                        Console.WriteLine("\nBooks of borrowed User:\n" + count + ".) " + book + "\n");
                        count++;
                    }
                    return books;
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
