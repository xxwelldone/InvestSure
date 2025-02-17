
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;

namespace InvestSure.App.Interfaces
{
    public class ExternalAPIService : IExternalAPIService
    {
        private readonly IExternalAPIRepository _externalAPIRepository;

        public ExternalAPIService(IExternalAPIRepository externalAPIRepository)
        {
            _externalAPIRepository = externalAPIRepository;
        }

        public async Task<double> GetExhangeAsync(string baseCurrency, string buyingCurrency)
        {
          ExchangeRateResponse exchangeRate= await _externalAPIRepository.GetAsync(baseCurrency);
          double value =  exchangeRate.Conversion_Rates.FirstOrDefault(x=> x.Key == buyingCurrency).Value;

            return value;
        }
    }
}
