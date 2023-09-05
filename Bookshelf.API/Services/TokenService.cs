using Bookshelf.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bookshelf.API.Services
{
    public class TokenService
    {
        public string GenerateToken(Reader reader)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(reader),
                SigningCredentials = signingCredentials,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        // Os Claims são as informações que estarão armazenadas no token.
        private ClaimsIdentity GenerateClaims(Reader reader)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, reader.UserName));
            claimsIdentity.AddClaim(new Claim("name_user", reader.Name));
            claimsIdentity.AddClaim(new Claim("user_image", reader.Image));

            return claimsIdentity;
        }
    }
}
