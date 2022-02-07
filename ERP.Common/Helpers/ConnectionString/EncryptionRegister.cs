using Unity;

namespace ERP.Common.Helpers.ConnectionString
{
    public static class EncryptionRegister
    {
        public static IUnityContainer RegisterConnectionStringEncryption(this IUnityContainer container)
        {
            return container.RegisterType<IDecryptor, Decryptor>()
                            .RegisterType<IEncryptor, Encrypter>();
        }
    }
}
