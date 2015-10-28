using System;
using System.Collections.Generic;

namespace IsValid
{
    public interface IValidatableValue<T>
    {
        bool IsValueSet { get; }

        T Value { get; }

        IEnumerable<string> Locale { get; }
    }
}