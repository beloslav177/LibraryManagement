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
        private DataContext context = new DataContext();
        private User user = new User();

        public async Task<Book> AddBook()
        {
            try
            {
                string authorFirstName;
                string authorLastName;        
                string bookName;
                Console.WriteLine("Please enter a first name and last name of author a book.");
                string authorName = Console.ReadLine();
                authorFirstName = authorName.Split(' ')[0];
                authorLastName = authorName.Split(' ')[1];
                authorName = authorFirstName + " " + authorLastName;

                Console.WriteLine("Please enter a name of book.");
                bookName = Console.ReadLine();
                var bookNameExist = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);

                while (bookNameExist != null)
                {
                    Console.WriteLine("Book is already exist. Please try again.");
                    bookName = Console.ReadLine();
                    bookNameExist = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                }

                var book = new Book { AuthorFirstName = authorFirstName, AuthorLastName = authorLastName, BookName = bookName };
                context.Books.Add(book);
                context.SaveChanges();
                Console.WriteLine("Book " + bookName + " is added by " + authorFirstName + authorLastName);
                return book;
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
                string bookName;
                string firstName;
                string lastName;
                Console.WriteLine("Please enter a name of Book you want to borrow.");

                bookName = Console.ReadLine();
                var book = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);

                while(book == null)
                {
                    Console.WriteLine("Book is not exist in Library. Try again");
                    bookName = Console.ReadLine();
                    book = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                }

                while (book.IsBorrowed == true)
                {
                    Console.WriteLine("Book is already taken, it's not possible to borrow once again.");
                    bookName = Console.ReadLine();
                    book = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                }

                Console.WriteLine("Please write a first name and last name of the person to whom you want to borrow the book");
                string name = Console.ReadLine();
                firstName = name.Split(' ')[0];
                lastName = name.Split(' ')[1];
                name = firstName + " " + lastName;
                var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);

                if (user == null) 
                {
                    Console.WriteLine("User does'nt exist.");
                    name = Console.ReadLine();
                    firstName = name.Split(' ')[0];
                    lastName = name.Split(' ')[1];
                    name = firstName + " " + lastName;
                    user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
                }

                user.IsBorrowing = true;
                book.IsBorrowed = true;
                book.UserNameOfBorrowed = firstName + " " + lastName;
                context.SaveChanges();
                Console.WriteLine(firstName + " " + lastName + " is borrowing " + bookName);
                return book;
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
                string bookName;
                Console.WriteLine("Please enter a name of the book you want to delete.");

                bookName = Console.ReadLine();
                var book = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);

                while(book == null)
                {
                    Console.WriteLine("The book is does'nt exist in Library. Try again.");
                    bookName = Console.ReadLine();
                    book = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                }

                while (book.IsBorrowed == true)
                {
                    Console.WriteLine("This book is borrowed, so it's not possible to delete. Try different book.");
                    bookName = Console.ReadLine();
                    book = await context.Books.FirstOrDefaultAsync(b => b.BookName == bookName);
                }

                context.Books.Remove(book);
                context.SaveChanges();
                Console.WriteLine("You already removed a book with name " + bookName);
                return book;
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
                var books = await context.Books.ToListAsync();
                Console.WriteLine("Your library:\n");
                books.ForEach(i => Console.Write("{0}\n", i.BookName));
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
                string bookName;
                Console.WriteLine("Please enter a name of the book you want to see.");
                bookName = Console.ReadLine();
                var book = await context.Books
                .FirstOrDefaultAsync(b => b.BookName == bookName);

                while (book == null)
                {
                    Console.WriteLine("The book is does'nt exist in Library. Try again.");
                    bookName = Console.ReadLine();
                    book = await context.Books
                    .FirstOrDefaultAsync(b => b.BookName == bookName);
                }
                Console.WriteLine("Your book " + bookName);
                return book;
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
                string bookName;
                string firstName;
                string lastName;
                Console.WriteLine("Please enter a user name and last name which want a return book.");

                string name = Console.ReadLine();
                firstName = name.Split(' ')[0];
                lastName = name.Split(' ')[1];
                name = firstName + " " + lastName;
                var user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);

                while (user == null)
                {
                    Console.WriteLine("User doesn't exist. Please try again.");
                    name = Console.ReadLine();
                    firstName = name.Split(' ')[0];
                    lastName = name.Split(' ')[1];
                    name = firstName + " " + lastName;
                    user = await context.Users.FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
                }
                Console.WriteLine("Please enter a name of book you want a return.");

                bookName = Console.ReadLine();
                var book = await context.Books.FirstOrDefaultAsync(b => b.BookName.Equals(bookName));

                while (book == null)
                {
                    Console.WriteLine("Book is not exist in Library. Try again.");
                    bookName = Console.ReadLine();
                    book = await context.Books.FirstOrDefaultAsync(b => b.BookName.Equals(bookName));
                }
                while (book.IsBorrowed == false)
                {
                    Console.WriteLine("Book is not borrowed. Try different book.");
                    bookName = Console.ReadLine();
                    book = await context.Books.FirstOrDefaultAsync(b => b.BookName.Equals(bookName));
                }

                user.IsBorrowing = false;
                book.IsBorrowed = false;
                book.UserNameOfBorrowed = null;
                context.SaveChanges();

                Console.WriteLine("The book " + bookName + " is returned");
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                return null;
            }
        }
    }
}
