namespace ERP.Common.Helpers.ConnectionString
{
    public interface IEncryptor
    {
        string Encrypt(string value);
    }
}
