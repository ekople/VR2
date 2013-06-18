using System.Security.Cryptography;
using System.Text;
using System;
using System.Web.Mvc;

namespace PiimaToostusKlient.Utils
{
    public class Utils
    {
        public static string GenerateSaltedHash(string userName, string pssWd)
        {
            if (userName ==null || pssWd == null)
            {
                return null;
            }
            byte[] userNameBytes = GetBytes(userName);
            byte[] pssWdBytes = GetBytes(pssWd);
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[userNameBytes.Length + pssWdBytes.Length];

            for (int i = 0; i < userNameBytes.Length; i++)
            {
                plainTextWithSaltBytes[i] = userNameBytes[i];
            }
            for (int i = 0; i < pssWdBytes.Length; i++)
            {
                plainTextWithSaltBytes[userNameBytes.Length + i] = pssWdBytes[i];
            }

            byte[] hash = algorithm.ComputeHash(plainTextWithSaltBytes);
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sBuilder.Append(hash[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetKuvatavDate(DateTime? inputDateTime)
        {
            if (inputDateTime == null)
            { return string.Empty; }
            return inputDateTime.Value.ToString("dd.MM.yyyy"); 
        }
    }
}