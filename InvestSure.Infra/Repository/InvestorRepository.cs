
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
    }
}
