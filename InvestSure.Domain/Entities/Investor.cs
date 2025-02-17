

namespace InvestSure.Domain.Entities
{
    public class Investor : BaseEntity
    {
        public string Name { get; set; }
        public string Nacionality { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt { get; set; }

    }
}
