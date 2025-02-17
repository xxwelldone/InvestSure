using InvestSure.App.Dtos;
using InvestSure.App.Interfaces;
using InvestSure.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestSure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        [HttpPost]
        public async Task<ActionResult<Investment>> BuyInvestment(InvetmentCreateDTO createDTO)
        {
            try
            {
                Investment investment = await _investmentService.Create(createDTO);
                return new CreatedAtRouteResult
                    (
                    routeName: "GetById",
                    routeValues: investment.Id,
                    value: investment
                    );

            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );

            }
        }
    }
}