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
        [TestCase(null, true)]
        [TestCase("foo", true)]
        [TestCase("FOO", false)]
        [TestCase("123", true)]
        [TestCase("foo123", true)]
        [TestCase("FOO123", false)]
        [TestCase("Foo123", false)]
        public void IsLowercaseTest(string value, bool expected)
        {
            Assert.AreEqual(expected, value.IsValid().Lowercase());
        }
    }
}
