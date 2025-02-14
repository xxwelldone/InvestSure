

namespace InvestSure.Domain.Entities
{
    public class Asset : BaseEntity
    {
        public string AssetName { get; set; }
        public string TypeAsset {  get; set; }
        public decimal Price { get; set; }
        public string Currency {  get; set; }
         
    }
}
