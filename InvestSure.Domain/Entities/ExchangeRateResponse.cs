

namespace InvestSure.Domain.Entities
{
    public class ExchangeRateResponse
    {
        public string? Result { get; set; }
        public string? Documentation { get; set; }
        public string? Terms_of_use { get; set; }
        public long? Time_last_update_unix { get; set; }
        public string? Time_last_update_utc { get; set; }
        public long? Time_next_update_unix { get; set; }
        public string? Time_next_update_utc { get; set; }
        public string? Base_Code { get; set; }
        public Dictionary<string, double>? Conversion_Rates { get; set; }
    }
}
