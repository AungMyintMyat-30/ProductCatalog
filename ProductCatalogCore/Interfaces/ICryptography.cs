using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogCore.Interfaces
{
    public interface ICryptography
    {
        /// <summary>
        /// Encrypt AES (To Base64String)
        /// </summary>
        /// <remarks>
        /// Cryptography Encrytion
        /// Using RijndaelManaged Encrypt mode
        /// <ul><br/>
        /// <li>Mode -> CipherMode.CBC</li>
        /// <li>Padding -> PaddingMode.PKCS7</li>
        /// <li>FeedbackSize -> 128</li>
        /// </ul>
        /// </remarks>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptAES(string plainText);

        /// <summary>
        /// Decrypt AES (From Base64String)
        /// </summary>
        /// <remarks>
        /// Cryptography Encrytion
        /// Using RijndaelManaged Encrypt mode
        /// <ul>
        /// <li>Mode -> CipherMode.CBC</li>
        /// <li>Padding -> PaddingMode.PKCS7</li>
        /// <li>FeedbackSize -> 128</li>
        /// </ul>
        /// </remarks>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string DecryptAES(string cipherText);

        /// <summary>
        /// Encrypt AES (To HEX String)
        /// </summary>
        /// <remarks>
        /// Cryptography Encrytion
        /// Using RijndaelManaged Encrypt mode
        /// <ul>
        /// <li>Mode -> CipherMode.CBC</li>
        /// <li>Padding -> PaddingMode.PKCS7</li>
        /// <li>FeedbackSize -> 128</li>
        /// </ul>
        /// </remarks>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptAES_HEX(string plainText);

        /// <summary>
        /// Decrypt AES (From HEX String)
        /// </summary>
        /// <remarks>
        /// Cryptography Encrytion
        /// Using RijndaelManaged Encrypt mode
        /// <ul>
        /// <li>Mode -> CipherMode.CBC</li>
        /// <li>Padding -> PaddingMode.PKCS7</li>
        /// <li>FeedbackSize -> 128</li>
        /// </ul>
        /// </remarks>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string DecryptAES_HEX(string cipherText);
    }
}
