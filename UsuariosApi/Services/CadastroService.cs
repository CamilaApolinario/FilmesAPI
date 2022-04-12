using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private EmailService _emailService;
        
        public CadastroService(IMapper mapper, UserManager<CustomIdentityUser> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            
        }

        public async Task<Result> CadastraUsuario(CreateUsuarioDto createDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createDto); //converte um CreateUsuarioDto para nosso modelo de Usuario, usando AutoMapper
            
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario); //converte o Usuario para um IdentityUser, usando o AutoMapper
            
            IdentityResult resultadoIdentity = await _userManager
                .CreateAsync(usuarioIdentity, createDto.Password); // cadastra no banco o usuarioIdentity, usando um gerenciador de usuarios, UserManager. Quando executa essa tarefa de criação assincrona, ela executa uma tarefa com um resultado de sucesso ou não. 
          
            if (resultadoIdentity.Succeeded) 
            {
                var code = _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity)
                    .Result;// nossa tarefa vai aguardar o resultado com sucesso e a geração do codigo, que será fornecido para o usuario confirmar sua identidade.
                
                var encodedCode = HttpUtility.UrlEncode(code);

                _emailService.EnviarEmail(new [] { usuarioIdentity.Email}, 
                    "Link de Ativação", usuarioIdentity.Id, encodedCode); //logica para enviar email para o usuario cadastrado, lista de emails, como id, e o codigo de ativação
                
                return Result.Ok().WithSuccess(code);
            }
            
            return Result.Fail("Falha ao cadastrar usuário");

        }

        public async Task<Result> AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId); //recupera o usuario que foi recebido na requisição

            var identityResult = await _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao); //recebe o usuario e o codigo de ativação e aguarda o resultado 
            
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }

            return Result.Fail("Falha ao ativar conta de usuario");
        }
    }
}
