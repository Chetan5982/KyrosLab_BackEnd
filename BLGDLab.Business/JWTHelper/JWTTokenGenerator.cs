using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLGDLab.Business.JWTHelper
{
    public static class JWTTokenGenerator
    {
        // Build configuration from appsettings.json
        private static readonly IConfiguration _configuration =
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        public static string GenerateToken(dynamic user)
        {
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];

            if (string.IsNullOrEmpty(key))
                throw new Exception("JWT Key is missing in appsettings.json");

            if (string.IsNullOrEmpty(issuer))
                throw new Exception("JWT Issuer is missing in appsettings.json");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName?.ToString() ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email?.ToString() ?? string.Empty),
                new Claim(ClaimTypes.MobilePhone, user.Phone?.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Role, user.RoleId == 2 ? "Admin" : "User")
            };

            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
