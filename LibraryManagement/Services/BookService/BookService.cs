using Library.Data;
using Library.Model;
using Library.Services.UserService;
using LibraryManagement.Services.MessageService;
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
        private MessageService messageService = new MessageService();

        public async Task<Book> AddAuthorOfBookAsync(string message)
        {
            string authorFirstName = "";
            string authorLastName = "";

            Console.WriteLine("Please enter a first name and last name of author a book.");
            string authorName = Console.ReadLine();
            var tokens = authorName.Split(' ');
            authorFirstName = tokens[0];
            if (tokens.Length > 1)
            {
                authorLastName = tokens[1];
            }

            string bookName = "";
            Console.WriteLine($"Please enter a name of book for {message}");

            bookName = Console.ReadLine();
            var b = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
            if (b != null)
            {
                return b;
            }
            else
            {
                return new Book { AuthorFirstName = authorFirstName, AuthorLastName = authorLastName , BookName = bookName };
            }
        }

        public async Task<Book> FindBookOrCreateNewAsync(string message)
        { 
            string bookName = "";
            Console.WriteLine($"Please enter a name of book for {message}");

            bookName = Console.ReadLine();
            var b = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
            if (b != null)
            {
                return b;
            }
            else
            {
                return new Book { BookName = bookName };
            }
        }

        public async Task<Book> AddBookAsync()
        {
            try
            {
                var book = await AddAuthorOfBookAsync("add book");
                if (book.Id == default)
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                    Console.WriteLine("\nBook " + book.BookName + " is added by Author: " + book.AuthorName);
                }
                else
                {
                    messageService.Exist(book.BookName);
                }
                messageService.PressAny();
                return book;
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }            
        }        

        public async Task DeleteBookAsync()
        {
            try
            {
                //var book = await FindBookOrCreateNewAsync("delete Book");

                //if (book.Id == default)
                //{
                //    messageService.NotExist(book.BookName);
                //}
                //else
                //{
                //    if ((await GetBorrowedBooksAsync(userModel)).Count == 0)
                //    {
                //        context.Books.Remove(book);
                //        context.SaveChanges();
                //        Console.WriteLine("\nYou already removed book with name " + book.BookName  + " from " + book.AuthorName);
                //    }
                //    else
                //    {
                //        Console.WriteLine($"\n {book.BookName} book is'nt possible to delete, cause is already borrowed.");
                //    }
                //}
                //messageService.PressAny();
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            try
            {
                var books = await context.Books.ToListAsync();
                if (!books.Any())
                {
                    messageService.NotExist("Books");
                    messageService.PressAny();
                    return null;
                }
                else
                {
                    Console.WriteLine("Your library of books:\n");
                    books.ForEach(i => Console.WriteLine("{0}", i.BookName));
                    messageService.PressAny();
                    return books;
                }
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }            
        }

        public async Task<Book> GetBookAsync()
        {
            try
            {
                var bookModel = await FindBookOrCreateNewAsync("search");
                
                if (bookModel.Id == default)
                {
                    messageService.NotExist(bookModel.BookName);
                    messageService.PressAny();
                    return null;
                }
                else
                {
                    Console.WriteLine("\nSearched book: " + bookModel.BookName);
                    messageService.PressAny();
                    return bookModel;
                }
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }
        }

        public async Task<Book> BorrowBookAsync()
        {
            try
            {
                var bookModel = await FindBookOrCreateNewAsync("borrow the Book");

                bookModel = await context.Books.Include(u => u.User).FirstOrDefaultAsync(b => b.BookName == bookModel.BookName);

                if (bookModel == null)
                {
                    messageService.NotExist("Requested book");
                    messageService.PressAny();
                    return null;
                }
                else if (bookModel.User != default)
                {
                    messageService.IsBorrowing(bookModel.BookName);
                    messageService.PressAny();
                    return null;
                }
                else
                {
                    Console.WriteLine("\nYou want a borrow book: " + bookModel.BookName + "\n");
                    return bookModel;                    
                }                
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }
        }

        public async Task<Book> ReturnBookAsync()
        {
            try
            {
                var bookModel = await FindBookOrCreateNewAsync("return the Book");
                if (bookModel.Id == default)
                {
                    messageService.NotExist(bookModel.BookName);
                    messageService.PressAny();
                    return null;
                }
                else if (bookModel.User != default)
                {
                    messageService.IsBorrowing(bookModel.BookName);
                    messageService.PressAny();
                    return bookModel;
                }
                else
                {
                    bookModel.User = null;
                    context.SaveChanges();
                    Console.WriteLine("User return book with title: " + bookModel.BookName);
                    messageService.PressAny();
                    return bookModel;
                }   
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }
        }

        public async Task<List<Book>> GetBorrowedBooksAsync(User userModel)
        {
            try
            {
                if (userModel != null)
                {
                    return await context.Books.Include(u => u.User).Where(b => b.User != default).ToListAsync();                    
                }
                return new List<Book>();
            }
            catch (Exception ex)
            {
                messageService.Error(ex);
                return null;
            }
        }
    }
}
