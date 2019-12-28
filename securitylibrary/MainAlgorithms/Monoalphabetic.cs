using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();

            char[] key = new char[26];
            string alphabets = "abcdefghijklmnopqrstuvwxyz";
            char[] alphabetss = alphabets.ToCharArray();

            // char[] chosen = new char[26];
            for (int i = 0; i < cipherText.Length; i++)
            {
                int j = plainText[i] - 'a';
                key[j] = cipherText[i];
                //  chosen[i]=cipherText[i];
            }

            for (int i = 0; i < 26; i++)
            {

                if (key[i] == '\0')
                {
                    for (int j = 0; j < 26; j++)
                    {

                        if (alphabetss[j] == ' ' || key.Contains(alphabetss[j]))
                        {
                            continue;
                        }
                        else
                        {
                            key[i] = alphabetss[j];
                            alphabetss[j] = ' ';
                        }
                        break;
                    }
                }

            }
            return new string(key);
        }

        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            char[] plaintext = new char[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (cipherText[i] == ' ')
                {
                    plaintext[i] = ' ';
                }
                else
                {

                    int j = key.IndexOf(cipherText[i]) + 97;
                    plaintext[i] = (char)j;
                }
            }
            return new string(plaintext);
            //throw new NotImplementedException();


        }



        public string Encrypt(string plainText, string key)
        {
            char[] ct = new char[plainText.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == ' ')
                {
                    ct[i] = ' ';
                }
                else
                {
                    int j = plainText[i] - 'a';
                    ct[i] = key[j];
                }
            }
            return new string(ct);
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            cipher = cipher.ToLower();
            char[] ct = cipher.ToCharArray();

            string frequencies = "etaoinsrhldcumfpgwybvkxjqz";
            char[] freq = frequencies.ToCharArray();

            string alphabets = "abcdefghijklmnopqrstuvwxyz";
            char[] alphabetss = alphabets.ToCharArray();

            int[] arr = new int[26];

            //frequency of ciphertext
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < ct.Length; j++)
                {
                    if (alphabetss[i] == ct[j])
                    {
                        arr[i] = arr[i] + 1;
                    }
                }
            }
            //sort
            for (int i = 0; i < 25; i++)
            {
                for (int j = 25; j > 0; j--)
                {
                    if (arr[j] > arr[j - 1])
                    {
                        char temp = ct[j - 1];
                        ct[j - 1] = ct[j];
                        ct[j] = temp;


                        int tempo = arr[j - 1];
                        arr[j - 1] = arr[j];
                        arr[j] = tempo;
                    }
                }
            }
            for (int i = 0; i < 25; i++)
            {
                ct[i] = freq[i];
            }
            return new string(ct);
            // throw new NotImplementedException();
        }
    }
}
