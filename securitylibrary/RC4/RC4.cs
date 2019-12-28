using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RC4
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class RC4 : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            StringBuilder ct = new StringBuilder(cipherText);
            //key generated
            char[] k = new char[cipherText.Length];
            var s = new int[256];
            var t = new int[256];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = i;
            }
            for (int i = 0; i < s.Length; i++)
            {
                t[i] = key[i % key.Length];
            }

            int tmp;
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + s[i] + t[i]) % 256;
                tmp = s[i];
                s[i] = s[j];
                s[j] = tmp;
            }
            j = 0;
            int p = 0;
            while (p < cipherText.Length)
            {
                p = (p + 1) % 256;
                j = (j + s[p]) % 256;
                tmp = s[p];
                s[p] = s[j];
                s[j] = tmp;
                int n = (s[p] + s[j]) % 256;
                k[p - 1] = (char)s[n];
                ct[p - 1] = (char)(cipherText[p - 1] ^ k[p - 1]);
            }
            string plain = ct.ToString();
            return plain;
            // throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            //  string pt = String.Copy(plainText);
            StringBuilder pt = new StringBuilder(plainText);
            //key generated
            char[] k = new char[plainText.Length];
            var s = new int[256];
            var t = new int[256];

            for (int i = 0; i < s.Length; i++)
            {
                s[i] = i;
            }
            for (int i = 0; i < s.Length; i++)
            {
                t[i] = key[i % key.Length];
            }
            int tmp;
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + s[i] + t[i]) % 256;
                tmp = s[i];
                s[i] = s[j];
                s[j] = tmp;
            }
            j = 0;
            int p = 0;
            while (p < plainText.Length)
            {
                p = (p + 1) % 256;
                j = (j + s[p]) % 256;
                tmp = s[p];
                s[p] = s[j];
                s[j] = tmp;
                int n = (s[p] + s[j]) % 256;
                k[p - 1] = (char)s[n];
                pt[p - 1] = (char)(plainText[p - 1] ^ k[p - 1]);
            }
            string cipher = pt.ToString();
            return cipher;
            //     throw new NotImplementedException();

        }
    }
}
