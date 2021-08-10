using System;
using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Event.Helpers
{
    public class AesGcmService : IDisposable
    {
        private readonly AesGcm _aes;

        public AesGcmService()
        {
            // Derive key
            // AES key size is 16 bytes
            // We use a fixed salt and small iteration count here; the latter should be increased for weaker passwords
            var password = "Event";
            var key = new Rfc2898DeriveBytes(password, new byte[8], 1000).GetBytes(16);

            // Initialize AES implementation
            _aes = new AesGcm(key);
        }

        public void Dispose()
        {
            _aes.Dispose();
        }

        public string Encrypt(string plain)
        {
            // Get bytes of plaintext string
            var plainBytes = Encoding.UTF8.GetBytes(plain);

            // Get parameter sizes
            var nonceSize = AesGcm.NonceByteSizes.MaxSize;
            var tagSize = AesGcm.TagByteSizes.MaxSize;
            var cipherSize = plainBytes.Length;

            // We write everything into one big array for easier encoding
            var encryptedDataLength = 4 + nonceSize + 4 + tagSize + cipherSize;
            var encryptedData = encryptedDataLength < 1024
                ? stackalloc byte[encryptedDataLength]
                : new byte[encryptedDataLength].AsSpan();

            // Copy parameters
            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(0, 4), nonceSize);
            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4), tagSize);
            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            // Generate secure nonce
            RandomNumberGenerator.Fill(nonce);

            // Encrypt
            _aes.Encrypt(nonce, plainBytes.AsSpan(), cipherBytes, tag);

            // Encode for transmission
            return Convert.ToBase64String(encryptedData);
        }

        public string Decrypt(string cipher)
        {
            // Decode
            var encryptedData = Convert.FromBase64String(cipher).AsSpan();

            // Extract parameter sizes
            var nonceSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(0, 4));
            var tagSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4));
            var cipherSize = encryptedData.Length - 4 - nonceSize - 4 - tagSize;

            // Extract parameters
            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            // Decrypt
            var plainBytes = cipherSize < 1024 ? stackalloc byte[cipherSize] : new byte[cipherSize];
            _aes.Decrypt(nonce, cipherBytes, tag, plainBytes);

            // Convert plain bytes back into string
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}