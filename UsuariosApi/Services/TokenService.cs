using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> usuario) // cria um modelo de token, e qual é o usuario que está recebendo
        {
            Claim[] direitosUsuario = new Claim[] //o que esse usuario exige//seus direitos
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
            };

            var chave = new SymmetricSecurityKey( // chave para criptografar o token
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256); // gera as credenciais apartir da chave e de um algoritimo de criptografia

            var token = new JwtSecurityToken( // gera o token, com as exigencias, segurança e credenciais, e o tempo que dura o token
                claims: direitosUsuario,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token); // transforna o token em uma string
            return new Token(tokenString);
        }
    }
}
