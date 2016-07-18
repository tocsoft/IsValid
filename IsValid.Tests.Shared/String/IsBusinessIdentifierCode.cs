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
    public class IsBusinessIdentifierCode
    {

        [Test]
        [TestCase(0, true)]
        [TestCase(8, false)]
        [TestCase(10, true)]
        [TestCase(11, false)]
        [TestCase(12, true)]
        public void ValidateLength(int length, bool hasError)
        {
            var bic = "".PadLeft(length);
            var validator = bic.IsValid();
            var actual = validator.BusinessIdentifierCode();
            var messages = validator.Errors.Select(x => x.ErrorMessage);

            var toFind = "Invalid length";

            if (!hasError)
            {
                Assert.That(messages, Has.None.EqualTo(toFind));
            }
            else {
                Assert.That(messages, Has.Some.EqualTo(toFind));
            }
        }

        [Test]
        [TestCase("AAAABBCCXXX", false)]
        [TestCase("A23ABBCC", true)]
        public void IncorrectBusinessCode(string code, bool hasError)
        {
            var bic = code;
            var validator = bic.IsValid();
            var actual = validator.BusinessIdentifierCode();
            var messages = validator.Errors.Select(x => x.ErrorMessage);

            var toFind = "Invalid Institution Code";

            if (!hasError)
            {
                Assert.That(messages, Has.None.EqualTo(toFind));
            }
            else {
                Assert.That(messages, Has.Some.EqualTo(toFind));
            }
        }
        [Test]
        [TestCase("AAAAFFCCXXX", true)]
        [TestCase("AAAAGBCC", false)]
        public void IncorrectCountryCode(string code, bool hasError)
        {
            var bic = code;
            var validator = bic.IsValid();
            var actual = validator.BusinessIdentifierCode();
            var messages = validator.Errors.Select(x => x.ErrorMessage);

            var toFind = "Invalid Country Code";

            if (!hasError)
            {
                Assert.That(messages, Has.None.EqualTo(toFind));
            }
            else {
                Assert.That(messages, Has.Some.EqualTo(toFind));
            }
        }
        [Test]
        [TestCase("AAAAFF-CXXX", true)]
        [TestCase("AAAAGBCCXXX", false)]
        [TestCase("AAAAGB00XXX", false)]
        [TestCase("AAAAGBA5", false)]
        public void IncorrectLocationCode(string code, bool hasError)
        {
            var bic = code;
            var validator = bic.IsValid();
            var actual = validator.BusinessIdentifierCode();
            var messages = validator.Errors.Select(x => x.ErrorMessage);

            var toFind = "Invalid Location Code";

            if (!hasError)
            {
                Assert.That(messages, Has.None.EqualTo(toFind));
            }
            else {
                Assert.That(messages, Has.Some.EqualTo(toFind));
            }
        }

        [Test]
        [TestCase("AAAAFFCCX-X", true)]
        [TestCase("AAAAGBCCX1X", false)]
        [TestCase("AAAAGB0011X", false)]
        [TestCase("AAAAGBA5045", false)]
        [TestCase("AAAAGBA5", false)]//branch missing is valid
        public void IncorrectBranchCode(string code, bool hasError)
        {
            var bic = code;
            var validator = bic.IsValid();
            var actual = validator.BusinessIdentifierCode();
            var messages = validator.Errors.Select(x => x.ErrorMessage);

            var toFind = "Invalid Branch Code";

            if (!hasError)
            {
                Assert.That(messages, Has.None.EqualTo(toFind));
            }
            else {
                Assert.That(messages, Has.Some.EqualTo(toFind));
            }
        }


    }
}
