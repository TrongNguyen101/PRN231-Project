using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Ultils.AuthorizationToken
{
    public class JwtDecoder
    {
        public static T DecodedPayload<T>(dynamic JwtToken) where T : class
        {
            var parts = JwtToken.Split('.');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid JWT format");
            }
            var payload = parts[1];
            var jsonBytes = Base64UrlEncoder.DecodeBytes(payload);
            var jsonString = Encoding.UTF8.GetString(jsonBytes);
            var result = JsonConvert.DeserializeObject<T>(jsonString);

            return result;
        }
    }
}