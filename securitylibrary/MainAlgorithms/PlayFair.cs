using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            string newCipherText = cipherText.ToLower();
            StringBuilder plainText = new StringBuilder();
            int i;
            char[] res = new char[2];

            string newKey = new string(key.ToCharArray().Distinct().ToArray());
            if (newKey.Contains("j"))
                newKey.Replace("j", "i");

            char[,] map = buildMap(newKey);

            for (i = 0; i < cipherText.Length; i++)
            {
                res = newChars(newCipherText[i], newCipherText[i + 1], map, true);
                plainText.Append(res[0]);
                plainText.Append(res[1]);
                i++;
            }

            return editText(plainText.ToString(), true);
        }

        public string Encrypt(string plainText, string key)
        {
            string newPlainText = editText(plainText.ToLower(), false);
            StringBuilder cipherText = new StringBuilder();
            int i;
            char[] res = new char[2];

            string newKey = new string(key.ToCharArray().Distinct().ToArray());
            if (newKey.Contains("j"))
                newKey.Replace("j", "i");

            char[,] map = buildMap(newKey);

            for (i = 0; i < newPlainText.Length; i++)
            {
                res = newChars(newPlainText[i], newPlainText[i + 1], map, false);
                cipherText.Append(res[0]);
                cipherText.Append(res[1]);
                i++;
            }

            return cipherText.ToString();
        }

        public char[,] buildMap(string key)
        {
            int i, j, k, len = key.Length;
            char ch = 'a';
            char[,] Arr = new char[5, 5];
            for (i = 1; i <= 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    if (len > 0)
                    {
                        Arr[i - 1, j] = key[j + 5 * (i - 1)];
                        len--;
                        continue;
                    }

                    for (k = 0; k < 26; k++)
                    {
                        if (key.Contains(ch))
                            ch++;

                        else
                            break;
                    }
                    if (ch == 'j')
                        ch++;

                    Arr[i - 1, j] = ch;

                    ch++;

                }
            }

            return Arr;
        }

        public char[] newChars(char x, char y, char[,] map, bool dec)
        {
            int pX, pY, ix, jx, iy, jy, var = 1;
            pX = newPosition(x, map);
            pY = newPosition(y, map);

            jx = pX % 5;
            ix = (pX / 5);

            jy = pY % 5;
            iy = (pY / 5);



            if (dec)
                var = -1;

            if (ix == iy)
            {
                int t1 = (jx + var) % 5, t2 = (jy + var) % 5;
                if (t1 < 0) t1 += 5;
                if (t2 < 0) t2 += 5;

                return new char[] { map[ix, t1], map[ix, t2] };
            }

            else if (jx == jy)
            {
                int t1 = (ix + var) % 5, t2 = (iy + var) % 5;
                if (t1 < 0) t1 += 5;
                if (t2 < 0) t2 += 5;
                return new char[] { map[t1, jx], map[t2, jx] };
            }

            else
                return new char[] { map[ix, jy], map[iy, jx] };
        }

        public int newPosition(char x, char[,] map)
        {
            int i, j, position = 0;

            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    if (x == map[i, j])
                        return position;
                    else
                        position++;
                }
            }

            return position;
        }

        public string editText(string text, bool dec)
        {
            int len = text.Length;

            StringBuilder s = new StringBuilder(text);

            if (dec)
            {
                for (int i = 0; i < len; i++)
                {
                    if (i + 1 < s.Length && i - 1 >= 0)
                    {
                        if (s[i] == 'x' && s[i - 1] == s[i + 1] && i % 2 != 0)
                            s.Remove(i, 1);
                    }
                }
                if (s[s.Length - 1] == 'x') s.Remove(s.Length - 1, 1);
            }

            else
            {
                for (int i = 0; i < len; i++)
                {
                    if (i + 1 < len)
                    {
                        if (s[i] == s[i + 1]) s.Insert(i + 1, 'x');
                        if (s[i] == 'j') s[i] = 'i';
                    }
                }

                if (s.Length % 2 != 0)
                    s.Append("x");
            }

            return s.ToString();
        }
    }
}
