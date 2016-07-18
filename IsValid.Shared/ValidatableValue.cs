using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public struct ValidatableValue<T>
    {
        private T _value;
        private bool _valueSet;
        private readonly IEnumerable<string> _locales;
        private readonly List<ValidationResult> _errors;

        public T Value { get { return _value; } }

        public bool IsValueSet { get { return _valueSet; } }

        public IEnumerable<string> Locale { get { return _locales; } }

        public IEnumerable<ValidationResult> Errors { get { return _errors ?? Enumerable.Empty<ValidationResult>(); } }

        public bool IsValid { get { return !Errors.Any(); } }

        public ValidatableValue(T val, params string[] locale)
        {
            _value = val;
            _valueSet = true;

            if (locale == null || locale.Length == 0)
            {
                _locales = new[] { Thread.CurrentThread.CurrentUICulture.Name };
            }
            else
            {
                _locales = locale;
            }
            _errors = new List<ValidationResult>();
        }

        internal void AddError(string error)
        {
            _errors?.Add(new ValidationResult(error));
        }

        internal void AddError(ValidationResult error)
        {
            _errors?.Add(error);
        }

        internal void AddError(IEnumerable<ValidationResult> errors)
        {
            foreach (var e in errors)
            {
                _errors?.Add(e);
            }
        }
    }
}
