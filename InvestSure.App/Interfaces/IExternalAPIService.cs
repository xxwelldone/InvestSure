

namespace InvestSure.App.Interfaces
{
    public interface IExternalAPIService
    {
        Task<double> GetExhangeAsync(string baseCurrency, string buyingCurrency);
    }
}
