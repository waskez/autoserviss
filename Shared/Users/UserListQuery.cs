using MediatR;

namespace AutoServiss.Shared.Users
{
    public class UserListQuery : IRequest<UserListModel[]>
    {
    }
}