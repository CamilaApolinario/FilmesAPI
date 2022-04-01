using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager; // gerenciador de login
        private TokenService _tokenService;
        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(request.UserName, request.Password, false, false); //sinInManager vai tentar autenticação via password(persistencia em fazer login, e bloquear caso o login tenha falhado.)
            
            if (resultadoIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager//para recuperar o identityUser que o token recebeu, o primeiro que encontrar
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario => //recuperra o usuario com o nome normalizado, em letras caixaalta recebee o request de usuario, e transforma em caixaalta
                    usuario.NormalizedUserName == request.UserName.ToUpper());
                
                Token token = _tokenService.CreateToken(identityUser);
                
                return Result.Ok().WithSuccess(token.Value); //retorna sucesso com o valor do token
            }
                
            return Result.Fail("Login falhou!");
        }
    }    
}
