
using InvestSure.App.Interfaces;
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;

namespace InvestSure.App.Services
{
    public class AssetService : IAssetService
    {

        private readonly IAssetRepository _assetRepository;

        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            IEnumerable<Asset> assets = await _assetRepository.GetAllAsync();
            return assets;
        }

        public async Task<Asset> GetById(Guid id)
        {
            return await _assetRepository.GetByIdAsync(id);

        }
    }
}
