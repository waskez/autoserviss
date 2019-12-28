using AutoServiss.Shared.Users;
using System.Threading.Tasks;

namespace AutoServiss.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginQuery loginQuery);
        Task Logout();
    }
}