using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TeaNotes.Users.Models;
using TeaNotes.Auth.Models;

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

        public AuthToken GenerateRefreshToken()
        {
            return new() { ExpiresAt = DateTime.Now.AddDays(_refreshLifetime), Token = Guid.NewGuid().ToString() };
        }

        public AuthToken GenerateAccessToken(User user)
        {
            var expires = DateTime.Now.AddMinutes(_tokenLifetime);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: new Claim[] { new(ClaimTypes.Name, user.NickName) },
                expires: expires,
                signingCredentials: _signingCredentials
            );

            return new() { ExpiresAt = expires, Token = _tokenHandler.WriteToken(token) };
        }
    }
}
