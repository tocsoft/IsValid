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
                int sumOfDigits = input
                    .Where((e) => e >= '0' && e <= '9')
                    .Reverse()
                    .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                    .Sum((e) => e / 10 + e % 10);

                if (sumOfDigits % 10 != 0)
                {
                    inputV.AddError("Check sum not valid");
                }
            }

            return inputV.IsValid;
        }

    }
}
