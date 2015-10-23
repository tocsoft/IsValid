using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsValid
{
    public struct ValidatableValue<T> : IValidatableValue<T>
    {
        private T _value;
        private bool _valueSet;
        public T Value { get { return _value; } }
        public bool IsValueSet { get { return _valueSet; } }

        public ValidatableValue(T val)
        {
            _value = val;
            _valueSet = true;
        }
    }
}
