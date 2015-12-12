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
        //https://en.wikipedia.org/wiki/International_Bank_Account_Number#Algorithms
        static List<CountryValidation> _countryValidationLength = new List<CountryValidation>()
        {
new CountryValidation("GBkk bbbb ssss sscc cccc cc", "en-GB"),
            new CountryValidation("ALkk bbbs sssx cccc cccc cccc cccc"),
new CountryValidation("ADkk bbbb ssss cccc cccc cccc"),
new CountryValidation("ATkk bbbb bccc cccc cccc"),
new CountryValidation("AZkk bbbb cccc cccc cccc cccc cccc"),
new CountryValidation("BHkk bbbb cccc cccc cccc cc"),
new CountryValidation("BEkk bbbc cccc ccxx"),
new CountryValidation("BAkk bbbs sscc cccc ccxx"),
new CountryValidation("BRkk bbbb bbbb ssss sccc cccc ccct n"),
new CountryValidation("BGkk bbbb ssss ddcc cccc cc"),
new CountryValidation("CRkk bbbc cccc cccc cccc c"),
new CountryValidation("HRkk bbbb bbbc cccc cccc c"),
new CountryValidation("CYkk bbbs ssss cccc cccc cccc cccc"),
new CountryValidation("CZkk bbbb ssss sscc cccc cccc"),
new CountryValidation("DKkk bbbb cccc cccc cc"),
new CountryValidation("DOkk bbbb cccc cccc cccc cccc cccc"),
new CountryValidation("TLkk bbbc cccc cccc cccc cxx"),
new CountryValidation("EEkk bbss cccc cccc cccx"),
new CountryValidation("FOkk bbbb cccc cccc cx"),
new CountryValidation("FIkk bbbb bbcc cccc cx"),
new CountryValidation("FRkk bbbb bggg ggcc cccc cccc cxx"),
new CountryValidation("GEkk bbcc cccc cccc cccc cc"),
new CountryValidation("DEkk bbbb bbbb cccc cccc cc"),
new CountryValidation("GIkk bbbb cccc cccc cccc ccc"),
new CountryValidation("GRkk bbbs sssc cccc cccc cccc ccc"),
new CountryValidation("GLkk bbbb cccc cccc cc"),
new CountryValidation("GTkk bbbb mmtt cccc cccc cccc cccc"),
new CountryValidation("HUkk bbbs sssk cccc cccc cccc cccx"),
new CountryValidation("ISkk bbbb sscc cccc iiii iiii ii"),
new CountryValidation("IEkk aaaa bbbb bbcc cccc cc"),
new CountryValidation("ILkk bbbn nncc cccc cccc ccc"),
new CountryValidation("ITkk xaaa aabb bbbc cccc cccc ccc"),
new CountryValidation("JOkk bbbb nnnn cccc cccc cccc cccc cc"),
new CountryValidation("KZkk bbbc cccc cccc cccc"),
new CountryValidation("XKkk bbbb cccc cccc cccc"),
new CountryValidation("KWkk bbbb cccc cccc cccc cccc cccc cc"),
new CountryValidation("LVkk bbbb cccc cccc cccc c"),
new CountryValidation("LBkk bbbb cccc cccc cccc cccc cccc"),
new CountryValidation("LIkk bbbb bccc cccc cccc c"),
new CountryValidation("LTkk bbbb bccc cccc cccc"),
new CountryValidation("LUkk bbbc cccc cccc cccc"),
new CountryValidation("MKkk bbbc cccc cccc cxx"),
new CountryValidation("MTkk bbbb ssss sccc cccc cccc cccc ccc"),
new CountryValidation("MRkk bbbb bsss sscc cccc cccc cxx"),
new CountryValidation("MUkk bbbb bbss cccc cccc cccc 000d dd"),
new CountryValidation("MDkk bbcc cccc cccc cccc cccc"),
new CountryValidation("MCkk bbbb bsss sscc cccc cccc cxx"),
new CountryValidation("MEkk bbbc cccc cccc cccc xx"),
new CountryValidation("NLkk bbbb cccc cccc cc"),
new CountryValidation("NOkk bbbb cccc ccx"),
new CountryValidation("PKkk bbbb cccc cccc cccc cccc"),
new CountryValidation("PSkk bbbb xxxx xxxx xccc cccc cccc c"),
new CountryValidation("PLkk bbbs sssx cccc cccc cccc cccc"),
new CountryValidation("PTkk bbbb ssss cccc cccc cccx x"),
new CountryValidation("QAkk bbbb cccc cccc cccc cccc cccc c"),
new CountryValidation("ROkk bbbb cccc cccc cccc cccc"),
new CountryValidation("SMkk xaaa aabb bbbc cccc cccc ccc"),
new CountryValidation("SAkk bbcc cccc cccc cccc cccc"),
new CountryValidation("RSkk bbbc cccc cccc cccc xx"),
new CountryValidation("SKkk bbbb ssss sscc cccc cccc"),
new CountryValidation("SIkk bbss sccc cccc cxx"),
new CountryValidation("ESkk bbbb gggg xxcc cccc cccc"),
new CountryValidation("SEkk bbbc cccc cccc cccc cccc"),
new CountryValidation("CHkk bbbb bccc cccc cccc c"),
new CountryValidation("TNkk bbss sccc cccc cccc cccc"),
new CountryValidation("TRkk bbbb bxcc cccc cccc cccc cc"),
new CountryValidation("AEkk bbbc cccc cccc cccc ccc"),
new CountryValidation("VGkk bbbb cccc cccc cccc cccc"),

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
            var val = inputVal.Value.ToUpper();
            var countryCode = val.Trim().Substring(0, 2);

            var validator = _countryValidationLength.Where(x => x.CountryCode == countryCode).FirstOrDefault();
            if (validator == null)
            {
                inputVal.AddError("Unrecognised country code");
                return false;
            }

            return validator.Validate(inputVal);

        }

        private class CountryValidation
        {
            public CountryValidation(string code, string bankAccountLocal = null)
            {
                var clean = code.Replace(" ", "").ToUpper();
                CountryCode = clean.Substring(0, 2);
                var sb = new StringBuilder();
                sb.Append(CountryCode);
                char current = '#';
                int count = 0;
                //sectionstart
                for (var i = 2; i < clean.Length; i++)
                {
                    var next = clean[i];
                    if (current != next)
                    {
                        //close old one
                        if (current != '#')
                        {
                            sb.Append(")");
                        }
                        //open new one
                        sb.Append($"(?<CG_{next}>");
                        current = next;
                    }
                    sb.Append("\\S");
                }
                sb.Append(")");

                LocaleCode = bankAccountLocal;
                Length = clean.Length;
                //lets convert pattern to regex

                Pattern = new Regex(sb.ToString());
            }

            public string CountryCode { get; set; }
            private string LocaleCode { get; set; }
            private Regex Pattern { get; set; }
            private int Length { get; set; }

            internal bool Validate(ValidatableValue<string> inputVal)
            {
                var val = inputVal.Value.ToUpper();
                var charCount = val.Count(x => Char.IsLetterOrDigit(x));
                if (charCount != Length)
                {
                    inputVal.AddError("Invalid length");
                    return false;
                }
                StringBuilder sb = new StringBuilder();

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

                //lets try and validate the actual account details

                if (!string.IsNullOrWhiteSpace(LocaleCode))
                {
                    val = inputVal.Value.ToUpper().Replace(" ", "");
                    var m = Pattern.Match(val);
                    string bankCode = null;
                    if (m.Groups["CG_S"] != null)
                    {
                        bankCode = m.Groups["CG_S"].Value;//sort code
                    }
                    var accountNumber = m.Groups["CG_C"].Value;//account number
                    var validator = accountNumber.IsValid(LocaleCode);
                    if (!validator.BankAccount(bankCode))
                    {
                        foreach (var e in validator.Errors)
                        {
                            inputVal.AddError(e);
                        }
                }
                }

                return inputVal.IsValid;
            }
        }
    }
}

