using System.Security.Cryptography;
using System.Text;

namespace Repositories.HashAlgorithmRepository
{
    public class HashAlgorithmRepository : IHashAlgorithmRepository
    {
        public string Hash256Algorithm(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var values = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                values.Append(bytes[i].ToString("x2"));
            }
            return values.ToString();
        }
    }
}
