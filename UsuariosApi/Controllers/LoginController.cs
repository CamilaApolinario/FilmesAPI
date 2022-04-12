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
        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            Result resultado = _loginService.SolicitaResetSenhaUsuario(request);//recebe a solicitação de redefinição de senha
            if(resultado.IsFailed) return Unauthorized(resultado.Errors); // não autorizado e os erros 
            return Ok(resultado.Successes);
        }

        [HttpPost("/efetua-reset")]
        public IActionResult ResetaSenhaUsuario(EfetuaResetRequest request)// efetua a redefinição
        {
            Result resultado = _loginService.ResetaSenhaUsuario(request);
            if(resultado.IsFailed) return Unauthorized(resultado.Errors); 
            return Ok(resultado.Successes);
        }
    }
}
