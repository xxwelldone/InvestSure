using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InvestSure.App.Interfaces;
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InvestSure.App.Services
{
    public class AuthenticateService : IAuthenticationService
    {
        private readonly IInvestorRepository _investorRepository;
        private readonly IConfiguration _configuration;


        public AuthenticateService(IInvestorRepository investorRepository, IConfiguration configuration)
        {
            _investorRepository = investorRepository;
            _configuration = configuration;

        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            Investor user = await _investorRepository.GetByEmail(email);

            if (user == null) { return false; }
            using HMACSHA512 hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return false;
                }
            }
            return true;

        }

        public string GenerateToken(Investor user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256),
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"]
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> UserExists(string email)
        {
            Investor user = await _investorRepository.GetByEmail(email);

            if (user == null) { return false; }
            return true;
        }
    }
}
