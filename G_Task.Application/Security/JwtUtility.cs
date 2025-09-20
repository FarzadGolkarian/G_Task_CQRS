using G_Task.Application.DTOs.Account;
using G_Task.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace G_Task.Application.Security
{
    public class JwtUtility
    {
        private readonly IConfiguration _configuration;

        public JwtUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LoginAccountDto GenerateToken(HttpRequest request, GetLoginDto LoginDto)
        {
            var secretKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                                                            SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_configuration["Jwt:EncryptionKey"]);

            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey),
                                                                  SecurityAlgorithms.Aes128KW,
                                                                  SecurityAlgorithms.Aes128CbcHmacSha256);

            Microsoft.Extensions.Primitives.StringValues val;

            request.Headers.TryGetValue(HeaderNames.Authorization, out val);

            var role = ((int)ClientUserTypeEnum.Employee).ToString();

            var claims = new List<Claim>
        {
            new(ClaimTypes.Name,LoginDto.UserName),
            new(ClaimTypes.Actor,role),
            new(ClaimTypes.Role,ClientUserTypeEnum.Employee.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
        };

            if (!string.IsNullOrEmpty(val))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.CHash, val));
            }
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(180),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var encryptedJwt = tokenHandler.WriteToken(securityToken);

            return new LoginAccountDto(LoginDto.UserName, encryptedJwt, securityToken.ValidTo);
        }
    }
}
