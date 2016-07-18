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
    public class IsIsbnTests
    {

        [Test]
        [TestCase("340101319X", true)]
        [TestCase("9784873113685", true)]
        [TestCase("3423214121", false)]
        [TestCase("9783836221190", false)]
        [TestCase("Foo", false)]
        public void IsIsbn(string input, bool expected)
        {
            var actual = input.IsValid().Isbn();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("0596004427", true)]
        [TestCase("0-596-00442-7", true)]
        [TestCase("0 596 00442 7", true)]
        [TestCase("161729134X", true)]
        [TestCase("1-617291-34-X", true)]
        [TestCase("1 617291 34 X", true)]
        [TestCase("3423214", false)]
        [TestCase("342321412122", false)]
        [TestCase("3423214121", false)]
        [TestCase("3-423-21412-1", false)]
        [TestCase("3 423 21412 1", false)]
        [TestCase("Foo", false)]
        public void IsIsbnVersion10(string input, bool expected)
        {
            var actual = input.IsValid().Isbn(IsbnVersion.Ten);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("9783836221191", true)]
        [TestCase("978-3-8362-2119-1", true)]
        [TestCase("978 3 8362 2119 1", true)]
        [TestCase("9784873113685", true)]
        [TestCase("978-4-87311-368-5", true)]
        [TestCase("978 4 87311 368 5", true)]
        [TestCase("9783836221190", false)]
        [TestCase("978-3-8362-2119-0", false)]
        [TestCase("978 3 8362 2119 0", false)]
        [TestCase("Foo", false)]
        public void IsIsbnVersion13(string input, bool expected)
        {
            var actual = input.IsValid().Isbn(IsbnVersion.Thirteen);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsIsbnnThrowsWhenSuppliedUnknownVersion()
        {
            const int version = 42;
            var message = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                "9784873113685".IsValid().Isbn((IsbnVersion)version);
            });

            StringAssert.Contains(
                "Isbn version " + version + " is not supported.",
                message.Message);
        }

    }
}
