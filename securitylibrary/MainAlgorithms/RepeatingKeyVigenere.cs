using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            int occurance = 0, index = 0;

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

                key += alphabets[x];
                if (key.Length > 2 && key[1] == alphabets[x])
                {
                    index = key.LastIndexOf(alphabets[x]);
                    occurance++;

                }

                if (occurance == 2)
                {
                    if (key[0] == key[index - 1])
                    {
                        StringBuilder sb = new StringBuilder(key);
                        sb.Remove(index - 1, 2);
                        key = sb.ToString();
                        break;
                    }



                }






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
            // increase key length
            if (key.Length != cipherText.Length)
            {
                StringBuilder asb = new StringBuilder(key);
                int j = 0;
                for (int i = key.Length; i < cipherText.Length; i++)
                {
                    char s = asb[j];
                    asb.Append(s);
                    j++;
                }
                key = asb.ToString();
            }

            StringBuilder sb = new StringBuilder(plaintext);

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
                sb.Append(alphabets[x]);

            }
            plaintext = sb.ToString();

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
                StringBuilder asb = new StringBuilder(key);
                int j = 0;
                for (int i = key.Length; i < plainText.Length; i++)
                {
                    char s = asb[j];
                    asb.Append(s);
                    j++;
                }
                key = asb.ToString();
            }
            StringBuilder sb = new StringBuilder(cipher);
            for (int i = 0; i < plainText.Length; i++)
            {
                int x = position(plainText[i]);
                int y = position(key[i]);
                sb.Append(tableu[x, y]);


            }
            cipher = sb.ToString();

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