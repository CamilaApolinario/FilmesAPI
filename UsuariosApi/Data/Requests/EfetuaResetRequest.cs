using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    public class EfetuaResetRequest
    {
        [Required]
        [DataType(DataType.Password)] //informa que é um tipo de senha
        public string Password { get; set; } //senha nova

        [Required]
        [DataType(DataType.Password)] 
        [Compare("Password")] // faz a comparação entre as duas senhas digitadas
        public string RePassword { get; set; } //confirma se digitou a senha certa
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Token { get; set; } // enviar o token para garantir que é a mesma pessoa 
    }
}
