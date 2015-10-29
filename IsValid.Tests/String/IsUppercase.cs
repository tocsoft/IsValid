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
    public class IsUppercase
    {

        [Test]
        [TestCase(null,true)]
        [TestCase("foo",false)]
        [TestCase("FOO",true)]
        [TestCase("123",true)]
        [TestCase("foo123",false)]
        [TestCase("FOO123",true)]
        [TestCase("Foo123",false)]
        public bool IsUppercaseTest(string value)
        {
            return value.IsValid().Uppercase();
        }
    }
}
