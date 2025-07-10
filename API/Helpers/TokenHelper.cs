using Core.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Helpers
{
    public static class TokenHelper
    {
        public static SymmetricSecurityKey GenerateKey(IConfiguration configuration)
        {
            string key = configuration.GetSection("JWT").GetValue<string>("SecretKey");
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        public static string GenerateToken(SymmetricSecurityKey key, string audience, string issuer, IList<Claim> claims)
        {

            var token = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                audience: audience,
                issuer: issuer,
                expires: DateTime.Now.AddDays(30),
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static async Task<string> CreateTokenResponse(User user, UserManager<User> userManager, IConfiguration configuration)
        {
            var claims = await userManager.GetClaimsAsync(user);
            var audience = configuration.GetSection("JWT").GetValue<string>("Audience");
            var issuer = configuration.GetSection("JWT").GetValue<string>("Issuer");

            var key = GenerateKey(configuration);

            return GenerateToken(key, audience, issuer, claims);


        }
    }
}
