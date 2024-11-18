using System.Text;
using System.Security.Cryptography;

namespace DatingSiteTeamProject.Helpers
{
    public class Hasher
    { 
            public static string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
            }

            public static bool VerifyPassword(string enteredPassword, string storedHash)
            {
                string enteredPasswordHash = HashPassword(enteredPassword);
                return enteredPasswordHash == storedHash;
            }
    }
}
