
using Dapper;
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;
using InvestSure.Infra.Data;

namespace InvestSure.Infra.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DBSession session) : base(session)
        {
        }
        public async Task<IEnumerable<Account>> findByInvestorIdAsync(Guid investorId)
        {
            string sql = $@"SELECT * from public.investor where investor_id = @investor_id";
            IEnumerable<Account> accounts = await Session.DbConnection.QueryAsync<Account>(sql, investorId);
            return accounts;


        }
    }
}
