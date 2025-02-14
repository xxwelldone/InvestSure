using InvestSure.App.Interfaces;
using InvestSure.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestSure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAll()
        {
            try
            {
                return Ok(await _assetService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetById(Guid id)
        {
            try
            {
                return Ok(await _assetService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

    }
}
