

namespace Utilities.Security
{
    using System.Web.Helpers;
    public class Encryption
    {
        public static string EncryptingPassword(string password)
        {
            return Crypto.HashPassword(Crypto.SHA256(password));
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, Crypto.SHA256(password));
        }
    }
}