using AutoServiss.Application.Entities;
using AutoServiss.Persistence;
using AutoServiss.Shared.Users;
using FluentValidation;
using LiteDB;

namespace AutoServiss.Application.Users
{
    public class UserEditValidator : AbstractValidator<UserEditCommand>
    {
        private readonly LiteDatabase _db;

        public UserEditValidator(ILiteDbContext dbContext)
        {
            _db = dbContext.Database;

            RuleFor(x => x.Id).NotEmpty().WithMessage("Nav norādīts Id")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email)
                        .Cascade(CascadeMode.StopOnFirstFailure)
                        .NotEmpty().WithMessage("Nav norādīta E-pasta adrese")
                        .Must(EmailMustUnique).WithMessage("Lietotājs ar šādu e-pasta adresi jau eksistē");
                });           
        }

        private bool EmailMustUnique(UserEditCommand command, string email)
        {
            var collection = _db.GetCollection<User>("Users");
            // ja lietotājs neeksitē - e-pastu nepārbaudam
            var user = collection.FindById(command.Id);
            if (user == null) return true;

            collection.EnsureIndex(x => x.Email);
            return collection.FindOne(x => x.Id != command.Id && x.Email == email) == null;
        }
    }
}