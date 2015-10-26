using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using IsValid;
using System.Threading;
using System.Globalization;

namespace IsValid.Tests.String
{
    [TestFixture]
    public class IsNumeric
    {

        [Test]
        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("-", ExpectedResult = false)]
        [TestCase("-123", ExpectedResult = true)]
        [TestCase("123", ExpectedResult = true)]
        [TestCase("Foo", ExpectedResult = false)]
        [TestCase("123Foo123", ExpectedResult = false)]
        public bool IsNumericTest(string value)
        {
            return value.IsValid().Numeric();
        }
    }
}
