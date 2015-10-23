using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public static class IsNumeric
    {
        /// <summary>
        /// Indicates whether input is in correct format for a number and is valid.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Numeric(this IValidatableValue<string> inputValue)
        {
            var input = inputValue.Value;
            if (input == null)
            {
                return false;
            }
            int length = input.Length;
            if (length == 0)
            {
                return false;
            }
            int i = 0;
            if (input[0] == '-')
            {
                if (length == 1)
                {
                    return false;
                }
                i = 1;
            }
            for (; i < length; i++)
            {
                char c = input[i];
                if (c <= '/' || c >= ':')
                {
                    return false;
                }
            }
            return true;
        }

    }
}
