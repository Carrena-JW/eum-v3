using Eum.Core.Shared.Repositories;
using Eum.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Shared.Services
{
    public interface IEncryptService : IService
    {
        string EncryptToString(string row);
        byte[] Encrypt(string row);
        string DecryptFromString(string row);
        string Decrypt(byte[] row);
    }
    public class AesEncryptoService : IEncryptService
    {
        private static string initialisationVector = "23888B78-F597-4245-AF4E-A2BDCFBFC4A3";
        private byte[] _key;
        private IAppConfigRepository _appConfigRepository;

        public AesEncryptoService(IAppConfigRepository appConfigRepository)
        {
            _appConfigRepository = appConfigRepository;
            _key = GetKey();
        }

        public string EncryptToString(string row)
        {
            return Convert.ToBase64String(Encrypt(row));
        }

        public byte[] Encrypt(string row)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            //set the mode for operation of the algorithm
            objrij.Mode = CipherMode.CBC;
            //set the padding mode used in the algorithm.
            objrij.Padding = PaddingMode.PKCS7;
            //set the size, in bits, for the secret key.
            objrij.KeySize = 0x80;
            //set the block size in bits for the cryptographic operation.
            objrij.BlockSize = 0x80;
            //set the symmetric key that is used for encryption & decryption.
            byte[] passBytes = _key;
            //set the initialization vector (IV) for the symmetric algorithm
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            //Creates symmetric AES object with the current key and initialization vector IV.
            ICryptoTransform objtransform = objrij.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(row);
            //Final transform the test string.
            return objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length);
        }

        public string DecryptFromString(string row)
        {
            byte[] encrypted = Convert.FromBase64String(row);
            return Decrypt(encrypted);
        }

        public string Decrypt(byte[] row)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            objrij.Mode = CipherMode.CBC;
            objrij.Padding = PaddingMode.PKCS7;
            objrij.KeySize = 0x80;
            objrij.BlockSize = 0x80;
            byte[] encryptedTextByte = row;
            byte[] passBytes = _key;
            byte[] EncryptionkeyBytes = new byte[0x10];
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(TextByte);  //it will return readable string
        }

        private byte[] GetKey()
        {
            var secret = _appConfigRepository.GetByKey("EncryptSecret");
            return Encoding.UTF8.GetBytes(secret.Value);
        }
    }
}
