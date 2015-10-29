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
        public static bool Email(this ValidatableValue<string> input)
        {
            try
            {
                if (new MailAddress(input.Value).Address != input.Value)
                {
                    input.AddError("Input doesn't match address part");
                }

                return true;
            }
            catch (Exception ex)
            {
                input.AddError(ex.Message);
            }


            return false;
        }

    }
}
