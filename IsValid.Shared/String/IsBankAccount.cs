using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using IsValid.BankAccounts;

namespace IsValid
{
    public static class IsBankAccount
    {
        static readonly IDictionary<string, Func<ValidatableValue<string>, string, bool>> validatorsWithBranch
            = new Dictionary<string, Func<ValidatableValue<string>, string, bool>> {
            { "en-GB", IsUKBankAccount.UKBankAccount}
        };

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
            var errors = new List<ValidationResult>();

            foreach (var l in inputV.Locale)
            {
                var validator = new ValidatableValue<string>(inputV.Value, l);

                if (validatorsWithBranch.ContainsKey(l))
                {
                    if (validatorsWithBranch[l](validator, branchNumber))
                    {
                        //is valid no errors
                        return true;
                    }
                }
                else
                {
                    validator.AddError($"Unable to validate banks for '{l}'");

                }
                errors.AddRange(validator.Errors);
            }

            //lets add all the validation errors (if invalid to the source validatable)
            foreach (var r in errors)
            {
                inputV.AddError(r);
            }

            return inputV.IsValid;
        }
    }
}
