using Login_System.Core;
using Login_System.Core.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login_System.Api.Extensions
{
    public static class JwtExtension
    {
        public static string Generate(ResponseData data)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(data),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = credentials
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(ResponseData user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("Id", user.Id));
            claims.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            foreach (var role in user.Roles)
                claims.AddClaim(new Claim(ClaimTypes.Role, role));

            return claims;
        }
    }
}
