using System.Security.Cryptography;
using System.Text;

namespace APILegalizations.Domain.Utils
{
    public class Helper
    {
        public string encryptPassword(string token)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(token)));
        }
    }
}
