using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using IsValid;
using System.Threading;
using System.Globalization;

#if PCL
namespace IsValid.PCL.Tests.String
#else
namespace IsValid.Tests.String
#endif
{
    [TestFixture]
    public class IsEmail
    {

        [Test]
        [TestCase("foo@bar.com", true)]
        [TestCase("foo@bar.com.au", true)]
        [TestCase("foo+bar@bar.com", true)]
        [TestCase("invalidemail@", false)]
        [TestCase("invalid.com", false)]
        [TestCase("@invalid.com", false)]
        public void TestEmail(string value, bool expected)
        {
            var v = value.IsValid();
            var r = v.Email();

            Assert.AreEqual(expected, r);
            Assert.AreEqual(expected ? 0 : 1, v.Errors.Count());
        }
    }
}
