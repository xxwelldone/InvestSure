

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
        private readonly IExternalAPIService _externalAPIService;

        private readonly IMapper _mapper;

        public InvestmentService(IInvestimentRepository investmentrepository,
            IloginService loginService, IAssetRepository assetRepository,
            IAccountRepository accountRepository, IExternalAPIService externalAPIService, IMapper mapper)
        {
            _investmentrepository = investmentrepository;
            _loginService = loginService;
            _assetRepository = assetRepository;
            _accountRepository = accountRepository;
            _externalAPIService = externalAPIService;
            _mapper = mapper;
        }

        public async Task<Investment> Create(InvetmentCreateDTO createDTO)
        {
            Investor investor = await _loginService.GetCurrentUserAsync();

            IEnumerable<Account> accountList = await _accountRepository.findByInvestorIdAsync(investor.Id);
            Account? account = accountList.FirstOrDefault(x => x.Id == createDTO.Account_Id);

            if (account == null)
            {
                throw new InvalidOperationException("Conta fornecida não existe ou não pertence ao usuário");
            }
            else
            {
                Asset asset = await _assetRepository.GetByIdAsync(createDTO.Asset_Id);
                if (asset == null) { throw new Exception("Id informado para Asset não existe em nossa base"); }


                double doubleExchangeRate = await _externalAPIService.GetExhangeAsync(account.Currency, asset.Currency);
                Decimal exchangeRate =  Convert.ToDecimal(doubleExchangeRate);

                Decimal amountInvested = asset.Price * createDTO.Quantity;
                Decimal amountToBePaid = amountInvested * exchangeRate;

                if (account.Amount < amountToBePaid)
                {
                    throw new InvalidOperationException("Saldo insuficiente para realizar compra de investimento");

                }
                else
                {
                    account.Withdraw(amountToBePaid);
                    await _accountRepository.Update(account);
                    Investment investment = new Investment()
                    {
                        Account_Id = account.Id,
                        Investiment_Type = asset.TypeAsset,
                        AssetName = asset.AssetName,
                        Asset_Id = asset.Id,
                        Quantity = createDTO.Quantity,
                        TotalAmount = amountInvested,
                        Currency = asset.Currency,
                        CreatedAt = DateTime.Now

                    };
                    
                    
                    Guid id = await _investmentrepository.CreateAsync(investment);
                    Investment created = await _investmentrepository.GetByIdAsync(id);

                    return created;

                }



            }

        }
        public async Task<Investment> GetByIdAsync(Guid id)
        {
            Investment investment = await _investmentrepository.GetByIdAsync(id);
            return investment;


        }
    }
}
