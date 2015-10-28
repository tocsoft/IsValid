using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public static class Validator
    {
        public static IValidatableValue<string> IsValid(this string value, params string[] locale)
        {
            return new ValidatableValue<string>(value, locale);
        }

        public static IValidatableValue<string> IsValid(this string value)
        {
            return value.IsValid((string[])null);
        }
        
        public static IValidatableValue<object> IsValid(this object value, params string[] locale)
        {
            return new ValidatableValue<object>(value, locale);
        }

        public static IValidatableValue<object> IsValid(this object value)
        {
            return value.IsValid((string[])null);
        }
    }
}
