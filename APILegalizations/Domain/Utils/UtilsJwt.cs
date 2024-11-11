using APILegalizations.Data.Context;
using APILegalizations.Data.Models;
using APILegalizations.Domain.Entities;
using JWT.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWT.Utils
{
    public class UtilsJwt
    {
        private readonly LegalizationContext _context; 
        private readonly IConfiguration _config;

        public UtilsJwt(LegalizationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string GenerateJwtToken(UserDomainRes user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _config["JWT:ValidIssuer"],
                _config["JWT:ValidAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["JWT:ExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public RefreshToken GenerateRefreshToken(UserDomainRes user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddMinutes(120),
                Created = DateTime.Now,
                UserId = user.UserId
            };

            _context.RefreshTokens.Add(refreshToken);
            return refreshToken;
        }
    }
}
