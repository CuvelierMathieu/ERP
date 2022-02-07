using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ERP.Common.Helpers.ConnectionString
{
    internal class Decryptor : IDecryptor
    {
        private const string key = "jk8XKQS5L7cFVdfU";

        public string Decrypt(string value)
        {
            byte[] initialisationVector = new byte[16];
            byte[] buffer = Convert.FromBase64String(value);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = initialisationVector;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new(buffer))
                using (CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read))
                using (StreamReader streamReader = new(cryptoStream))
                return streamReader.ReadToEnd();
            }
        }
    }
}
