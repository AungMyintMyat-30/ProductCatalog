using Microsoft.EntityFrameworkCore.Metadata.Internal;

using ProductCatalogCore.Interfaces;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProductCatalogInfrastructure.Services
{
    public class Cryptography : ICryptography
    {
        public string EncryptAES(string plainText)
        {
            byte[] keybytes = Encoding.UTF8.GetBytes("8945603788714414");
            byte[] iv = Encoding.UTF8.GetBytes("8945603788714414");

            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                return string.Empty;
            }
            if (keybytes == null || keybytes.Length <= 0)
            {
                return string.Empty;
            }
            if (iv == null || iv.Length <= 0)
            {
                return string.Empty;
            }

            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using var aesAlg = Aes.Create();
            //Settings
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.FeedbackSize = 128;
            aesAlg.Key = keybytes;
            aesAlg.IV = iv;
            //// Create a encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new())
            {
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {

                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }

            return Convert.ToBase64String(encrypted);
        }

        public string DecryptAES(string cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                return string.Empty;
            }

            byte[] keybytes = Encoding.UTF8.GetBytes("8945603788714414");
            byte[] iv = Encoding.UTF8.GetBytes("8945603788714414");
            //DECRYPT FROM CRIPTOJS
            byte[] encrypted = Convert.FromBase64String(cipherText);

            if (keybytes == null || keybytes.Length <= 0)
            {
                return string.Empty;
            }
            if (iv == null || iv.Length <= 0)
            {
                return string.Empty;
            }
            // Declare the string used to hold
            // the decrypted text.
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using var aesAlg = Aes.Create();
            //Settings
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.FeedbackSize = 128;
            aesAlg.Key = keybytes;
            aesAlg.IV = iv;
            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for decryption.
            using MemoryStream msDecrypt = new(encrypted);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            return srDecrypt.ReadToEnd(); ;
        }

        public string EncryptAES_HEX(string plainText)
        {
            byte[] keybytes = Encoding.UTF8.GetBytes("8945603788714414");
            byte[] iv = Encoding.UTF8.GetBytes("8945603788714414");

            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                return string.Empty;
            }
            if (keybytes == null || keybytes.Length <= 0)
            {
                return string.Empty;
            }
            if (iv == null || iv.Length <= 0)
            {
                return string.Empty;
            }

            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using var aesAlg = Aes.Create();
            //Settings
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.FeedbackSize = 128;
            aesAlg.Key = keybytes;
            aesAlg.IV = iv;
            //// Create a encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new())
            {
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {

                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }

            return Convert.ToHexString(encrypted);
        }

        public string DecryptAES_HEX(string cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                return string.Empty;
            }

            byte[] keybytes = Encoding.UTF8.GetBytes("8945603788714414");
            byte[] iv = Encoding.UTF8.GetBytes("8945603788714414");
            //DECRYPT FROM CRIPTOJS
            byte[] encrypted = Convert.FromHexString(cipherText);

            if (keybytes == null || keybytes.Length <= 0)
            {
                return string.Empty;
            }
            if (iv == null || iv.Length <= 0)
            {
                return string.Empty;
            }
            // Declare the string used to hold
            // the decrypted text.
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using var aesAlg = Aes.Create();
            //Settings
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.FeedbackSize = 128;
            aesAlg.Key = keybytes;
            aesAlg.IV = iv;
            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for decryption.
            using MemoryStream msDecrypt = new(encrypted);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            return srDecrypt.ReadToEnd(); ;
        }
    }
}
