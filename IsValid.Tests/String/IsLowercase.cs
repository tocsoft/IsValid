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
    public class IsLowercase
    {

        [Test]
        [TestCase(null, ExpectedResult = true)]
        [TestCase("foo", ExpectedResult = true)]
        [TestCase("FOO", ExpectedResult = false)]
        [TestCase("123", ExpectedResult = true)]
        [TestCase("foo123", ExpectedResult = true)]
        [TestCase("FOO123", ExpectedResult = false)]
        [TestCase("Foo123", ExpectedResult = false)]
        public bool IsLowercaseTest(string value)
        {
            return value.IsValid().Lowercase();
        }
    }
}
