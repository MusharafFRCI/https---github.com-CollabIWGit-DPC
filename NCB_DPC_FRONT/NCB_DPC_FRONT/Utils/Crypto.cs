using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NCB_DPC_FRONT.Utils
{
    public class Crypto
    {
        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = null;
            string plainText = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(cipherText))
                {
                    cipherBytes = System.Convert.FromBase64String(cipherText);
                }

                // Create a new instance of the RijndaelManaged
                // class.  This generates a new key and initialization
                // vector (IV).
                using (RijndaelManaged rijAlg = new RijndaelManaged())
                {
                    var key = Encoding.UTF8.GetBytes(MaupassIntegration.Constants.DecryptKey);
                    var iv = Encoding.UTF8.GetBytes(MaupassIntegration.Constants.DecryptKey);

                    if (key == null || key.Length <= 0)
                    {
                        throw new ArgumentNullException("key");
                    }

                    if (iv == null || iv.Length <= 0)
                    {
                        throw new ArgumentNullException("iv");
                    }

                    //Settings  
                    rijAlg.Mode = CipherMode.CBC;
                    rijAlg.Padding = PaddingMode.PKCS7;
                    rijAlg.FeedbackSize = 128;

                    rijAlg.Key = key;
                    rijAlg.IV = iv;

                    // Create an encryptor to perform the stream transform.
                    // Create a decryptor to perform the stream transform.
                    ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plainText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return plainText;
        }
    }
}
