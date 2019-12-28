using AutoServiss.Application.Entities;
using AutoServiss.Persistence;
using AutoServiss.Shared.Users;
using FluentValidation;
using LiteDB;
using System.Text.RegularExpressions;

namespace AutoServiss.Application.Users
{
    public class UserCreateValidator : AbstractValidator<UserCreateCommand>
    {
        private readonly LiteDatabase _db;

        public UserCreateValidator(ILiteDbContext dbContext)
        {
            _db = dbContext.Database;

            RuleFor(x => x.FullName).NotEmpty().WithMessage("Nav norādīts Pilns vārds");
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Nav norādīta E-pasta adrese")
                .Must(EmailMustUnique).WithMessage("Lietotājs ar šādu e-pasta adresi jau eksistē");
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Nav norādīta Parole")
                .Custom((parole, context) =>
                {
                    if (!string.IsNullOrEmpty(parole))
                    {
                        if (parole.Length < 9)
                            context.AddFailure("Parolei jābūt vismaz 9 simbolus garai");
                        if (!Regex.IsMatch(parole, @"[0-9]+(\.[0-9][0-9]?)?"))
                            context.AddFailure("Parolē jābūt vismaz vienam ciparam");
                        if (!Regex.IsMatch(parole, @"^(?=.*[a-z]).+$"))
                            context.AddFailure("Parolē jābūt vismaz vienam mazajam burtam");
                        if (!Regex.IsMatch(parole, @"^(?=.*[A-Z]).+$"))
                            context.AddFailure("Parolē jābūt vismaz vienam lielajam burtam");
                        if (!Regex.IsMatch(parole, @"[.,!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]"))
                            context.AddFailure("Parolē jābūt vismaz vienam speciālajam simbolam");
                    }
                });
        }

        private bool EmailMustUnique(string email)
        {
            var collection = _db.GetCollection<User>("Users");
            collection.EnsureIndex(x => x.Email);
            return collection.FindOne(x => x.Email == email) == null;
        }
    }
}