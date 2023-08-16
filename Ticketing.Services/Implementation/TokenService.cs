
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticketing.BuisinessLayer.Interface;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;

namespace Ticketing.BuisinessLayer.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;

        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public JwtSecurityToken GenerateJsonWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtOptions.Issuer,
                _jwtOptions.Audience,
                GetClaims(user),
                expires: DateTime.Now.AddMinutes(_jwtOptions.TokenValidityInMinutes),
                signingCredentials: credentials);

            return token;
        }



        #region private Methods
        private IEnumerable<Claim> GetClaims(User user)
        {
            try
            {
                DateTime RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOptions.RefreshTokenValidityInDays);

                var claims = new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("DisplayName", user.Name),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email),
                        new Claim("RefreshTokenExpiryTime", RefreshTokenExpiryTime.ToString("yyyy-MM-dd HH:mm:ss"))

                    };
                return claims;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


    }
}