using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ERP.Common.Helpers.ConnectionString
{
    internal class Encrypter : IEncryptor
    {
        private const string key = "jk8XKQS5L7cFVdfU";

        public string Encrypt(string value)
        {
            byte[] initialisationVector = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = initialisationVector;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new())
                using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new(cryptoStream))
                        streamWriter.Write(value);

                    array = memoryStream.ToArray();
                }
            }

            return Convert.ToBase64String(array);
        }
    }
}
