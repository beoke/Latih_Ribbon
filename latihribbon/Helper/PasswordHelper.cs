using Konscious.Security.Cryptography;
using System;
using System.Security.Cryptography;
using System.Text;

namespace latihribbon.Helper
{
    /// <summary>
    /// Helper class untuk operasi hashing password menggunakan Argon2id.
    /// Dipindahkan dari FormUser_RiwayatLogin agar dapat digunakan lintas layer (DAL, UI).
    /// </summary>
    public static class PasswordHelper
    {
        private const int DegreeOfParallelism = 4;
        private const int MemorySize = 32768; // 32 MB
        private const int Iterations = 2;
        private const int HashLength = 32;
        private const int SaltLength = 16;

        /// <summary>
        /// Hash password menggunakan Argon2id dengan salt acak.
        /// Format output: "base64(salt):base64(hash)"
        /// </summary>
        public static string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                MemorySize = MemorySize,
                Iterations = Iterations
            };

            byte[] hashBytes = argon2.GetBytes(HashLength);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifikasi password terhadap hash yang tersimpan.
        /// </summary>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) return false;

            var parts = hashedPassword.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt;
            byte[] hashToCompare;
            try
            {
                salt = Convert.FromBase64String(parts[0]);
                hashToCompare = Convert.FromBase64String(parts[1]);
            }
            catch
            {
                return false;
            }

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                MemorySize = MemorySize,
                Iterations = Iterations
            };

            byte[] hashBytes = argon2.GetBytes(HashLength);

            // Constant-time comparison untuk mencegah timing attack
            return CryptographicEquals(hashBytes, hashToCompare);
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Perbandingan byte array dalam waktu konstan (mencegah timing attack).
        /// </summary>
        private static bool CryptographicEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int result = 0;
            for (int i = 0; i < a.Length; i++)
                result |= a[i] ^ b[i];
            return result == 0;
        }
    }
}
