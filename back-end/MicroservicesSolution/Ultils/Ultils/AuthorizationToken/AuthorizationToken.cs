
namespace Ultils.AuthorizationToken
{
    public class AuthorizationToken : IAuthorizationToken
    {
        public async Task<Boolean> AuthorizationAsync(dynamic jwtToken, string role)
        {
            var userClaim = JwtDecoder.DecodedPayload<UserClaim>(jwtToken);
            var roleClaim = userClaim.UserRole;
            if (roleClaim != role.ToLower())
            {
                return false;
            }
            return true;
        }
    }
}