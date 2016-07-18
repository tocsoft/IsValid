using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    //information was sourced from https://en.wikipedia.org/wiki/International_Bank_Account_Number#Validating_the_IBAN
    public static class IsBusinessIdentifierCode
    {

        /// <summary>
        /// Indicates whether supplied input is either in ISBN-10 digit format or ISBN-13 digit format.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="version">Valid options are: IsbnVersion.Ten, IsbnVersion.Thirteen or IsbnVersion.Any</param>
        /// <returns></returns>
        /// IsbnVersion
        public static bool BusinessIdentifierCode(this ValidatableValue<string> inputVal)
        {
            var val = inputVal.Value;
            if (!(val.Length == 8 || val.Length == 11))
            {
                inputVal.AddError("Invalid length");
            }
            if (val.Length >= 4)
            {
                var institutionCode = inputVal.Value.Substring(0, 4);
                if (institutionCode.Any(x => !Char.IsLetter(x)))
                {
                    //must be made up of letters
                    inputVal.AddError("Invalid Institution Code");
                }
            }

            if (val.Length >= 6)
            {
                var countryCode = inputVal.Value.Substring(4, 2).ToUpperInvariant();
                if (!Constants.CountryCodes.Codes.Contains(countryCode))
                {
                    inputVal.AddError("Invalid Country Code");
                }
            }
            if (val.Length >= 8)
            {
                var locationCode = inputVal.Value.Substring(6, 2).ToUpperInvariant();
                if (locationCode.Any(x => !Char.IsLetterOrDigit(x)))
                {
                    //must be made up of letters
                    inputVal.AddError("Invalid Location Code");
                }
            }
            if (val.Length >= 11)
            {
                var branchCode = inputVal.Value.Substring(8, 3).ToUpperInvariant();
                if (branchCode.Any(x => !Char.IsLetterOrDigit(x)))
                {
                    //must be made up of letters
                    inputVal.AddError("Invalid Branch Code");
                }
            }
            return inputVal.IsValid;
        }
    }
}

