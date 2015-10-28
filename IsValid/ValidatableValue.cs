using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public struct ValidatableValue<T> : IValidatableValue<T>
    {
        private T _value;
        private bool _valueSet;
        private readonly IEnumerable<string> _locales;

        public T Value { get { return _value; } }
        public bool IsValueSet { get { return _valueSet; } }

        public IEnumerable<string> Locale { get { return _locales; } }

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
        }
    }
}
