


namespace InvestSure.App.Dtos
{
    public class CreateInvestorDTO
    {
        public string Name { get; set; }
        public string Nacionality { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
    }
}
