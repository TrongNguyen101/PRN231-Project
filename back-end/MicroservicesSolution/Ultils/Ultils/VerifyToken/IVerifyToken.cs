namespace Ultils.VerifyToken
{
    public interface IVerifyToken
    {
        Task<Boolean> VerifyTokenAsync(dynamic jwtToken);
    }
}