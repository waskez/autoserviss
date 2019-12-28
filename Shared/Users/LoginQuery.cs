using MediatR;

namespace AutoServiss.Shared.Users
{
    public class LoginQuery : IRequest<LoginResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}