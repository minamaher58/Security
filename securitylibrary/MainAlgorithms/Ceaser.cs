using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            char[] pt = plainText.ToCharArray();
            int char1_index = 'a';

            int letters = ('z' - 'a') + 1;
            for (int i = 0; i < pt.Length; i++)
            {
                char current_char = pt[i];
                int current_index = current_char - char1_index;
                int cipherindex = (current_index + key) % letters;

                char cipherchar = (char)(char1_index + cipherindex);
                pt[i] = cipherchar;
            }
            return new string(pt);

            //   throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, int key)
        {
            string ctemp = cipherText.ToLower();
            char[] ct = ctemp.ToCharArray();

            char char1_index = 'a';
            int letters = ('z' - 'a') + 1;
            for (int i = 0; i < ct.Length; i++)
            {
                char current_char = ct[i];



                int current_index = current_char - char1_index;



                int cipherindex = (current_index - key);

                cipherindex = ((cipherindex % letters) + letters) % letters;

                //int x = 0;

                /*if (cipherindex < 0)
                {
                    while (cipherindex < 0)
                    {
                        cipherindex = cipherindex + letters;
                        //  cipherindex = (current_index + key) % letters;
                        // cipherindex = cipherindex % letters;

                    }
                }
                else
                {
                    cipherindex = cipherindex % letters;
                }*/
                char cipherchar = (char)(cipherindex + char1_index);
                ct[i] = cipherchar;
            }
            return new string(ct);

            //throw new NotImplementedException();
        }

        public int Analyse(string plainText, string cipherText)
        {
            int key = 0;

            string temp = plainText.ToLower();

            char[] pt = temp.ToCharArray();

            temp = cipherText.ToLower();

            char[] ct = temp.ToCharArray();



            int char1_index = 'a';
            int letters = ('z' - 'a') + 1;
            //  for (int i = 0; i < pt.Length; i++)
            //   {
            char current_char_pt = pt[0];
            char current_char_ct = ct[0];

            int current_index_pt = current_char_pt - char1_index;
            int current_index_ct = current_char_ct - char1_index;
            //int cipherindex = (current_index + key) % letters;
            key = (((current_index_ct - current_index_pt) % letters) + letters) % letters;

            //  }
            return key;

            //throw new NotImplementedException();
        }
    }
}
