

using InvestSure.App.Dtos;

namespace InvestSure.App.Interfaces
{
    public interface IloginService
    {

        Task<ResponseInvestorDTO> CreateAsync(CreateInvestorDTO investor);
        Task<AuthenticatedDTO> LoginAsync(LoginDTO loginDTO);
    }
}
