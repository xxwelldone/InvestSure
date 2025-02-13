
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
            using (var connection = Session.DbConnection)
            {
                string sql = $@"SELECT * FROM public.investor WHERE email = @email";

                Investor investor = await connection.QueryFirstOrDefaultAsync<Investor>(sql, email);
                return investor;
            }
        }


    }
}
