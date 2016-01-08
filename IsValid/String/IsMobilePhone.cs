using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public static class IsMobilePhone
    {
        private static Dictionary<string, Validator> LocaleMobilePhoneRegexes = new Dictionary<string, Validator>
        {
            { "zh-CN", new Validator(@"^(\+?0?86\-?)?1[345789][0-9]{9}$") },
            { "zh-TW", new Validator(@"^(\+?886\-?|0)?9\d{8}$") },
            { "en-ZA", new Validator(@"^(\+?27|0)(\d{9})$") },
            { "en-AU", new Validator(@"^(\+?61|0)4(\d{8})$") },
            { "fr-FR", new Validator(@"^(\+?33|0)(6|7)\d{8}$") },
            { "en-HK", new Validator(@"^(\+?852\-?)?[569]\d{3}\-?\d{4}$") },
            { "pt-PT", new Validator(@"^(\+351)?9[1236]\d{7}$") },
            { "el-GR", new Validator(@"^(\+30)?((2\d{9})|(69\d{8}))$") },
            { "en-GB", new Validator(@"^(\+?44|0)7\d{9}$", " ", "-") },
            { "en-US", new Validator(@"^(\+?1)?[2-9]\d{2}[2-9](?!11)\d{6}$") },
            { "en-ZM", new Validator(@"^(\+26)?09[567]\d{7}$") },
            { "ru-RU", new Validator(@"^(\+?7|8)?9\d{9}$") },
            { "nb-NO", new Validator(@"^(\+?47)?[49]\d{7}$") },
            { "nn-NO", new Validator(@"^(\+?47)?[49]\d{7}$") },
            { "nl-NL", new Validator(@"^(\+31|0)6\d{7}$") }
        };

        private static bool IsLocalPhone(ValidatableValue<string> phoneNumber, string locale, string exitCode)
        {
            if (!phoneNumber.IsValueSet || string.IsNullOrWhiteSpace(phoneNumber.Value))
            {
                return false;
            }

            Validator validator;
            if (LocaleMobilePhoneRegexes.TryGetValue(locale, out validator))
            {
                return validator.Validate(phoneNumber.Value, exitCode);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the given phone number is a mobile phone number or not. Based on the current locale of the executing thread
        /// </summary>
        /// <param name="phoneNumber">The phone number to check.</param>
        /// <returns>True if it is a mobile phone number, false otherwise.</returns>
        /// <remarks>
        /// Relies on locales that use specific blocks of numbers for mobile phone numbers.
        /// </remarks>
        public static bool MobilePhone(this ValidatableValue<string> phoneNumber)
        {
            return MobilePhone(phoneNumber, null);
        }

        /// <summary>
        /// Determines whether the given phone number is a mobile phone number or not. Based on the current locale of the executing thread
        /// </summary>
        /// <param name="phoneNumber">The phone number to check.</param>
        /// <param name="exitCode">The exit code.</param>
        /// <returns>
        /// True if it is a mobile phone number, false otherwise.
        /// </returns>
        /// <remarks>
        /// Relies on locales that use specific blocks of numbers for mobile phone numbers.
        /// </remarks>
        public static bool MobilePhone(this ValidatableValue<string> phoneNumber, string exitCode)
        {
            if (!phoneNumber.Locale.Any(loc => IsLocalPhone(phoneNumber, loc, exitCode)))
            {
                phoneNumber.AddError("Not a recognised mobile phone number");
            }
            return phoneNumber.IsValid;
        }

        private class Validator
        {
            private readonly string[] _ignoredCharacters;
            private readonly Regex _regex;

            public Validator(string regexPatterns, params string[] ignoredCharacters)
            {
                _regex = new Regex(regexPatterns, RegexOptions.Compiled);
                _ignoredCharacters = ignoredCharacters ?? new string[0];
            }

            public bool Validate(string number, string exitCode)
            {
                foreach (var c in _ignoredCharacters)
                {
                    number = number.Replace(c, "");
                }

                if (exitCode != null && number.StartsWith(exitCode, StringComparison.OrdinalIgnoreCase))
                {
                    number = "+" + number.Substring(exitCode.Length);
                }


                return _regex.IsMatch(number);
            }
        }
    }
}
