namespace ERP.Common.Helpers.ConnectionString
{
    public interface IDecryptor
    {
        string Decrypt(string value);
    }
}
