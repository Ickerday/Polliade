using Polliade.Models;
using System.Threading.Tasks;

namespace Polliade.Services.UserAuth
{
    public interface IUserAuthService
    {
        Task<User> GetLoggedInUserAsync();
        Task Logout();

    }
}