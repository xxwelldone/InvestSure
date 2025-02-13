

using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using InvestSure.App.Dtos;
using InvestSure.App.Interfaces;
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;

namespace InvestSure.App.Services
{
    public class LoginService : IloginService
    {
        private readonly IMapper _mappingToDTO;
        private readonly IAuthenticationService _authenticate;
        private readonly IInvestorRepository _investorRepository;

        public LoginService(IMapper mappingToDTO, IAuthenticationService authenticate, IInvestorRepository investorRepository)
        {
            _mappingToDTO = mappingToDTO;
            _authenticate = authenticate;
            _investorRepository = investorRepository;
        }

        public async Task<ResponseInvestorDTO> CreateAsync(CreateInvestorDTO investor)
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

            ResponseInvestorDTO responseDTO = _mappingToDTO.Map<ResponseInvestorDTO>(investorCreated);

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
            return new AuthenticatedDTO { Email = loginDTO.Email,  Token = token };

        }
    }
}
