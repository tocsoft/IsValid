using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IsValid
{
    public static partial class IsBankAccount
    {
        /// <summary>
        /// Loads the uk validation rules.
        /// </summary>
        /// <param name="modulusWeightTable">The modulus weight table.</param>
        /// <param name="substitutionData">The substitution data.</param>
        public static void LoadUKValidationRules(string modulusWeightTable, string substitutionData)
        {
            UK.LoadUKValidationRules(modulusWeightTable, substitutionData);
        }

        private static class UK
        {
            private static string RawModulusWeightTable;
            private static string RawSubstitutionData;
            private static bool initalized = false;

            internal static void Validate(ValidatableValue<string> inputV, string branchNumber)
            {
                Initilize();

                //branchNumber
                //convertToInt to seed up validator range checks
                var cleanedBranchNumber = RemoveSpacesAndHyphens(branchNumber);
                if (cleanedBranchNumber.Length != 6)
                {
                    inputV.AddError("branchNumber must be exactly 6 digits long");
                    return;
                }
                int integerBracnhNumber = 0;
                if (!int.TryParse(cleanedBranchNumber, out integerBracnhNumber))
                {
                    inputV.AddError("branchNumber is in a invalid format");
                }

                //lets run some account number/sortcodes
                var cleanedAccountNumber = RemoveSpacesAndHyphens(inputV.Value);

                if (cleanedAccountNumber.Length < 8)
                {
                    cleanedAccountNumber = cleanedAccountNumber.PadLeft('0');//padded
                }

                if (cleanedAccountNumber.Length == 9)
                {
                    //Santander (formerly Alliance & Leicester Commercial Bank plc)
                    //we use the fist digit of the account as the last digit of the sortcode
                    cleanedBranchNumber = cleanedBranchNumber.Substring(0, 5) + cleanedAccountNumber[0];
                    cleanedAccountNumber = cleanedAccountNumber.Substring(1, 8);
                }


                //if we have 10 digits then we actually have to perform the check twice becauseer there are 2 different ways to cut up an 8 digit number
                if (cleanedAccountNumber.Length == 10)
                {
                    //this could be a 'National Westminster Bank plc' if so use last 8
                    var validatable = new ValidatableValue<string>(cleanedAccountNumber.Substring(2, 8));
                    UK.Validate(validatable, cleanedBranchNumber);
                    if (!validatable.IsValid)
                    {
                        //Co-Operative Bank plc last 8
                        validatable = new ValidatableValue<string>(cleanedAccountNumber.Substring(0, 8));
                        UK.Validate(validatable, cleanedBranchNumber);
                    }

                    foreach (var e in validatable.Errors)
                    {
                        inputV.AddError(e);
                    }
                    return;
                }
                var combindAccountRef = cleanedBranchNumber + cleanedAccountNumber;

                var validator = Validators.Where(x => x.CanValidate(integerBracnhNumber, combindAccountRef));
                if (!validator.Any())
                {
                    //is valid we cant test it
                    return;
                }

                bool isValid = false;

                var firstTest = validator.First();

                if (firstTest.Exception == 5)
                {
                    //for exception 5 we need to swap out the sortcode and start again
                    if (SortCodeSubstitutions.ContainsKey(cleanedBranchNumber))
                    {
                        cleanedBranchNumber = SortCodeSubstitutions[cleanedBranchNumber];
                        integerBracnhNumber = int.Parse(cleanedBranchNumber);
                        combindAccountRef = cleanedBranchNumber + cleanedAccountNumber;
                        validator = Validators.Where(x => x.CanValidate(integerBracnhNumber, combindAccountRef));
                    }
                }

                if (firstTest.Calculate(combindAccountRef))
                {
                    if (validator.Count() == 1 || new[] { 2, 9, 10, 11, 12, 13, 14 }.Contains(firstTest.Exception))
                    {
                        return;
                    }
                    else
                    {
                        if (validator.Skip(1).First().Calculate(combindAccountRef))
                        {
                            return;
                        }
                    }
                }
                else
                {
                    var secondTest = validator.Skip(1).FirstOrDefault();
                    if (firstTest.Exception == 2)
                    {
                        combindAccountRef = "309634" + cleanedAccountNumber;

                        secondTest = Validators.Where(x => x.CanValidate(309634, combindAccountRef)).FirstOrDefault();
                    }

                    if (new[] { 2, 9, 10, 11, 12, 13, 14 }.Contains(firstTest.Exception))
                    {
                        if (secondTest.Calculate(combindAccountRef))
                        {
                            return;
                        }
                    }
                }


                inputV.AddError("Invalid account details");
            }

            private static string RemoveSpacesAndHyphens(string input)
            {
                return Regex.Replace(input, "[\\s-]+", "");
            }
            static object locker = new object();

            public static IEnumerable<Validator> Validators { get; private set; }
            public static IDictionary<string, string> SortCodeSubstitutions { get; private set; }

            private static void Initilize()
            {
                if (initalized)
                    return;

                lock (locker)
                {

                    Validators = RawModulusWeightTable.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Validator(x)).ToList();


                    SortCodeSubstitutions = RawSubstitutionData
                                            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(x => x.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                                            .Where(x => x.Length == 2)
                                            .ToDictionary(x => x[0], x => x[1]);
                    initalized = true;
                }
            }

            /// <summary>
            /// Loads the uk validation rules.
            /// </summary>
            /// <param name="modulusWeightTable">The modulus weight table.</param>
            /// <param name="substitutionData">The substitution data.</param>
            public static void LoadUKValidationRules(string modulusWeightTable, string substitutionData)
            {
                var assembly = typeof(IsBankAccount).Assembly;
                string[] resources = null;
                resources = resources ?? assembly.GetManifestResourceNames();

                var resourcePrefix = "IsValid.";

                if (File.Exists(modulusWeightTable))
                {
                    modulusWeightTable = File.ReadAllText(modulusWeightTable);
                }
                else if (resources.Contains(resourcePrefix + modulusWeightTable))
                {
                    using (var sr = new StreamReader(assembly.GetManifestResourceStream(resourcePrefix + modulusWeightTable)))
                    {
                        modulusWeightTable = sr.ReadToEnd();
                    }
                }
                if (File.Exists(substitutionData))
                {
                    substitutionData = File.ReadAllText(substitutionData);
                }
                else if (resources.Contains(resourcePrefix + substitutionData))
                {
                    using (var sr = new StreamReader(assembly.GetManifestResourceStream(resourcePrefix + substitutionData)))
                    {
                        substitutionData = sr.ReadToEnd();
                    }
                }

                RawModulusWeightTable = modulusWeightTable;
                RawSubstitutionData = substitutionData;
                initalized = false;//need to be reloaded
            }


            public class Validator
            {
                private int sortEnd;
                private int sortStart;
                private int[] wightings;
                public int Exception;
                private int target;
                Func<string, int[], bool> calc;
                public Validator(string line)
                {
                    var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    sortStart = int.Parse(parts[0]);
                    sortEnd = int.Parse(parts[1]);
                    switch (parts[2].ToUpperInvariant())
                    {
                        case "MOD11":
                            calc = StandardModulus11;
                            break;
                        case "MOD10":
                            calc = StandardModulus10;
                            break;
                        case "DBLAL":
                            calc = DoubleAlternate;
                            break;
                    }
                    if (parts.Length > 17)
                    {
                        Exception = int.Parse(parts[17]);
                    }
                    wightings = parts.Skip(3).Take(14).Select(x => int.Parse(x)).ToArray();
                }

                internal bool CanValidate(int integerBracnhNumber, string combinedCode)
                {
                    var g = combinedCode[12];
                    var h = combinedCode[13];
                    var a = combinedCode[6];
                    if (Exception == 6)
                    {
                        if (g == h &&
                        new[] { '4', '5', '6', '7', '8' }.Contains(a)
                            )
                        {
                            //auto valid if uncheckable
                            return false;
                        }
                    }

                    return (integerBracnhNumber >= sortStart && integerBracnhNumber <= sortEnd);
                }

                internal bool Calculate(string combinedCode)
                {
                    var local = wightings;
                    var g = combinedCode[12];
                    var h = combinedCode[13];
                    var a = combinedCode[6];
                    var b = combinedCode[7];

                    if (Exception == 10)
                    {
                        if (b == '9' && g == '9' && (a == '0' || a == '9'))
                        {
                            local = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 }.Concat(local.Skip(8)).ToArray();
                        }
                    }


                    if (Exception == 7 && g == '9')
                    {
                        local = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 }.Concat(local.Skip(8)).ToArray();
                    }

                    if (Exception == 2 || Exception == 9)
                    {
                        if (a != '0')
                        {
                            if (g != '9')
                            {
                                local = new int[] { 0, 0, 1, 2, 5, 3, 6, 4, 8, 7, 10, 9, 3, 1 };
                            }
                            else
                            {
                                local = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 8, 7, 10, 9, 3, 1 };
                            }
                        }
                    }

                    return calc(combinedCode, local);
                }

                internal bool DoubleAlternate(string combinedCode, int[] weightings)
                {
                    if (Exception == 3)
                    {
                        var c = combinedCode[8];
                        if (c == '6' || c == '9')
                        {
                            return true;
                        }
                    }

                    int pos = 0;
                    for (var i = 0; i < combinedCode.Length; i++)
                    {
                        //as int
                        var v = (combinedCode[i] - '1') + 1;
                        var weighted = v * weightings[i];

                        var tmp = weighted / 10;
                        pos += weighted / 10;
                        pos += weighted - (tmp * 10);
                    }

                    if (Exception == 1)
                    {
                        pos += 27;
                    }

                    var checkCalc = pos % 10;
                    var target = 0;
                    if (Exception == 5)
                    {
                        var h = combinedCode[13];
                        target = h - '1' + 1;

                        if (checkCalc == 0 && target == 0)
                        {
                            return true;
                        }
                        else
                        {
                            checkCalc = 10 - checkCalc;
                        }
                    }

                    return checkCalc == target;
                }

                internal bool StandardModulus10(string combinedCode, int[] weightings)
                {
                    return StandardModulus(combinedCode, 10, weightings);
                }

                internal bool StandardModulus11(string combinedCode, int[] weightings)
                {
                    var passed = StandardModulus(combinedCode, 11, weightings);

                    if (Exception == 14 && !passed)
                    {
                        var test = combinedCode.Last();
                        if (!new[] { '0', '1', '9' }.Contains(test))
                        {
                            return false;
                        }
                        else
                        {
                            combinedCode = combinedCode.Substring(0, 6) + '0' + combinedCode.Substring(6, 7);
                            passed = StandardModulus(combinedCode, 11, weightings);
                        }
                    }

                    return passed;
                }

                internal bool StandardModulus(string combinedCode, int mod, int[] weightings)
                {


                    int pos = 0;
                    for (var i = 0; i < combinedCode.Length; i++)
                    {
                        //as int
                        var v = (combinedCode[i] - '1') + 1;
                        pos += v * weightings[i];
                    }

                    var target = 0;
                    var chekCalc = pos % mod;

                    if (Exception == 4)
                    {
                        target = int.Parse(combinedCode.Substring(combinedCode.Length - 2, 2));
                    }

                    if (Exception == 5)
                    {
                        var g = combinedCode[12];
                        target = g - '1' + 1;

                        if (chekCalc == 0 && g == '0')
                        {
                            return true;
                        }
                        else if (chekCalc == 1)
                        {
                            return false;
                        }
                        else
                        {
                            chekCalc = 11 - chekCalc;
                        }
                    }

                    return chekCalc == target;
                }
            }
        }
    }
}
