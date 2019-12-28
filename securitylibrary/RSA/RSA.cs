using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            int cipher = 0;

            int n = p * q;
            int Qn = (p - 1) * (q - 1);
            cipher = (int)Mod(M, e, n);

            return cipher;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            int plain = 0;

            int n = p * q;
            int Qn = (p - 1) * (q - 1);


            ExtendedEuclid ex = new ExtendedEuclid();
            int inverse = ex.GetMultiplicativeInverse(e, Qn);
            int d = inverse;
            if (d < 0)
            {
                d += Qn;
            }

            //  plain = (int)(Pow(C, d) % n);

            plain = (int)Mod(C, d, n);


            return plain;
        }
        public long Pow(int xbase, int ypower)
        {
            if (ypower == 0)
                return 1;
            long x = Pow(xbase, ypower / 2);
            x = x * x;
            if (ypower % 2 == 1)
                x = x * xbase;


            return x;
        }
        double Mod(int a, int e, int mod)
        {
            string binary = Convert.ToString(e, 2);
            double res = a;
            for (int i = 0; i < binary.Length - 1; i++)
            {
                if (binary[i + 1] == '1')
                {
                    res = ((Math.Pow(res, 2)) * a) % mod;

                }
                else
                    res = Math.Pow(res, 2) % mod;
            }
            return res;
        }

    }

    public class ExtendedEuclid
    {
        int[] A, B;
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            A = new int[] { 1, 0, baseN };
            B = new int[] { 0, 1, number };
            int GCD = extendedEuclidean();

            if (GCD == 1)
                return B[1];

            return 0;
        }
        private int extendedEuclidean()
        {
            if (B[2] == 0)
                return A[2];
            else if (B[2] == 1)
                return B[2];

            int Q = A[2] / B[2];
            int[] T = { A[0] - Q * B[0], A[1] - Q * B[1], A[2] - Q * B[2] };
            A = B;
            B = T;
            return extendedEuclidean();
        }
    }
}
