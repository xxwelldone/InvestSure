

namespace InvestSure.Domain.Entities
{
    public class Investor : BaseEntity
    {
        public string Name { get; set; }
        public string Nacionality { get; set; }
        public DateOnly BirthDate { get; set; }
        public IEnumerable<Account> accounts { get; set; } = new List<Account>();

    }
}
