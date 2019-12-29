using AutoServiss.Application.Entities;
using AutoServiss.Application.Exceptions;
using AutoServiss.Persistence;
using AutoServiss.Shared.Users;
using LiteDB;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AutoServiss.Application.Users
{
    public class UserEditCommandHandler : IRequestHandler<UserEditCommand, UserEditResult>
    {
        private readonly LiteDatabase _db;

        public UserEditCommandHandler(ILiteDbContext dbContext)
        {
            _db = dbContext.Database;
        }

        public Task<UserEditResult> Handle(UserEditCommand request, CancellationToken cancellationToken)
        {
            var collection = _db.GetCollection<User>("Users");
            var user = collection.FindById(request.Id);
            if (user == null)
            {
                throw new NotFoundException("Lietotājs neeksistē");
            }

            user.Email = request.Email;
            user.Enabled = request.Enabled;

            collection.Update(user);

            return Task.FromResult(new UserEditResult
            {
                Message = $"Atjaunināti lietotāja \"{request.Email}\" dati"
            });
        }
    }
}