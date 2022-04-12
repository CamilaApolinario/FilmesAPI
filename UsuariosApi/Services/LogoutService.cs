using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser> _signInManager; //recupera o IdentityUser

        public LogoutService(SignInManager<CustomIdentityUser> signInManager)
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
