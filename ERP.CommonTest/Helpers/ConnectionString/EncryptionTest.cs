using ERP.Common.Helpers.ConnectionString;
using NUnit.Framework;
using System;
using Unity;

namespace ERP.CommonTest.Helpers.ConnectionString
{
    [TestFixture]
    public class EncryptionTest
    {
        IUnityContainer container;

        public EncryptionTest()
        {
            container = new UnityContainer();
        }

        [OneTimeSetUp]
        public void InitializeContainer()
        {
            container.RegisterConnectionStringEncryption();
        }

        [Test]
        public void EncryptDoesntThrow()
        {
            IEncryptor? encryptor = container.Resolve<IEncryptor>();

            string textToCipher = Guid.NewGuid().ToString();
            _ = encryptor.Encrypt(textToCipher);
        }

        [Test]
        public void DecryptDoesntThrow()
        {
            IDecryptor decryptor = container.Resolve<IDecryptor>();
            IEncryptor? encryptor = container.Resolve<IEncryptor>();

            string textToCipher = Guid.NewGuid().ToString();
            string cipheredText = encryptor.Encrypt(textToCipher);
            _ = decryptor.Decrypt(cipheredText);
        }

        [Test]
        public void EncriptThenDecryptReturnsInitialValue()
        {
            IDecryptor decryptor = container.Resolve<IDecryptor>();
            IEncryptor? encryptor = container.Resolve<IEncryptor>();

            string expected = Guid.NewGuid().ToString();
            string cipheredText = encryptor.Encrypt(expected);
            string actual = decryptor.Decrypt(cipheredText);

            Assert.AreEqual(expected, actual);
        }
    }
}
