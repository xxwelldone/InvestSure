using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
