namespace Repositories.HashAlgorithmRepository
{
    public interface IHashAlgorithmRepository
    {
        public string HashPassword(string password);
    }
}
