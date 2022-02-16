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
        void AddAuthorOfBook();
        Task<Book> AddBook();
        Task<Book> DeleteBook();
        Task<Book> GetBook();
        Task<List<Book>> GetAllBooks();
        Task<Book> BorrowBook();
        Task<Book> ReturnBook();
        Task<List<string>> GetBorrowedBooks();
    }
}
