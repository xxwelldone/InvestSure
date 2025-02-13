

namespace InvestSure.App.Dtos
{
    public class ResponseInvestorDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Nacionality { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
