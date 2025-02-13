using InvestSure.App.Dtos;
using InvestSure.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestSure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IloginService _loginService;

        public LoginController(IloginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseInvestorDTO>> Create(CreateInvestorDTO usuarioCreateDTO)
        {
            try
            {
                ResponseInvestorDTO usuarioResponseDTO = await _loginService.CreateAsync(usuarioCreateDTO);
                return Ok(usuarioResponseDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Authenticate")]
        public async Task<ActionResult<AuthenticatedDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                AuthenticatedDTO user = await _loginService.LoginAsync(loginDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
