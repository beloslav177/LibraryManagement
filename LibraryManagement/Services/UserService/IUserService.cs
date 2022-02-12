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
        Task<List<User>> AddUser(User user);
        Task<List<User>> DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetBorrowedBooks(Book book, User user);
        Task<User> GetUser (string FirstName, string LastName);
    }
}
