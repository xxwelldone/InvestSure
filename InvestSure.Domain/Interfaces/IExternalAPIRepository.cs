

using InvestSure.Domain.Entities;

namespace InvestSure.Domain.Interfaces
{
    public interface IExternalAPIRepository
    {
        Task<ExchangeRateResponse> GetAsync(string parameter);
    }
}
