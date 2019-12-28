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
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResult>
    {
        private readonly LiteDatabase _db;

        public LoginQueryHandler(ILiteDbContext dbContext)
        {
            _db = dbContext.Database;
        }

        public Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var result = new LoginResult
            {
                Message = "Nepareiza e-pasta adrese vai parole"
            };

            var collection = _db.GetCollection<User>("Users");
            collection.EnsureIndex(x => x.Email);
            var user = collection.FindOne(u => u.Email == request.Email);
            if (user != null)
            {
                var valid = PasswordHash.ValidatePassword(request.Password, user.PasswordHash);
                if (valid) result.Message = "OK";
            }          

            return Task.FromResult(result);
        }
    }
}