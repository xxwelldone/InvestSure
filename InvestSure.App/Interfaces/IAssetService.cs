

using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;

namespace InvestSure.App.Interfaces
{
    public interface IAssetService
    {

        Task<IEnumerable<Asset>> GetAllAsync();
        Task<Asset> GetById(Guid id);
    }
}
