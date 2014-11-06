using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Crypter
    {
        public static string EncryptPassword(string textPassword)
        {
            //Crypter le mot de passe          
            byte[] passBytes = System.Text.Encoding.Unicode.GetBytes(textPassword);
            string encryptPass = Convert.ToBase64String(passBytes);
            return encryptPass;
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            //Decrypter le mot de passe    
            byte[] passByteData = Convert.FromBase64String(encryptedPassword);
            string originalPassword = System.Text.Encoding.Unicode.GetString(passByteData);
            return originalPassword;
        }
    }
}
