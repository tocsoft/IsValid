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
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("-", false)]
        [TestCase("-123", true)]
        [TestCase("123", true)]
        [TestCase("Foo", false)]
        [TestCase("123Foo123", false)]
        public void IsNumericTest(string value, bool expected)
        {
            Assert.AreEqual(expected, value.IsValid().Numeric());
        }
    }
}
