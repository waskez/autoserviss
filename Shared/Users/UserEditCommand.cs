using MediatR;

namespace AutoServiss.Shared.Users
{
    public class UserEditCommand : IRequest<UserEditResult>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
    }
}