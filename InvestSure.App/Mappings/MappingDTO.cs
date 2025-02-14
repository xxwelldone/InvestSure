using AutoMapper;
using InvestSure.App.Dtos;
using InvestSure.Domain.Entities;

namespace InvestSure.App.Mappings
{
    public class MappingDTO : Profile
    {
        public MappingDTO()
        {
            CreateMap<InvestorCreateDTO, Investor>()
             .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToDateTime(new TimeOnly(0, 0))));
            CreateMap<Investor, InvestorResponseDTO>();
        }
    }
}
