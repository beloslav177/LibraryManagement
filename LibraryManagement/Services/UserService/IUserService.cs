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
        public bool Close { get; set; }

        Task<User> AddUser();
        Task<User> DeleteUser();
        Task<User> GetUser();
        Task<List<User>> GetAllUsers();
        Task<User> GetBorrowedBooks();
    }
}
