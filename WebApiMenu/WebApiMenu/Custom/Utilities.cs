using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiRestaurant.Models;

namespace WebApiRestaurant.Custom
{
    public class Utilities
    {
        private readonly IConfiguration _conf;
        public Utilities(IConfiguration configuration)
        {
            _conf = configuration;
        }

        public string encryptSHA256(string plainText)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string generateJWT(User userModel)
        {
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
                new Claim(ClaimTypes.Email, userModel.Mail!),
                new Claim(ClaimTypes.Role, userModel.Role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
