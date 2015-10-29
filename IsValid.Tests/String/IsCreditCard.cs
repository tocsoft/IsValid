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
    public class IsCreditCard
    {

        [Test]
        [TestCase("375556917985515", true)]
        [TestCase("36050234196908", true)]
        [TestCase("4716461583322103", true)]
        [TestCase("4716-2210-5188-5662", true)]
        [TestCase("4929 7226 5379 7141", true)]
        [TestCase("5398228707871527", true)]
        [TestCase("Foo", false)]
        [TestCase("Bar123", false)]
        [TestCase("5398228707871528", false)]

        public void TestCreditCard(string value, bool expected)
        {
            var v = value.IsValid();
            var r = v.CreditCard();

            Assert.AreEqual(expected, r);
            Assert.AreEqual(expected ? 0 : 1, v.Errors.Count());
        }
    }
}
