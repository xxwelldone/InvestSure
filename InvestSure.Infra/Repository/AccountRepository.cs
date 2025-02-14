
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
        public async Task<Account> findByInvestorIdAsync(Guid id) {
            string sql = $@"SELECT * from public.investor where investor_id = @id";
            return await Session.DbConnection.QueryFirstOrDefaultAsync<Account>(sql, id);
        
        }
    }
}
