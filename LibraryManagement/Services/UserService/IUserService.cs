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
        void FindUser();
        Task<User> AddUser();
        Task<User> DeleteUser();
        Task<User> GetUser();
        Task<List<User>> GetAllUsers();
    }
}
