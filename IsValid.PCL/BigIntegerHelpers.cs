using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace IsValid
{
    internal class BigIntegerHelpers
    {
        public static System.Numerics.BigInteger Parse(string sval)
        {
            int index = 0;
            int sign = 1;
            if (sval[0] == '-')
            {
                if (sval.Length == 1)
                {
                    throw new FormatException("Zero length BigInteger");
                }

                sign = -1;
                index = 1;
            }
            else
            {
                if (sval.Length == 0)
                {
                    throw new FormatException("Zero length BigInteger");
                }
            }

            // strip leading zeros from the string value
            while (index < sval.Length && int.Parse(sval[index].ToString()) == 0)
            {
                index++;
            }

            if (index >= sval.Length)
            {
                // zero value - we're done
                sign = 0;
                return BigInteger.Zero;
            }


            BigInteger b = BigInteger.Zero;
            BigInteger r = new BigInteger(10);
            while (index < sval.Length)
            {
                // (optimise this by taking chunks of digits instead?)
                var t = BigInteger.Multiply(b, r);
                b = BigInteger.Add(t, new BigInteger(int.Parse(sval[index].ToString())));
                index++;
            }

            //convert it to the correct sign!!
            return  BigInteger.Multiply(b, new BigInteger(sign));
        }
    }
}
