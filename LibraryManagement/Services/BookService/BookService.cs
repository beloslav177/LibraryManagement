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

        public void PressEnter()
        {
            Console.WriteLine("\n Press Enter to go back.");
            Console.ReadLine();
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
                    Console.Clear();
                    Console.WriteLine("Book " + bookModel.BookName + " is already exist.");
                    return null;                    
                }
                else
                {
                    bookModel = new Book { AuthorFirstName = authorFirstName, AuthorLastName = authorLastName, BookName = bookName };
                    context.Books.Add(bookModel);
                    context.SaveChanges();
                    Console.WriteLine("Book " + bookModel.BookName + " is added by " + authorFirstName + authorLastName);
                    return bookModel;
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
                    Console.WriteLine("\nThe book" + bookModel.BookName + " is does'nt exist in Library.");
                    PressEnter();
                }
                if (bookModel.IsBorrowed == true)
                {
                    Console.WriteLine("Book is already taken, it's not possible to borrow once again.");
                    PressEnter();
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
                    Console.Clear();
                    Console.WriteLine("\nThe book" + bookModel.BookName + " is does'nt exist in Library.");
                    return null;
                }

                if (bookModel.IsBorrowed == true)
                {
                    Console.Clear();
                    Console.WriteLine("\nThis book" + bookModel.BookName + " is borrowed, so it's not possible to delete.");
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
                    Console.Clear();
                    Console.WriteLine("\nThe book" + bookModel.BookName + " is does'nt exist in Library.");
                    PressEnter();
                }

                if (bookModel != null)
                {
                    Console.Clear();
                    Console.WriteLine("\nYour book " + bookModel.BookName);
                    return bookModel;
                }
                return null;
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
                    Console.WriteLine("\nThe user is does'nt exist in Library.");
                    PressEnter();
                    return null;
                }

                Console.WriteLine("\nPlease enter a name of book you want a return.");

                bookName = Console.ReadLine();
                bookModel = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (bookModel.BookName == null)
                {
                    Console.WriteLine("\nThe book" + bookModel.BookName + " is does'nt exist in Library.");
                    PressEnter();
                }
                if (bookModel.IsBorrowed == false)
                {
                    Console.WriteLine("\nBook is not borrowed.");
                    PressEnter();
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
                    Console.WriteLine("\nThe user is does'nt exist in Library.");
                    PressEnter();
                    return null;
                }

                bookName = Console.ReadLine();
                bookModel = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (bookModel.BookName == null)
                {
                    Console.WriteLine("\nThe book" + bookModel.BookName + " is does'nt exist in Library.");
                    PressEnter();
                }

                if (userModel != null)
                {
                    var books = await context.Books.Include(u => u.User).Where(b => b.IsBorrowed).Select(b => b.BookName).ToListAsync();
                    count = 1;

                    foreach (var book in books)
                    {
                        Console.WriteLine("\nBooks of borrowed User:\n" + count + ".) " + book + "\n");
                        count++;
                    }
                    if (books == null)
                    {
                        Console.WriteLine("\n User has not borrowed books.");
                        PressEnter();
                    }
                    PressEnter();
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
