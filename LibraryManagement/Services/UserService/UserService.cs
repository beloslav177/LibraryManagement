using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.UserService
{
    public class UserService : IUserService
    {
        public Task<List<User>> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetBorrowedBooks(Book book, User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string FirstName, string LastName)
        {
            throw new NotImplementedException();
        }
    }
}
