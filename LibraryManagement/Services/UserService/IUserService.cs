using Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.UserService
{
    public interface IUserService
    {
        Task<User> FindUserOrCreateNewAsync(string message);
        Task<User> AddUserAsync();
        Task<User> GetUserAsync();
        Task<List<User>> GetAllUsersAsync();
    }
}
