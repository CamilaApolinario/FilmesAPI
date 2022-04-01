using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsuariosApi.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int> // definindo o uso de um banco de dados e uso do Identity, isso precisa ser configurado no Startup
    {
        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        {
        }
        
    }
}
