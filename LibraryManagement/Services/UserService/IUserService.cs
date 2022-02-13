using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.UserService
{
    public interface IUserService
    {
        Task<User> AddUser(string firstName, string lastName);
        Task<User> DeleteUser(string firstName, string lastName);
        Task<User> GetUser(string firstName, string lastName);
        Task<List<User>> GetAllUsers();
        Task<Book> GetBorrowedBooks(string bookName, string firstName, string lastName);
    }
}
