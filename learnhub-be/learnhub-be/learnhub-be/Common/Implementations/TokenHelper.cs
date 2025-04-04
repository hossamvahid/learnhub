using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using learnhub_be.Common.Interfaces;
using learnhub_be.DataAccess.Models;
using Microsoft.IdentityModel.Tokens;

namespace learnhub_be.Common.Implementations
{
    public class TokenHelper : ITokenHelper
    {
        public static string GenerateJwtToken(Claim[] claim)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(

                issuer: Environment.GetEnvironmentVariable("ISSUER"),
                audience: Environment.GetEnvironmentVariable("AUDIENCE"),
                claims: claim,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials

                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(tokenDescriptor);
        }
        public static string GetUsername(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor).Value;
        }


        public static string GetRole(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c=>c.Type==ClaimTypes.Role).Value;
        }


    }
}
