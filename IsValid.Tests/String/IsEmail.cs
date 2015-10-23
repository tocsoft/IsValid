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
    public class IsEmail
    {

        [Test]
        [TestCase("foo@bar.com", ExpectedResult = true)]
        [TestCase("foo@bar.com.au", ExpectedResult = true)]
        [TestCase("foo+bar@bar.com", ExpectedResult = true)]
        [TestCase("invalidemail@", ExpectedResult = false)]
        [TestCase("invalid.com", ExpectedResult = false)]
        [TestCase("@invalid.com", ExpectedResult = false)]
        public bool TestEmail(string value)
        {
            return value.IsValid().Email();
        }
    }
}
