using AutoServiss.Application.Entities;
using AutoServiss.Application.Infrastructure;
using AutoServiss.Persistence;
using AutoServiss.Shared.Users;
using LiteDB;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AutoServiss.Application.Users
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserCreateResult>
    {
        private readonly LiteDatabase _db;

        public UserCreateCommandHandler(ILiteDbContext dbContext)
        {
            _db = dbContext.Database;
        }

        public Task<UserCreateResult> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var collection = _db.GetCollection<User>("Users");
            collection.Insert(new User
            {
                Email = request.Email,
                PasswordHash = PasswordHash.HashPassword(request.Password),
                Enabled = request.Enabled
            });

            return Task.FromResult(new UserCreateResult
            {
                Message = $"Izveidots jauns lietotājs \"{request.Email}\""
            });
        }
    }
}