

using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using InvestSure.App.Dtos;
using InvestSure.App.Interfaces;
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InvestSure.App.Services
{
    public class LoginService : IloginService
    {
        private readonly IMapper _mappingToDTO;
        private readonly IAuthenticationService _authenticate;
        private readonly IInvestorRepository _investorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LoginService(IMapper mappingToDTO, IAuthenticationService authenticate, IInvestorRepository investorRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _mappingToDTO = mappingToDTO;
            _authenticate = authenticate;
            _investorRepository = investorRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<InvestorResponseDTO> CreateAsync(InvestorCreateDTO investor)
        {
            Investor user = _mappingToDTO.Map<Investor>(investor);

            bool userExist = await _authenticate.UserExists(user.Email);

            if (userExist)
            {

                throw new Exception("Usuário já cadastrado no sistema");
            }

            using HMAC crypt = new HMACSHA512();
            byte[] passwordHash = crypt.ComputeHash(Encoding.UTF8.GetBytes(investor.Password));
            byte[] passwordSalt = crypt.Key;
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _investorRepository.CreateAsync(user);
            Investor investorCreated = await _investorRepository.GetByEmail(user.Email);

            InvestorResponseDTO responseDTO = _mappingToDTO.Map<InvestorResponseDTO>(investorCreated);

            return responseDTO;

        }
        public async Task<AuthenticatedDTO> LoginAsync(LoginDTO loginDTO)
        {

            Investor user = await _investorRepository.GetByEmail(loginDTO.Email);
            if (user == null)
            {
                throw new Exception("Usuário inválido");
            }
            bool isAuthenticated = await _authenticate.AuthenticateAsync(loginDTO.Email, loginDTO.Password);
            if (!isAuthenticated)
            {
                throw new Exception("Usuário ou senha inválidos");
            }
            string token = _authenticate.GenerateToken(user);
            return new AuthenticatedDTO { Email = loginDTO.Email, Token = token };

        }
        public async Task<Investor> GetCurrentUserAsync()
        {
            Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("id").Value);
            Investor investor = await _investorRepository.GetByIdAsync(userId);
            if (investor == null)
            {
                throw new Exception("Usuário não encontrado");

            }
            return investor;

        }
    }
}
