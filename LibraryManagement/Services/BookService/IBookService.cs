using Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public interface IBookService
    {
        Task<Book> AddAuthorOfBookAsync(string message);
        Task<Book> FindBookOrCreateNewAsync(string message);
        Task<Book> AddBookAsync();
        Task DeleteBookAsync();
        Task<Book> GetBookAsync();
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> BorrowBookAsync();
        Task<Book> ReturnBookAsync();
        Task<List<Book>> GetBorrowedBooksAsync(User userModel);
    }
}
