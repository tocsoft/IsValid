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
    public static class IsIban
    {
        static Dictionary<string, int> _countryValidationLength = new Dictionary<string, int>()
        {
            {"AL", 28},
            {"AD", 24},
            {"AT", 20},
            {"AZ", 28},
            {"BH", 22},
            {"BE", 16},
            {"BA", 20},
            {"BR", 29},
            {"BG", 22},
            {"CR", 21},
            {"HR", 21},
            {"CY", 28},
            {"CZ", 24},
            {"DK", 18},
            {"DO", 28},
            {"TL", 23},
            {"EE", 20},
            {"FO", 18},
            {"FI", 18},
            {"FR", 27},
            {"GE", 22},
            {"DE", 22},
            {"GI", 23},
            {"GR", 27},
            {"GL", 18},
            {"GT", 28},
            {"HU", 28},
            {"IS", 26},
            {"IE", 22},
            {"IL", 23},
            {"IT", 27},
            {"JO", 30},
            {"KZ", 20},
            {"XK", 20},
            {"KW", 30},
            {"LV", 21},
            {"LB", 28},
            {"LI", 21},
            {"LT", 20},
            {"LU", 20},
            {"MK", 19},
            {"MT", 31},
            {"MR", 27},
            {"MU", 30},
            {"MC", 27},
            {"MD", 24},
            {"ME", 22},
            {"NL", 18},
            {"NO", 15},
            {"PK", 24},
            {"PS", 29},
            {"PL", 28},
            {"PT", 25},
            {"QA", 29},
            {"RO", 24},
            {"SM", 27},
            {"SA", 24},
            {"RS", 22},
            {"SK", 24},
            {"SI", 19},
            {"ES", 24},
            {"SE", 24},
            {"CH", 21},
            {"TN", 24},
            {"TR", 26},
            {"AE", 23},
            {"GB", 22},
            {"VG", 24},

            {"DZ", 24},
            {"AO", 25},
            {"BJ", 28},
            {"BF", 27},
            {"BI", 16},
            {"CM", 27},
            {"CV", 25},
            {"IR", 26},
            {"CI", 28},
            {"MG", 27},
            {"ML", 28},
            {"MZ", 25},
            {"SN", 28},
            {"UA", 29},
        };

        /// <summary>
        /// Indicates whether supplied input is either in ISBN-10 digit format or ISBN-13 digit format.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="version">Valid options are: IsbnVersion.Ten, IsbnVersion.Thirteen or IsbnVersion.Any</param>
        /// <returns></returns>
        /// IsbnVersion
        public static bool Iban(this ValidatableValue<string> inputVal)
        {
            StringBuilder sb = new StringBuilder();
            var val = inputVal.Value;
            var countryCode = val.Trim().Substring(0, 2);

            if (!_countryValidationLength.ContainsKey(countryCode))
            {
                inputVal.AddError("Unrecognised country code");
                return false;
            }

            var charCount = val.Count(x => Char.IsLetterOrDigit(x));
            if (charCount != _countryValidationLength[countryCode])
            {
                inputVal.AddError("Invalid length");
                return false;
            }

            for (var i = 0; i < val.Length; i++)
            {
                var c = val[i];
                if (!Char.IsWhiteSpace(c))
                {
                    if (Char.IsLetter(c))
                    {
                        //letter
                        var v = (Char.ToUpper(c) - 'A') + 10;
                        sb.Append(v);
                    }
                    else if (Char.IsDigit(c))
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        inputVal.AddError("Contains invalid character(s)");
                        return false;
                    }
                }
            }

            val = sb.ToString();
            val = val.Substring(6) + val.Substring(0, 6);

            var intVal = System.Numerics.BigInteger.Parse(val);

            var remainder = System.Numerics.BigInteger.Remainder(intVal, new System.Numerics.BigInteger(97));
            if (!remainder.IsOne)
            {
                inputVal.AddError("Invalid check digit");
            }

            return inputVal.IsValid;
        }
    }
}

