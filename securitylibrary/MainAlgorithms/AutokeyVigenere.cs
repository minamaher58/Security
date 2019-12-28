using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {

            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            string key = "";
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            char[] alphabets = alpha.ToCharArray();
            char[,] tableu = new char[26, 26];

            //build tableu
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    tableu[i, j] = alphabets[j];
                }
                alphabets = shiftstring(alphabets);
            }



            for (int j = 0; j < cipherText.Length; j++)
            {
                int x = 0;
                int y = position(plainText[j]);
                for (int i = 0; i < 26; i++)
                {
                    if (tableu[i, y] == cipherText[j])
                    {
                        x = i;
                        break;
                    }


                }
                if (plainText[0] == alphabets[x])
                    break;
                key += alphabets[x];
            }

            return key;
        }


        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            string plaintext = "";
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            char[] alphabets = alpha.ToCharArray();
            char[,] tableu = new char[26, 26];
            //bulid tableu
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    tableu[i, j] = alphabets[j];
                }
                alphabets = shiftstring(alphabets);
            }



            for (int j = 0; j < cipherText.Length; j++)
            {
                int x = 0;
                int y = position(key[j]);
                for (int i = 0; i < 26; i++)
                {
                    if (tableu[y, i] == cipherText[j])
                    {
                        x = i;
                        break;
                    }


                }
                plaintext += alphabets[x];
                if (key.Length != cipherText.Length)
                    key += alphabets[x];

            }

            return plaintext;

        }

        public string Encrypt(string plainText, string key)
        {
            string cipher = "";
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            char[] alphabets = alpha.ToCharArray();
            char[,] tableu = new char[26, 26];

            //bulid tableu
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    tableu[i, j] = alphabets[j];
                }
                alphabets = shiftstring(alphabets);
            }
            //increase key length
            if (key.Length != plainText.Length)
            {
                int j = 0;
                for (int i = key.Length; i < plainText.Length; i++)
                {
                    char s = plainText[j];
                    key += s;
                    j++;
                }
            }
            for (int i = 0; i < plainText.Length; i++)
            {
                int x = position(plainText[i]);
                int y = position(key[i]);
                cipher += tableu[x, y];

            }

            return cipher;
        }
        public char[] shiftstring(char[] x)
        {
            string y = new string(x);
            y = y.Substring(1, x.Length - 1) + y.Substring(0, 1);
            return y.ToCharArray();
        }
        public int position(char x)
        {
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            int pos = 0;
            for (int i = 0; i < 26; i++)
            {
                if (x == alpha[i])
                    return pos;
                else pos++;

            }
            return pos;

        }

    }
}
