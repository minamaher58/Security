using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            string testedString;
            int i;
            for (i = 2; i < cipherText.Length; i++)
            {
                testedString = Decrypt(cipherText, i).ToLower();
                if (testedString.Equals(plainText.ToLower()))
                    return i;
            }
            return -1;
        }

        public string Decrypt(string cipherText, int key)
        {
            int levelSize = cipherText.Length / key;
            int remender = cipherText.Length % key, i, j;
            char[,] map = buildMap(cipherText, key);

            StringBuilder plainText = new StringBuilder();

            for (i = 0; i <= levelSize; i++)
                for (j = 0; j < key; j++)
                    plainText.Append(map[j, i]);

            plainText.Replace("\0", "");

            return plainText.ToString();
        }

        public string Encrypt(string plainText, int key)
        {
            StringBuilder cipherText = new StringBuilder(plainText);

            int len = plainText.Length;
            int levelSize = len / key;
            int remender = len % key, i, j, c = 0;

            if (remender == 0)
            {
                for (i = 0; i < levelSize; i++)
                    for (j = 0; j < key; j++)
                        cipherText[i + levelSize * j] = plainText[c++];
            }
            else
            {

                for (i = 0; i < levelSize; i++)
                {

                    for (j = 0; j < key; j++)
                    {
                        if (c == len - remender)
                            break;

                        else
                            cipherText[i + levelSize * j] = plainText[c++];
                    }
                }
                for (j = 0; j < remender; j++)
                {
                    cipherText.Insert(levelSize + (levelSize + 1) * j, plainText[c++]);
                    cipherText.Remove(cipherText.Length - 1, 1);
                }
            }

            return cipherText.ToString();
        }

        public char[,] buildMap(string text, int key)
        {
            int i, j, c = 0, len = text.Length;
            int levelSize = len / key;
            int remender = len % key;


            char[,] myMap = new char[len, len];

            for (i = 0; i < key; i++)
            {
                for (j = 0; j <= levelSize; j++)
                {
                    if (remender > 0)
                        myMap[i, j] = text[c++];

                    else
                    {
                        if (j < levelSize)
                            myMap[i, j] = text[c++];

                        else
                            myMap[i, j] = '\0';
                    }

                }

                remender--;
            }

            return myMap;
        }
    }
}
