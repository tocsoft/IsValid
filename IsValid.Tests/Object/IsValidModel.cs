using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsValid.Tests.Object
{
    [TestFixture]
    public class IsValidModel
    {
        [Test]
        public void ValidationMessagesSet()
        {
            var validatable = new temp
            {
                RequiredString = null,
                Range = 0
            }
            .IsValid();

            var r = validatable.Model();

            Assert.IsFalse(r);

            Assert.AreEqual(2, validatable.Errors.Count());
        }

        [Test]
        public void ExecuteRequiredStringValidatorIsValid()
        {

            var v = new temp
            {
                RequiredString = "string",
                Range = 50
            }
            .IsValid();
            var r = v.Model();

            Assert.IsTrue(r);
            Assert.IsTrue(v.IsValid);
            Assert.AreEqual(0, v.Errors.Count());
        }

        [Test]
        public void ExecuteRequiredStringValidator()
        {
            var v = new temp
            {
                RequiredString = null,
                Range = 50
            }
            .IsValid();
            var r = v.Model();

            Assert.IsFalse(r);
            Assert.IsFalse(v.IsValid);
            Assert.AreEqual(1, v.Errors.Count());
        }

        [Test]
        public void RangeInvalid()
        {
            var v = new temp
            {
                RequiredString = "str",
                Range = 1
            }
            .IsValid();
            var r = v.Model();

            Assert.IsFalse(v.IsValid);
            Assert.AreEqual(1, v.Errors.Count());
        }


        public class temp
        {
            [System.ComponentModel.DataAnnotations.Required]
            public string RequiredString { get; set; }

            [System.ComponentModel.DataAnnotations.Range(10, 100)]
            public int Range { get; set; }
        }
    }
}
