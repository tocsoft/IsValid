using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public static class IsCreditCard
    {
        /// <summary>
        /// Indicates whether input is in correct format for a credit card number and is valid.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CreditCard(this ValidatableValue<string> inputV)
        {

            var input = inputV.Value.Replace(" ", "");
            input = input.Replace("-", "");
            if (!input.IsValid().Numeric())
            {
                inputV.AddError("Contains invalid characters");
            }
            else
            {

                var sumOfDigits = 0;
                var pos = 0;
                for (var i = input.Length - 1; i >= 0; i--)
                {
                    var e = input[i];
                    if (e >= '0' && e <= '9')
                    {
                        var v = ((int)e - 48) * (pos % 2 == 0 ? 1 : 2);

                        sumOfDigits += v / 10 + v % 10;

                        pos++;
                    }
                }

                if (sumOfDigits % 10 != 0)
                {
                    inputV.AddError("Check sum not valid");
                }
            }

            return inputV.IsValid;
        }

    }
}
