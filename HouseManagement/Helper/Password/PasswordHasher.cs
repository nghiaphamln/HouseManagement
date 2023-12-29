using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Helper.Password;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        var saltKey = AppSettings.Get("SaltKey") ?? "";
        var salt = Encoding.UTF8.GetBytes(saltKey);
        var passwordHashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return passwordHashed;
    }

    public bool Compare(string password, string hashedPassword)
    {
        return Hash(password) == hashedPassword;
    }
}