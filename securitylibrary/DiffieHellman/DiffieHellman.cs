using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman
    {
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            BigInteger ya = (pow(alpha, xa)) % q;
            BigInteger yb = (pow(alpha, xb)) % q;
            BigInteger ka = (pow(yb, xa)) % q;
            BigInteger kb = (pow(ya, xb)) % q;

            return new List<int>() { (int)ka, (int)kb };
        }

        private BigInteger pow(BigInteger n, BigInteger ex)
        {
            BigInteger res = 1;
            while (ex > int.MaxValue)
            {
                ex -= int.MaxValue;
                res = res * BigInteger.Pow(n, int.MaxValue);
            }
            res = res * BigInteger.Pow(n, (int)ex);
            return res;
        }
    }
}
