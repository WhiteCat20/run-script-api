using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace run_script.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration; // from appsettings
        }
        public string CreateJWTToken(IdentityUser user, List<string> Roles)
        {
            // Create claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])); // from appsettings
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],// from appsettings
                configuration["Jwt:Audience"],// from appsettings
                claims, // all claims from above
                expires: DateTime.Now.AddMinutes(15), // expired berapa menit
                signingCredentials: credentials // credentials from above
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
