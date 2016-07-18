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
    public static class IsEmail
    {
        /// <summary>
        /// Determine whether input matches a valid email address.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Email(this ValidatableValue<string> input)
        {
            try
            {
                var validationResult = IsValid(input.Value);
                if (validationResult != null)
                {
                    input.AddError(validationResult);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                input.AddError(ex.Message);
            }


            return false;
        }

        //code adapted from  https://github.com/dotnet/corefx src/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/EmailAddressAttribute.cs
        private static string IsValid(string valueAsString)
        {
         
            if (valueAsString == null)
            {
                return null;
            }

            // only return true is there is only 1 '@' character
            // and it is neither the first nor the last character
            bool found = false;
            for (int i = 0; i < valueAsString.Length; i++)
            {
                if (valueAsString[i] == '@')
                {
                    if (found)
                    {
                        return "Found for than 1 '@' character";
                    }
                    if (i == 0)
                    {
                        return " Must not start with '@' character";
                    }
                    if (i == valueAsString.Length - 1)
                    {
                        return " Must not end with '@' character";
                    }
                   
                    found = true;
                }
            }
            if (found)
            {
                return null;
            }
            return "'@' character not found";
        }
    }
}
