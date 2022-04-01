using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager; //recupera o IdentityUser

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }
        public Result DeslogaUsuario()
        {
            var resultadoIdentity = _signInManager.SignOutAsync(); 
            
            if (resultadoIdentity.IsCompletedSuccessfully) // se completou com sucesso, retorna ok, se não retorna falhou
            {
                return Result.Ok();
            }
            
            return Result.Fail("Logout falhou!");        
        }   
    }
}
