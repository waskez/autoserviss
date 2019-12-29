using MediatR;

namespace AutoServiss.Shared.Users
{
    public class UserCreateCommand : IRequest<UserCreateResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
    }
}