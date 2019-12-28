using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            throw new NotImplementedException();
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int row = 0;
            int keycount = key.Count;
            int plaintextcount = plainText.Count;

            List<int> chipherlist = new List<int>();
            if (keycount % 2 == 0 && plaintextcount % 2 == 0)
                row = 2;
            if (keycount % 3 == 0 && plaintextcount % 3 == 0)
                row = 3;

            int col = plaintextcount / row;
            int[,] plaintext1 = new int[row, col];
            int index = 0;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    plaintext1[j, i] = plainText[index++];
                }

            }

            int divkey = keycount / row;
            int[,] Keyhill = new int[divkey, row];
            for (int i = 0; i < divkey; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    Keyhill[i, j] = key[i * row + j];
                }

            }

            int[,] cipherhill = new int[keycount / 2, plaintextcount / 2];
            for (int i = 0; i < divkey; i++)
            {
                for (int j = 0; j < plaintextcount / row; j++)
                {
                    for (int l = 0; l < row; l++)
                    {
                        cipherhill[i, j] += Keyhill[i, l] * plaintext1[l, j];
                    }
                }
            }

            for (int i = 0; i < col; i++)
            {
                for (int l = 0; l < divkey; l++)
                {
                    cipherhill[l, i] %= 26;
                    chipherlist.Add(cipherhill[l, i]);

                }

            }
            return chipherlist;
        }



        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

    }
}
