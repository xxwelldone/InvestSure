namespace InvestSure.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string AccountAgency {  get; set; }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string Investor_Id { get; set; }
        public Investor Investor { get; set; }
        

    }
}