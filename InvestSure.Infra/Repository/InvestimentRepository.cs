﻿
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;
using InvestSure.Infra.Data;

namespace InvestSure.Infra.Repository
{
    public class InvestimentRepository : BaseRepository<Investment>, IInvestimentRepository
    {
        public InvestimentRepository(DBSession session) : base(session)
        {
        }
    }
}
