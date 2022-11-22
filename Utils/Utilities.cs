using System.Security.Cryptography;
using System.Text;

namespace TreinamentoMarinho.Utils
{
    public class Utilities
    {
        public string GenerateHash(string login, string password)
        {
            var result = "";

            string concatPass = password + login;
            var passwordBytes = Encoding.UTF8.GetBytes(concatPass);
            using (SHA512 sha512 = SHA512.Create())
            {
                var resultBytes = sha512.ComputeHash(passwordBytes);
                var hashedBuilder = new StringBuilder(128);
                foreach (var item in resultBytes)
                {
                    hashedBuilder.Append(item.ToString("X2"));
                }
                result = hashedBuilder.ToString();
            }
            return result;
        }
    }
}
