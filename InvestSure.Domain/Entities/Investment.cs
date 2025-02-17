using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestSure.Domain.Entities
{
    public class Investment : BaseEntity
    {

        public Guid Account_Id { get; set; }
        public string Investiment_Type { get; set; }
        public string AssetName { get; set; }
        public Guid Asset_Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }

    }
}
