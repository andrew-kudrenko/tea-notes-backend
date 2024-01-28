using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TeaNotes.Users.Models;

namespace TeaNotes.Auth.Jwt
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();
        private readonly SigningCredentials _signingCredentials;
        private readonly int _tokenLifetime;
        private readonly string _audience;
        private readonly string _issuer;
        private readonly double _refreshLifetime;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;

            _tokenLifetime = int.Parse(_configuration["Jwt:AccessLifetimeInMinutes"]!);
            _issuer = _configuration["Jwt:Issuer"]!;
            _audience = _configuration["Jwt:Audience"]!;
            _refreshLifetime = double.Parse(_configuration["Jwt:RefreshLifetimeInDays"]!);

            _signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!)),
                SecurityAlgorithms.HmacSha256
            );
        }

        public (string, DateTime) GenerateRefreshToken() => (Guid.NewGuid().ToString(), DateTime.Now.AddMinutes(1));

        public (string, DateTime) GenerateAccessToken(User user)
        {
            var expires = DateTime.Now.AddSeconds(15);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: new Claim[] { new(ClaimTypes.Name, user.NickName) },
                expires: expires,
                signingCredentials: _signingCredentials
            );

            return (_tokenHandler.WriteToken(token), expires);
        }
    }
}
