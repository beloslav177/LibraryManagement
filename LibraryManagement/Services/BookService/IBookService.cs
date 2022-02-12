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
        Task<List<Book>> AddBook(Book book);
        Task<List<Book>> DeleteBook(int id);
        Task<List<Book>> GetBook(string BookName);
        Task<Book> GetAllBooks();
        Task<List<Book>> BorrowBook(Book book, User user);
        Task<List<Book>> ReturnBook(Book book, User user);
    }
}
