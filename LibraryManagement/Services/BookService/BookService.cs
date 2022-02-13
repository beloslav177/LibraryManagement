using Library.Data;
using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly DataContext context;
        private readonly User user;

        public BookService(DataContext context, User user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<Book> AddBook(string authorFirstName, string authorLastName, string bookName)
        {
            try
            {
                Console.WriteLine("Please enter a first name and last name of author a book.");
                string authorName = Console.ReadLine();
                authorName = authorFirstName + " " + authorLastName;

                Console.WriteLine("Please enter a name of book.");
                bookName = Console.ReadLine();

                var bookNameExist = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (bookNameExist != null)
                {
                    Console.WriteLine("Book is already exist.");
                }
                var book = new Book { AuthorFirstName = authorFirstName, AuthorLastName = authorLastName, BookName = bookName };
                context.Books.Add(book);
                context.SaveChanges();
                Console.WriteLine("Book " + bookName + " is added by " + authorFirstName + authorLastName);
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
                return null;
            }            
        }

        public async Task<Book> BorrowBook(string bookName, string firstName, string lastName)
        {
            try
            {
                Console.WriteLine("Please enter a name of Book you want to borrow.");
                bookName = Console.ReadLine();
                var book = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (book == null)
                {
                    Console.WriteLine("Book is not exist in Library.");
                    return null;
                }
                if (book.IsBorrowed == true)
                {
                    Console.WriteLine("Book is already taken, it's not possible to borrow once again.");
                    return null;
                }
                else
                {
                    Console.WriteLine("Please write a first name and last name of the person to whom you want to borrow the book");
                    string name = Console.ReadLine();
                    name = firstName + " " + lastName;
                    var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
                    if (user == null) Console.WriteLine("User does'nt exist.");
                    user.IsBorrowing = true;
                    book.IsBorrowed = true;
                    book.UserNameOfBorrowed = firstName + " " + lastName;
                    Console.WriteLine(firstName + " " + lastName + " is borrowing " + bookName);
                }
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
                return null;
            }            
        }

        public async Task<Book> DeleteBook(string bookName)
        {
            try
            {
                Console.WriteLine("Please enter a name of the book you want to delete.");
                bookName = Console.ReadLine();
                var book = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                if (book == null)
                {
                    Console.WriteLine("The book is does'nt exist in Library.");
                    return null;
                }
                if (book.IsBorrowed == true)
                {
                    Console.WriteLine("This book is borrowed, so it's not possible to delete.");
                    return null;
                }
                context.Books.Remove(book);
                Console.WriteLine("You already removed a book with name " + bookName);
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
                return null;
            }            
        }

        public async Task<List<Book>> GetAllBooks()
        {
            try
            {
                var books = await context.Books.ToListAsync();
                Console.WriteLine("Your library:\n");
                books.ForEach(i => Console.Write("{0}\n", i.BookName));
                return books;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
                return null;
            }            
        }

        public async Task<Book> GetBook(string bookName)
        {
            try
            {
                Console.WriteLine("Please enter a name of the book you want to see.");
                bookName = Console.ReadLine();
                var book = await context.Books
                .FirstOrDefaultAsync(b => b.BookName == bookName);

                if (book == null)
                {
                    Console.WriteLine("The book is does'nt exist in Library.");
                    return null;
                }
                Console.WriteLine("Your book " + bookName);
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
                return null;
            }
        }

        public async Task<Book> ReturnBook(string bookName, string firstName, string lastName)
        {
            try
            {
                Console.WriteLine("Please enter a user name and last name which want a return book.");
                string name = Console.ReadLine();
                name = firstName + " " + lastName;
                var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
                if (user == null) Console.WriteLine("User doesn't exist.");
                Console.WriteLine("Please enter a name of book you want a return.");
                bookName = Console.ReadLine();
                var book = await context.Books.FirstOrDefaultAsync(b => b.BookName.Equals(bookName));
                if (book == null) Console.WriteLine("Book is not exist in Library.");
                if (book.IsBorrowed == false) Console.WriteLine("Book is not borrowed.");
                user.IsBorrowing = false;
                book.IsBorrowed = false;
                book.UserNameOfBorrowed = "";
                Console.WriteLine("The book " + bookName + " is returned");
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown", ex);
                return null;
            }
        }
    }
}
