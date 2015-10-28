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
        public void ExecuteRequiredStringValidatorIsValid()
        {

            var r = new temp
            {
                RequiredString = "string",
                Range = 50
            }
            .IsValid()
            .Model();

            Assert.IsTrue(r);
        }

        [Test]
        public void ExecuteRequiredStringValidator()
        {
            var r = new temp
            {
                RequiredString = null,
                Range = 50
            }
            .IsValid()
            .Model();

            Assert.IsFalse(r);
        }

        [Test]
        public void RangeInvalid()
        {
            var r = new temp
            {
                RequiredString = "str",
                Range = 1
            }
            .IsValid()
            .Model();

            Assert.IsFalse(r);
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
