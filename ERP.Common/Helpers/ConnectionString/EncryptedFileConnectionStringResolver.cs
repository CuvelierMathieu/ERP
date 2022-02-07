using System;
using System.IO;

namespace ERP.Common.Helpers.ConnectionString
{
    public class EncryptedFileConnectionStringResolver : IConnectionStringResolver
    {
        private readonly string filePath;
        private string? connectionString;
        private string? fileContent;

        public EncryptedFileConnectionStringResolver(string filePath)
        {
            this.filePath = filePath;
        }

        public void Dispose()
        {
            connectionString = null;
        }

        public string GetConnectionString()
        {
            ReadFile();
            Decrypt();

            return connectionString ?? throw new InvalidOperationException($"Couldn't read connection string from file at {filePath}");
        }

        private void ReadFile()
        {
            fileContent = File.ReadAllText(filePath) ?? throw new InvalidOperationException($"Couldn't read file at {filePath}");
        }

        private void Decrypt()
        {
            Decryptor decryptor = new();
            connectionString = decryptor.Decrypt(fileContent ?? throw new InvalidOperationException($"Couldn't read file at {filePath}"));
        }
    }
}
