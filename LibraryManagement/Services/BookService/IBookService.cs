using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public interface IBookService
    {
        Task<Book> AddBook(string authorFirstName, string authorLastName, string bookName);
        Task<Book> DeleteBook(string bookName);
        Task<Book> GetBook(string BookName);
        Task<List<Book>> GetAllBooks();
        Task<Book> BorrowBook(string bookName, string firstName, string lastName);
        Task<Book> ReturnBook(string bookName, string firstName, string lastName);
    }
}
