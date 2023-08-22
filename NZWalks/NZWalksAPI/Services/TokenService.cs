using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZWalksAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalksAPI.Services
{
    public class TokenService : ITokenRepository
    {
        private readonly IConfiguration configuration;
        public TokenService(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        public string? CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!)
            };

            foreach (var role in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            string? jwtToken = configuration["Jwt:Key"];
            string? jwtIssuer = configuration["Jwt:Issuer"];
            string? jwtAudiance = configuration["Jwt:Audience"];

            if (jwtToken != null && jwtIssuer != null && jwtAudiance != null) 
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (jwtIssuer, 
                     jwtAudiance, 
                     claims, 
                     expires: DateTime.Now.AddMinutes(15), 
                     signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }
    }
}
