using Biblioteca.Models;
using Biblioteca.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class AuthService
    {
        private AuthorRepository AuthorRepository { get; set; }
        public JwtSecurityTokenHandler TokenHandler { get; set; }

        public AuthService(AuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
            TokenHandler = new JwtSecurityTokenHandler();
        }

        private string GenerateToken(Guid Id, string email)
        {
            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes("ultralongpassphrasethatshouldnotbehere")),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            JwtSecurityToken Token = TokenHandler.CreateJwtSecurityToken(TokenDescriptor);
            TokenHandler.WriteToken(Token);
            return Token.RawData;
        }

        public async Task<LoginResponse> Authenticate(string email, string password)
        {
            Author author = await AuthorRepository.GetByEmail(email);
            if (author == null)
                return new LoginResponse { Status = false };

            if (password != author.Password)
                return new LoginResponse { Status = false };

            string token = GenerateToken(author.Id, author.Email);

            return new LoginResponse { Status = true, Token = token, Id = author.Id };
        }
    }
}
