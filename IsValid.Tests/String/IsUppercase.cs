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
    public class IsUppercase
    {

        [Test]
        [TestCase("foo", ExpectedResult = false)]
        [TestCase("FOO", ExpectedResult = true)]
        [TestCase("123", ExpectedResult = true)]
        [TestCase("foo123", ExpectedResult = false)]
        [TestCase("FOO123", ExpectedResult = true)]
        [TestCase("Foo123", ExpectedResult = false)]
        public bool IsUppercaseTest(string value)
        {
            return value.IsValid().Uppercase();
        }
    }
}
