using Eum.Core.Data;
using System.Security.Cryptography;
using System.Text;

namespace Eum.gRPC.Server.Auth.Modules.Token.Services
{
    public interface IHashService : IService
    {
        string GetHashString(string inputString);

        bool IsMatchTwoHashString(string a, string b);
    }

    public class HashService : IHashService
    {
        private readonly HashAlgorithmName _algorithmName = HashAlgorithmName.SHA256;

        public string GetHashString(string inputString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                string base64String = Convert.ToBase64String(hashBytes);
                return base64String;
            }
        }

        public bool IsMatchTwoHashString(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
