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
        [TestCase("375556917985515", ExpectedResult = true)]
        [TestCase("36050234196908", ExpectedResult = true)]
        [TestCase("4716461583322103", ExpectedResult = true)]
        [TestCase("4716-2210-5188-5662", ExpectedResult = true)]
        [TestCase("4929 7226 5379 7141", ExpectedResult = true)]
        [TestCase("5398228707871527", ExpectedResult = true)]
        [TestCase("Foo", ExpectedResult = false)]
        [TestCase("Bar123", ExpectedResult = false)]
        [TestCase("5398228707871528", ExpectedResult = false)]

        public bool TestCreditCard(string value)
        {
            return value.IsValid().CreditCard();
        }
    }
}
