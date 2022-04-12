using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using UsuariosApi.Models;

namespace UsuariosApi.Data
{
    public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int> // definindo o uso de um banco de dados e uso do Identity, isso precisa ser configurado no Startup
    {
        private IConfiguration _configuration;

        public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            CustomIdentityUser primeiroUser = new CustomIdentityUser //instancia um usuario padrão de administrador e suas caracteristicas
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN ",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(), //carimbar o momento de criação desse usuario Guid gerenciador de identificador unico, garante que securityStamp seja unico
                Id = 199999
            };

            var roleAdmin = new IdentityRole<int>
            {
                Id = 99999,
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            var roleRegular = new IdentityRole<int>
            {
                Id = 99997,
                Name = "regular",
                NormalizedName = "REGULAR"
            };

            var relacionamentoUserParaRole = new IdentityUserRole<int>
            {
                RoleId = 99999,
                UserId = 199999
            };
            

            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>(); // instancia um hasher, que manipula criptografias

            primeiroUser.PasswordHash = hasher.HashPassword(primeiroUser, 
                _configuration.GetValue<string>("admininfo:password")); //usando o hasher para criptografar senha usando um determinado usuario e atribuindo o resultado para uma prop. desse usuario
            
            
            
            builder.Entity<CustomIdentityUser>().HasData(primeiroUser); //semeia um usuario em memoria pra dentro de um usuario identityUser


            builder.Entity<IdentityRole<int>>().HasData(roleAdmin);// semeia uma permissão

            builder.Entity<IdentityRole<int>>().HasData(roleRegular);


            builder.Entity<IdentityUserRole<int>>().HasData(relacionamentoUserParaRole); //relacionamento entre um usuario e uma permissão





        }
    }
}
