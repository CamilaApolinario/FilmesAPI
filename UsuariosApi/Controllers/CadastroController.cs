using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        private readonly CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = await _cadastroService.CadastraUsuario(createDto);
            
            if (resultado.IsFailed) 
                return StatusCode(500);

            return Ok(resultado.Successes);// retorna o codigo de ativação
        }

        [HttpGet("/ativa")]
        public async Task<IActionResult> AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            Result resultado = await _cadastroService.AtivaContaUsuario(request);
            
            if (resultado.IsFailed) 
                return StatusCode(500);

            return Ok(resultado.Successes);
        }
    }
}
