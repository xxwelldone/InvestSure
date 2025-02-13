using AutoMapper;
using InvestSure.App.Dtos;
using InvestSure.Domain.Entities;

namespace InvestSure.App.Mappings
{
    public class MappingDTO : Profile
    {
        public MappingDTO()
        {
            CreateMap<CreateInvestorDTO, Investor>();
            CreateMap<Investor, ResponseInvestorDTO>();
        }
    }
}
