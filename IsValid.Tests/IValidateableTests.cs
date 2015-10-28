using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid.Tests
{
    [TestFixture]
    public class IValidateableTests
    {
        [Test]
        public void localeAvailibleFromApiCall()
        {
            var ivalidatable = "".IsValid("locale");
            Assert.That(ivalidatable.Locale, Contains.Item("locale"));
        }

        [Test]
        public void MultipleLocaleAvailibleFromApiCall()
        {
            var ivalidatable = "".IsValid("locale", "loc2");
            Assert.That(ivalidatable.Locale, Contains.Item("locale").And.Contains("loc2"));
        }


        [Test]
        [TestCase("en-GB")]
        [TestCase("en-US")]
        [TestCase("pl-PL")]
        public void LocaleFromThread(string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(locale);
            var ivalidatable = "".IsValid();
            Assert.AreEqual(locale, ivalidatable.Locale.Single());
        }
    }
}
