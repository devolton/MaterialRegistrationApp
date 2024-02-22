using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MaterialLoginWpfApp;

   public static class Sha256Encrypter
    {
    public static string EncryptPassword(string password)
    {
        using var _sha256 = SHA256.Create();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashPasswordBytes = _sha256.ComputeHash(passwordBytes);
        _sha256.Dispose();
        return BitConverter.ToString(hashPasswordBytes);
    }
}

