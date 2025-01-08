using Microsoft.AspNetCore.Mvc;
using MimoFGTS.Content.Saque.DTO;
using MimoFGTS.Content.Saque.Interface;

namespace MimoFGTS.APIreturn.Saque
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaqueController : Controller
    {
        private readonly ISaqueService _saqueService;

        public SaqueController(ISaqueService saqueService)
        {
            _saqueService = saqueService;
        }

        [HttpPost("calcular-saque")]
        public async Task<IActionResult> CalcularSaque([FromBody] SaqueDTO saqueDTO)
        {
            var result = await _saqueService.CalcularSaque(saqueDTO);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.IsFailure);
        }

    }
}
