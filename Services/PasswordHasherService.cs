using System.Security.Cryptography;
using System.Text;
using csharp_url_shortener_api.Interfaces.Services;
using Konscious.Security.Cryptography;

namespace csharp_url_shortener_api.Services;

public class PasswordHasherService : IPasswordHasherService
{
    private const int SaltSize = 16; // 128 bits

    private const int HashSize = 32; // 256 bits

    private const int DegreeOfParallelism = 8; // Number of threads to use

    private const int Iterations = 4; // Number of iterations

    private const int MemorySize = 64 * 1024; // 64MB minimum   
    
    public string HashPassword(string password)
    {
        // Generate a random salt
        var salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Create hash
        var hash = HashPassword(password, salt);

        // Combine salt and hash
        var combinedBytes = new byte[salt.Length + hash.Length];
        Array.Copy(salt, 0, combinedBytes, 0, salt.Length);
        Array.Copy(hash, 0, combinedBytes, salt.Length, hash.Length);

        // Convert to base64 for storage
        return Convert.ToBase64String(combinedBytes);
    }


    public bool VerifyPassword(string password, string hashedPassword)
    {
        // Decode the stored hash
        var combinedBytes = Convert.FromBase64String(hashedPassword);

        // Extract salt and hash
        var salt = new byte[SaltSize];
        var hash = new byte[HashSize];
        Array.Copy(combinedBytes, 0, salt, 0, SaltSize);
        Array.Copy(combinedBytes, SaltSize, hash, 0, HashSize);

        // Compute hash for the input password
        var newHash = HashPassword(password, salt);

        // Compare the hashes
        return CryptographicOperations.FixedTimeEquals(hash, newHash);
    }

    private byte[] HashPassword(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            Iterations = Iterations,
            MemorySize = MemorySize
        };

        return argon2.GetBytes(HashSize);
    }
}