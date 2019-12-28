using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        List<List<int>> keys;
        public List<int> Analyse(string plainText, string cipherText)
        {
            string testedString;
            StringBuilder key = new StringBuilder("12");
            int i, j;
            keys = new List<List<int>>();

            for (i = 3; i <= cipherText.Length; i++)
            {
                keys.Clear();
                permute(keys, key.ToString(), 0, key.Length - 1);

                for (j = 0; j < keys.Count; j++)
                {
                    testedString = Decrypt(cipherText, keys[j]).ToLower();
                    if (testedString.Equals(plainText.ToLower()))
                        return keys[j];
                }

                key.Append(i.ToString());
            }

            return null;
        }

        private static void permute(List<List<int>> keys, String str, int left, int right)
        {
            if (left == right)
            {

                List<int> key = new List<int>();
                for (int i = 0; i < str.Length; i++)
                    key.Add(str[i] - 48);

                keys.Add(key);
            }
            else
            {
                for (int i = left; i <= right; i++)
                {
                    str = swap(str, left, i);
                    permute(keys, str, left + 1, right);
                    str = swap(str, left, i);
                }
            }
        }

        public static String swap(String str, int i, int j)
        {
            try
            {
                char temp;
                char[] charArray = str.ToCharArray();
                temp = charArray[i];
                charArray[i] = charArray[j];
                charArray[j] = temp;
                string newStr = new string(charArray);
                return newStr;
            }
            catch
            {
                return null;
            }

        }

        public string Decrypt(string cipherText, List<int> key)
        {
            int i, j;
            int textLen = cipherText.Length;
            int width = key.Max();
            int depth = textLen / width;
            if (textLen % width != 0) depth++;

            char[,] map = buildMap(cipherText, depth, width, true);

            List<string> myList = new List<string>();

            StringBuilder plainText = new StringBuilder();
            StringBuilder temp = new StringBuilder();

            for (i = 0; i < width; i++)
            {
                for (j = 0; j < depth; j++)
                    temp.Append(map[j, i]);

                myList.Add(temp.ToString());
                temp.Clear();
            }
            List<string> tempList = new List<string>(myList);

            for (i = 0; i < width; i++)
                tempList[key.IndexOf(i + 1)] = myList[i];

            for (i = 0; i < depth; i++)
                for (j = 0; j < width; j++)
                    plainText.Append(tempList[j][i]);

            plainText.Replace("\0", "");

            return plainText.ToString();
        }

        public string Encrypt(string plainText, List<int> key)
        {
            int i, j;
            int textLen = plainText.Length;
            int width = key.Max();
            int depth = textLen / width;
            if (textLen % width != 0) depth++;

            char[,] map = buildMap(plainText, depth, width, false);

            List<string> myList = new List<string>();
            StringBuilder cipherText = new StringBuilder();
            StringBuilder temp = new StringBuilder();

            for (i = 0; i < width; i++)
                myList.Add("\0");

            for (i = 0; i < width; i++)
            {
                for (j = 0; j < depth; j++)
                    temp.Append(map[j, i]);

                myList.RemoveAt(key[i] - 1);
                myList.Insert((key[i] - 1), temp.ToString());

                temp.Clear();
            }

            for (i = 0; i < myList.Count; i++)
                cipherText.Append(myList[i]);

            cipherText.Replace("\0", "");

            return cipherText.ToString();
        }

        public char[,] buildMap(string text, int depth, int width, bool dec)
        {
            int i, j, c = 0, len = text.Length;

            char[,] myMap = new char[depth, width];
            if (dec)
            {
                for (j = 0; j < width; j++)
                    for (i = 0; i < depth; i++)
                    {
                        if (c > len - 1)
                            myMap[i, j] = '\0';
                        else
                            myMap[i, j] = text[c++];
                    }
            }
            else
            {
                for (i = 0; i < depth; i++)
                    for (j = 0; j < width; j++)
                    {
                        if (c > len - 1)
                            myMap[i, j] = '\0';
                        else
                            myMap[i, j] = text[c++];
                    }
            }

            return myMap;
        }

    }
}