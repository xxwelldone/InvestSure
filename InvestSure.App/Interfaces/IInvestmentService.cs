

using InvestSure.App.Dtos;
using InvestSure.Domain.Entities;

namespace InvestSure.App.Interfaces
{
    public interface IInvestmentService
    {
        Task<Investment> Create(InvetmentCreateDTO createDTO);
    }
}
