using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Infrastructure
{
    public class JwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = [ new("userId", user.Id.ToString()) ];
            
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public Guid? ExtractUserIdFromToken(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    
                var tokenValidationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateLifetime = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
    
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out _);
                
                var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "userId");
                if (userIdClaim == null)
                    return null;
    
                if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                    return null;
    
                return userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token parsing error: {ex.Message}");
                return null;
            }
        }
    }
}