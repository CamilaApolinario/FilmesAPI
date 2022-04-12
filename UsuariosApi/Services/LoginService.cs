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
        private SignInManager<CustomIdentityUser> _signInManager; // gerenciador de login
        private TokenService _tokenService;
        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
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
                
                Token token = _tokenService
                    .CreateToken(identityUser, _signInManager
                    .UserManager.GetRolesAsync(identityUser)
                    .Result.FirstOrDefault());// no momento de criação do token passa qual a role desse usuario
                
                return Result.Ok().WithSuccess(token.Value); //retorna sucesso com o valor do token
            }
                
            return Result.Fail("Login falhou!");
        }

        public Result ResetaSenhaUsuario(EfetuaResetRequest request)//recupera usuario
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);

            IdentityResult resultadoIdentity = _signInManager
                .UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password)//passando usuario, token, e a nova senha
                .Result;

            if (resultadoIdentity.Succeeded)
                return Result.Ok().WithSuccess("Senha redefinida com sucesso!");

            return Result.Fail("Houve um erro na operação");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request) // faz a solicitação e gera um novo token
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email); // recupera o usuario por meio do email e compara com o recebido por requisição.

            if (identityUser != null)
            {
                string codigoDeRecuperacao = _signInManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser)
                    .Result;
                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }      
            return Result.Fail("Falha ao solicitar redefinição");
        }

        private CustomIdentityUser RecuperaUsuarioPorEmail(string email)
        {
            return _signInManager
                            .UserManager
                            .Users
                            .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());//NormalizeEmail, é o email em caixa alta, ToUpper transforma o request em caixa alta 
        }
    }    
}
