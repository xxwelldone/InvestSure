﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestSure.Domain.Entities;

namespace InvestSure.Domain.Interfaces
{
    public interface IInvestorRepository : IBaseRepository<Investor>
    {
        Task<Investor> GetByEmail(string email);
    }
}
