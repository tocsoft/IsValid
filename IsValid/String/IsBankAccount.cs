using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public static partial class IsBankAccount
    {
        static IsBankAccount()
        {
            LoadUKValidationRules("data.bank.uk.weightings.txt", "data.bank.uk.sortcode.txt");
        }

        public static bool BankAccount(this ValidatableValue<string> inputV)
        {
            return inputV.BankAccount(null);
        }

        /// <summary>
        /// Banks the account.
        /// </summary>
        /// <param name="branchNumber">The combind branch/bank number. I.E. UK sort code</param>
        /// <returns></returns>
        public static bool BankAccount(this ValidatableValue<string> inputV, string branchNumber)
        {
            var validatables = inputV.Locale.Select(l => new ValidatableValue<string>(inputV.Value, l)).ToList();

            foreach (var l in validatables)
            {
                var loc = l.Locale.Single();
                if (loc == "en-GB")
                {
                    UK.Validate(inputV, branchNumber);
                }
                else
                {
                    l.AddError($"Unable to validate banks for {loc}");
                }
            }

            //if none are valid lets show us some validation errors
            if (!validatables.Any(x => x.IsValid))
            {
                var res = validatables.First();
                foreach (var r in res.Errors)
                {
                    inputV.AddError(r);
                }
            }

            return inputV.IsValid;
        }

    }
}
