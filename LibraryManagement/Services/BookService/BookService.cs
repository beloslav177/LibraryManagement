using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public class BookService : IBookService
    {
        public Task<List<Book>> AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> BorrowBook(Book book, User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBook(string BookName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> ReturnBook(Book book, User user)
        {
            throw new NotImplementedException();
        }
    }
}
