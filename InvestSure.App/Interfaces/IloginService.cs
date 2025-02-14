

using InvestSure.App.Dtos;
using InvestSure.Domain.Entities;

namespace InvestSure.App.Interfaces
{
    public interface IloginService
    {

        Task<InvestorResponseDTO> CreateAsync(InvestorCreateDTO investor);
        Task<AuthenticatedDTO> LoginAsync(LoginDTO loginDTO);
        Task<Investor> GetCurrentUserAsync();
    }
}
