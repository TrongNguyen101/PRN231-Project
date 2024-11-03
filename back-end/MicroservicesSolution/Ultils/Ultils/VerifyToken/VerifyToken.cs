
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ultils.VerifyToken
{
    public class VerifyToken : IVerifyToken
    {
        private readonly IConfiguration configuration;
        public VerifyToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<Boolean> VerifyTokenAsync(dynamic jwtToken)
        {
            var JwtSettings = configuration.GetSection("JwtSettings");
            var parts = jwtToken.Split(".");
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid token");
            }
            var encodedHeader = parts[0];
            var encodedPayload = parts[1];

            var tokenData = $"{encodedHeader}.{encodedPayload}";

            var endcodedKey = Encoding.UTF8.GetBytes(JwtSettings["SecretKey"]);
            var signingKey = new SymmetricSecurityKey(endcodedKey);

            using var hmac = new HMACSHA256(signingKey.Key);
            var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(tokenData));
            var signatureEncoded = Base64UrlEncoder.Encode(signatureBytes);

            var resigningJwtToken = $"{tokenData}.{signatureEncoded}";

            if (jwtToken != resigningJwtToken)
            {
                return false;
            }
            return true;
        }
    }
}