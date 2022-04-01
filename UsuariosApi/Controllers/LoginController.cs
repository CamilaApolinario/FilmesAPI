using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {       
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogaUsuario(LoginRequest request) // requisição para fazer um login
        {
            Result resultado = _loginService.LogaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors); // se falhar, o request não terá autorização para fazer o login
            return Ok(resultado.Successes); // se o resultado não falhou retorna com seus sucessos, se falhar retorna Unauthorized
        }

    }
}
