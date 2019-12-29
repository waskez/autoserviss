using AutoServiss.Application.Entities;
using AutoServiss.Application.Infrastructure;
using AutoServiss.Persistence;
using AutoServiss.Shared.Users;
using LiteDB;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoServiss.Application.Users
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResult>
    {
        private readonly LiteDatabase _db;
        private readonly IConfiguration _configuration;

        public LoginQueryHandler(ILiteDbContext dbContext, IConfiguration configuration)
        {
            _db = dbContext.Database;
            _configuration = configuration;
        }

        public Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var result = new LoginResult
            {
                Errors = new[] { "Nepareiza e-pasta adrese vai parole" }
            };

            var collection = _db.GetCollection<User>("Users");
            collection.EnsureIndex(x => x.Email);
            var user = collection.FindOne(u => u.Email == request.Email);
            if (user != null)
            {
                var valid = PasswordHash.ValidatePassword(request.Password, user.PasswordHash);
                if (valid)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:SecretKey"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("sub", user.Email)
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    result.Token = tokenHandler.WriteToken(token);
                    result.Errors = null;
                }
            }          

            return Task.FromResult(result);
        }
    }
}