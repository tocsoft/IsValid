using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsValid
{
    public static class StringExtensions
    {
        public static char Last(this string str)
        {
            if(str == null)
            {
                return default(char);
            }

            return str[str.Length - 1];
        }

        public static int Count(this string str, Func<Char, bool> predictate)
        {
            if (str == null)
            {
                return 0;
            }

            int count = 0;
            foreach (var c in str)
            {
                if (predictate(c))
                {
                    count++;
                }
            }

            return count;
        }

        public static bool Any(this string str, Func<Char, bool> predictate)
        {
            if (str == null)
            {
                return false;
            }
            
            foreach (var c in str)
            {
                if (predictate(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
