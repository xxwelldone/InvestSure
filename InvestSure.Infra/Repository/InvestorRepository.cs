
using System.Security.Cryptography.X509Certificates;
using Dapper;
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;
using InvestSure.Infra.Data;

namespace InvestSure.Infra.Repository
{
    public class InvestorRepository : BaseRepository<Investor>, IInvestorRepository
    {
        public InvestorRepository(DBSession session) : base(session)
        {

          
        }
        public async Task<Investor> GetByEmail(string email)
        {
           
                string sql = $@"SELECT * FROM public.investor WHERE email = @email";

                Investor investor = await Session.DbConnection.QueryFirstOrDefaultAsync<Investor>(sql, new { email});
                return investor;
            
        }


    }
}
