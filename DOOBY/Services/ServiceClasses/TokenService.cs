using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DOOBY.Services.ServiceClasses
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email, string type)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            //var secretKey = Encoding.ASCII.GetBytes("Token_Key");
            var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Token_Key"] 
                  ?? "$#$##$@#$#@$FDFDSFSAFSDFSADFDSFSAFAFDFS");
            //var secretKey = Encoding.ASCII.GetBytes("This Token is the JWT Token.....");

            var issuer = jwtSettings["Issuer"];
            // var audience = jwtSettings["Audience"];
            //double min = Convert.ToDouble("ExpiresIn");
            //var expires = DateTime.UtcNow.AddMinutes(min);
            var expirationTime = DateTime.UtcNow.AddMinutes(50);
            // Convert.ToDouble(_configuration["JwtSettings:ExpiresIn"]));

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, type),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(expirationTime).ToUnixTimeSeconds().ToString())
            };

            //var keyBytes = new byte[64];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(keyBytes);
            //}

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Expires = expirationTime,
                //  Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
