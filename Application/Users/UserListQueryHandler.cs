using AutoServiss.Application.Entities;
using AutoServiss.Persistence;
using AutoServiss.Shared.Users;
using LiteDB;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoServiss.Application.Users
{
    public class UserListQueryHandler : IRequestHandler<UserListQuery, UserListModel[]>
    {
        private readonly LiteDatabase _db;

        public UserListQueryHandler(ILiteDbContext dbContext)
        {
            _db = dbContext.Database;
        }

        public Task<UserListModel[]> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.GetCollection<User>("Users");
            var users = collection.FindAll();
            var list = users.Select(u => new UserListModel
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Enabled = u.Enabled
            })
            .OrderBy(u => u.FullName)
            .ToArray();

            return Task.FromResult(list);
        }
    }
}
