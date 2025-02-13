
using InvestSure.Domain.Entities;

namespace InvestSure.App.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> UserExists(string email);
        string GenerateToken(Investor user);
    }
}
