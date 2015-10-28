using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsValid
{
    public static class IsValidModel
    {
        public static bool Model(this IValidatableValue<object> input)
        {
            if (input.Value == null)
            {
                return false;
            }

            var context = new ValidationContext(input.Value);
            //context.MemberName = "Range";
            var results = new List<ValidationResult>();
            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(input.Value, context, results, true);

            return !results.Any();
        }
    }
}
