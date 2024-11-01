namespace Ultils.AuthorizationToken
{
    public interface IAuthorizationToken
    {
        Task<Boolean> AuthorizationAsync(dynamic jwtToken, string role);
    }
}