

using System.Reflection.Metadata;
using AutoMapper;
using InvestSure.App.Dtos;
using InvestSure.App.Interfaces;
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;


namespace InvestSure.App.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestimentRepository _investmentrepository;
        private readonly IloginService _loginService;
        private readonly IAssetRepository _assetRepository;
        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;

        public InvestmentService(IInvestimentRepository repository, IMapper mapper, IAssetRepository assetRepository,
            IAccountRepository accountRepository)
        {
            _investmentrepository = repository;
            _mapper = mapper;
            _assetRepository = assetRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Investment> Create(InvetmentCreateDTO createDTO)
        {
            Investor investor = await _loginService.GetCurrentUserAsync();

            Account account = await _accountRepository.findByInvestorIdAsync(investor.Id);

            if (account == null)
            {
                throw new InvalidOperationException("Conta fornecida não existe ou não pertence ao usuário");
            }
            else
            {
                Asset asset = await _assetRepository.GetByIdAsync(createDTO.Asset_Id);
                if (asset == null) { throw new Exception("Id informado para Asset não existe em nossa base"); }


                Decimal totalAmount = asset.Price * createDTO.Quantity;

                if (account.Amount < totalAmount)
                {
                    throw new InvalidOperationException("Saldo insuficiente para realizar compra de investimento");

                }
                else
                {
                    Investment investment = new Investment()
                    {
                        Account_Id = account.Id,
                        Investiment_Type = asset.TypeAsset,
                        AssetName = asset.AssetName,
                        Quantity = createDTO.Quantity,
                        TotalAmount = totalAmount,
                        Currency = asset.Currency,

                    };
                    Guid id = await _investmentrepository.CreateAsync(investment);
                    Investment created = await _investmentrepository.GetByIdAsync(id);
                    return created;

                }



            }

        }
    }
}
