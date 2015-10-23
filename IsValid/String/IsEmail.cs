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
    public static class IsEmail
    {
        /// <summary>
        /// Determine whether input matches a valid email address.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Email(this IValidatableValue<string> input)
        {
            try
            {
                return new MailAddress(input.Value).Address == input.Value;
            }
            catch
            {
                return false;
            }
        }

    }
}
