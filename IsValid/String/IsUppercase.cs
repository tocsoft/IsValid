using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public static class IsUppercase
    {
        /// <summary>
        /// Determine whether input is in lower case.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Uppercase(this IValidatableValue<string> input)
        {
            if (!input.IsValueSet || input.Value == null)
            {
                return true;
            }

            var val = input.Value;
            return val == val.ToUpper();
        }
    }
}
