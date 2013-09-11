using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility
{
    public class Cryptography
    {
    
      public static string EncryptPwd(string pwd)
      {
          byte[] salt = Encoding.ASCII.GetBytes("CaraMPAA");
          Rfc2898DeriveBytes key = new Rfc2898DeriveBytes("Victory", salt);

          using (Aes aesAlg = Aes.Create())
          {

              aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
              aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

              
              // Create a decryptor to perform the stream transform.
              ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

              // Create the streams used for encryption. 
              using (MemoryStream msEncrypt = new MemoryStream())
              {
                  using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                  {
                      using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                      {

                          //Write all data to the stream.
                          swEncrypt.Write(pwd);
                      }
                      byte[] encrypted = msEncrypt.ToArray();
                      return Convert.ToBase64String(encrypted);
                  }
              }
          }
      }
    

    }
}
