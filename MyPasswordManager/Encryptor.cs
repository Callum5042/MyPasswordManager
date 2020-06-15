using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyPasswordManager
{
    public static class Encryptor
    {
        public static void Encrypt(string password, string data)
        {
            var (Key, IV) = GetKeyAndIv(password);

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var msEncrypt = File.OpenWrite("data.bin");
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(data);
        }

        public static string Decrypt(string password)
        {
            var (Key, IV) = GetKeyAndIv(password);

            using Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            // Create the streams used for decryption.
            using var msDecrypt = File.OpenRead("data.bin");
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);
            var str = srDecrypt.ReadToEnd();
            return str;
        }

        private static (byte[] Key, byte[] IV) GetKeyAndIv(string password)
        {
            SHA256 sha2 = new SHA256CryptoServiceProvider();

            byte[] rawKey = Encoding.UTF8.GetBytes(password);
            byte[] rawIV = Encoding.UTF8.GetBytes(password);

            byte[] hashKey = sha2.ComputeHash(rawKey);
            byte[] hashIV = sha2.ComputeHash(rawIV);

            Array.Resize(ref hashIV, 16);

            return (hashKey, hashIV);
        }
    }
}