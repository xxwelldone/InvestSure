using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestSure.Domain.Entities;

namespace InvestSure.Domain.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account> findByInvestorIdAsync(Guid id);
    }
}
