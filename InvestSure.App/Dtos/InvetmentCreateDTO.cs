

using System.ComponentModel.DataAnnotations;

namespace InvestSure.App.Dtos
{
    public class InvetmentCreateDTO
    {
        public Guid Asset_Id { get; set; }
        public Guid Account_Id { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Valor deve ser maior que 0")]
        public int Quantity { get; set; }
    }
}
