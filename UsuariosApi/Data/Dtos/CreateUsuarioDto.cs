using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)] // representa um valor de senha
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")] // para confirmar se a senha passada esta correta, ele faz uma comparação
        public string RePassword { get; set; }
    }
}
