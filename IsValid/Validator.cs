using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsValid
{
    public static class Validator
    {
        public static IValidatableValue<string> IsValid(this string value)
        {
            return new ValidatableValue<string>(value);
        }
    }
}
