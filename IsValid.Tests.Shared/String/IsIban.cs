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
    public class IsIbanTests
    {

        [Test]
        [TestCase("GB19 LOYD 3096 1700 7099 43")]
        [TestCase("DK50 0040 0440 1162 43")]
        [TestCase("AL47 2121 1009 0000 0002 3569 8741")]
        [TestCase("AD12 0001 2030 2003 5910 0100")]
        [TestCase("AT61 1904 3002 3457 3201")]
        [TestCase("AZ21 NABZ 0000 0000 1370 1000 1944")]
        [TestCase("BH67 BMAG 0000 1299 1234 56")]
        [TestCase("BE62 5100 0754 7061")]
        [TestCase("BA39 1290 0794 0102 8494")]
        [TestCase("BG80 BNBG 9661 1020 3456 78")]
        [TestCase("HR12 1001 0051 8630 0016 0")]
        [TestCase("CY17 0020 0128 0000 0012 0052 7600")]
        [TestCase("CZ65 0800 0000 1920 0014 5399")]
        [TestCase("EE38 2200 2210 2014 5685")]
        [TestCase("FO97 5432 0388 8999 44")]
        [TestCase("FI21 1234 5600 0007 85")]
        [TestCase("FR14 2004 1010 0505 0001 3M02 606")]
        [TestCase("GE29 NB00 0000 0101 9049 17")]
        [TestCase("DE89 3704 0044 0532 0130 00")]
        [TestCase("GI75 NWBK 0000 0000 7099 453")]
        [TestCase("GR16 0110 1250 0000 0001 2300 695")]
        [TestCase("GL56 0444 9876 5432 10")]
        [TestCase("HU42 1177 3016 1111 1018 0000 0000")]
        [TestCase("IS14 0159 2600 7654 5510 7303 39")]
        [TestCase("IE29 AIBK 9311 5212 3456 78")]
        [TestCase("IL62 0108 0000 0009 9999 999")]
        [TestCase("IT40 S054 2811 1010 0000 0123 456")]
        [TestCase("JO94 CBJO 0010 0000 0000 0131 0003 02")]
        [TestCase("KW81 CBKU 0000 0000 0000 1234 5601 01")]
        [TestCase("LV80 BANK 0000 4351 9500 1")]
        [TestCase("LB62 0999 0000 0001 0019 0122 9114")]
        [TestCase("LI21 0881 0000 2324 013A A")]
        [TestCase("LT12 1000 0111 0100 1000")]
        [TestCase("LU28 0019 4006 4475 0000")]
        [TestCase("MK072 5012 0000 0589 84")]
        [TestCase("MT84 MALT 0110 0001 2345 MTLC AST0 01S")]
        [TestCase("MU17 BOMM 0101 1010 3030 0200 000M UR")]
        [TestCase("MD24 AG00 0225 1000 1310 4168")]
        [TestCase("MC93 2005 2222 1001 1223 3M44 555")]
        [TestCase("ME25 5050 0001 2345 6789 51")]
        [TestCase("NL39 RABO 0300 0652 64")]
        [TestCase("NO93 8601 1117 947")]
        [TestCase("PK36 SCBL 0000 0011 2345 6702")]
        [TestCase("PL60 1020 1026 0000 0422 7020 1111")]
        [TestCase("PT50 0002 0123 1234 5678 9015 4")]
        [TestCase("QA58 DOHB 0000 1234 5678 90AB CDEF G")]
        [TestCase("RO49 AAAA 1B31 0075 9384 0000")]
        [TestCase("SM86 U032 2509 8000 0000 0270 100")]
        [TestCase("SA03 8000 0000 6080 1016 7519")]
        [TestCase("RS35 2600 0560 1001 6113 79")]
        [TestCase("SK31 1200 0000 1987 4263 7541")]
        [TestCase("SI56 1910 0000 0123 438")]
        [TestCase("ES80 2310 0001 1800 0001 2345")]
        [TestCase("SE35 5000 0000 0549 1000 0003")]
        [TestCase("CH93 0076 2011 6238 5295 7")]
        [TestCase("TN59 1000 6035 1835 9847 8831")]
        [TestCase("TR33 0006 1005 1978 6457 8413 26")]
        [TestCase("AE07 0331 2345 6789 0123 456")]
        public void IsIbanValid(string input)
        {
            var validator = input.IsValid();
            var actual = validator.Iban();
            Assert.IsTrue(actual, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [TestCase("GB02 LOYD 0899 9966 3749 18")]
        public void UKAcocuntNumberInvalid(string input)
        {
            var validator = input.IsValid();
            var actual = validator.Iban();
            var error = validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault();
            Assert.AreEqual("Invalid account details", error);
        }

        [Test]
        //no country codes
        [TestCase("3423214121")]
        [TestCase("9783836221190")]
        public void IsIbanInvalidCountry(string input)
        {
            var validator = input.IsValid();
            var actual = validator.Iban();
            Assert.AreEqual("Unrecognised country code", validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        [TestCase("AE07 0331 2345 6789 023 456")]
        public void IsIbanInvalidLength(string input)
        {
            var validator = input.IsValid();
            var actual = validator.Iban();
            Assert.AreEqual("Invalid length", validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        //Invalid checksums
        [Test]
        [TestCase("AE01 0331 2345 6789 0123 456")]
        public void IsIbanInvalidChecksums(string input)
        {
            var validator = input.IsValid();
            var actual = validator.Iban();
            Assert.AreEqual("Invalid check digit", validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }
    }
}
