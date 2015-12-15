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
    //test cases taken from https://www.vocalink.com/customer-support/modulus-checking/
    [TestFixture]
    public class IsBankAccount_UK
    {

        [Test]
        [TestCase("202959", "63748472", true)]
        [TestCase("871427", "46238510", true)]
        [TestCase("872427", "46238510", true)]
        [TestCase("871427", "09123496", true)]
        [TestCase("871427", "99123496", true)]
        [TestCase("820000", "73688637", true)]
        [TestCase("827999", "73988638", true)]
        [TestCase("827101", "28748352", true)]
        [TestCase("134020", "63849203", true)]
        [TestCase("118765", "64371389", true)]
        [TestCase("200915", "41011166", true)]
        [TestCase("938611", "07806039", true)]
        [TestCase("938600", "42368003", true)]
        [TestCase("938063", "55065200", true)]
        [TestCase("772798", "99345694", true)]
        [TestCase("086090", "06774744", true)]
        [TestCase("309070", "02355688", true)]
        [TestCase("309070", "12345668", true)]
        [TestCase("309070", "12345677", true)]
        [TestCase("309070", "99345694", true)]
        [TestCase("938063", "15764273", false)]
        [TestCase("938063", "15764264", false)]
        [TestCase("938063", "15763217", false)]
        [TestCase("118765", "64371388", false)]
        [TestCase("203099", "66831036", false)]
        [TestCase("203099", "58716970", false)]
        [TestCase("089999", "66374959", false)]
        [TestCase("107999", "88837493", false)]
        [TestCase("074456", "12345112", true)]
        [TestCase("070116", "34012583", true)]
        [TestCase("074456", "11104102", true)]
        [TestCase("180002", "00000190", true)]


        [TestCase("20-29-59", "63748472", true)]
        [TestCase("87-14-27", "46238510", true)]
        [TestCase("87-24-27", "46238510", true)]
        [TestCase("87-14-27", "09123496", true)]
        [TestCase("87-14-27", "99123496", true)]
        [TestCase("82-00-00", "73688637", true)]
        [TestCase("82-79-99", "73988638", true)]
        [TestCase("82-71-01", "28748352", true)]
        [TestCase("13-40-20", "63849203", true)]
        [TestCase("11-87-65", "64371389", true)]
        [TestCase("20-09-15", "41011166", true)]
        [TestCase("93-86-11", "07806039", true)]
        [TestCase("93-86-00", "42368003", true)]
        [TestCase("93-80-63", "55065200", true)]
        [TestCase("77 27 98", "993-45 694", true)]
        [TestCase("08 60 90", "067-74 744", true)]
        [TestCase("30 90 70", "023-55 688", true)]
        [TestCase("30 90 70", "123-45 668", true)]
        [TestCase("30 90 70", "123-45 677", true)]
        [TestCase("30 90 70", "993-45 694", true)]
        [TestCase("93 80 63", "157-64 273", false)]
        [TestCase("93 80 63", "157-64 264", false)]
        [TestCase("93 80 63", "157-63 217", false)]
        [TestCase("11 87 65", "643-71 388", false)]
        [TestCase("20 30 99", "668-31 036", false)]
        [TestCase("20 30 99", "587-16 970", false)]
        [TestCase("08 99 99", "663-74 959", false)]
        [TestCase("10 79 99", "888-37 493", false)]
        [TestCase("07 44 56", "123-45 112", true)]
        [TestCase("07 01 16", "340-12 583", true)]
        [TestCase("07 44 56", "111-04 102", true)]
        [TestCase("18 00 02", "000-00 190", true)]
        public void IsValid(string sortCode, string accountNo, bool isValid)
        {
            var validator = accountNo.IsValid("en-GB");
            var result = validator.BankAccount(sortCode);
            Assert.AreEqual(isValid, result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void PassModulus10Check()
        {
            var validator = "66374958".IsValid("en-GB");
            var result = validator.BankAccount("089999");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }
        [Test]
        public void PassModulus11Check()
        {
            var validator = "88837491".IsValid("en-GB");
            var result = validator.BankAccount("107999");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void PassModulus11AndDoubleAlternateChecks()
        {
            var validator = "63748472".IsValid("en-GB");
            var result = validator.BankAccount("202959");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void Exception10And11PassThenFail()
        {
            var validator = "46238510".IsValid("en-GB");
            var result = validator.BankAccount("871427");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void Exception10And11FailThenPass()
        {
            var validator = "46238510".IsValid("en-GB");
            var result = validator.BankAccount("872427");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void Exception10_AB_09_G_9_PassThenFail()
        {
            var validator = "09123496".IsValid("en-GB");
            var result = validator.BankAccount("871427");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void Exception10_AB_99_G_9_PassThenFail()
        {
            var validator = "99123496".IsValid("en-GB");
            var result = validator.BankAccount("871427");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void Exception3_C6_IgnoreSecond()
        {
            var validator = "73688637".IsValid("en-GB");
            var result = validator.BankAccount("820000");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }
        [Test]
        public void Exception3_C9_IgnoreSecond()
        {
            var validator = "73988638".IsValid("en-GB");
            var result = validator.BankAccount("827999");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void Exception3_C_NOT_6_OR_9()
        {
            var validator = "28748352".IsValid("en-GB");
            var result = validator.BankAccount("827101");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test]
        public void Exception4_CheckDigit()
        {
            var validator = "63849203".IsValid("en-GB");
            var result = validator.BankAccount("134020");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }
        [Test]
        public void Exception1()
        {
            var validator = "64371389".IsValid("en-GB");
            var result = validator.BankAccount("118765");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 6 where the account fails standard check but is a foreign currency account.")]
        public void Exception6()
        {
            var validator = "41011166".IsValid("en-GB");
            var result = validator.BankAccount("200915");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 5 where the check passes.")]
        public void Exception5()
        {
            var validator = "07806039".IsValid("en-GB");
            var result = validator.BankAccount("938611");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 5 where the check passes with substitution")]
        public void Exception5_WithSubstitution()
        {
            var validator = "42368003".IsValid("en-GB");
            var result = validator.BankAccount("938600");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 5 where both checks produce a remainder of 0 and pass.")]
        public void Exception5_Remainder0()
        {
            var validator = "55065200".IsValid("en-GB");
            var result = validator.BankAccount("938063");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }


        [Test(Description = "Exception 7 where passes but would fail the standard check.")]
        public void Exception7_PassButFailStandard()
        {
            var validator = "99345694".IsValid("en-GB");
            var result = validator.BankAccount("772798");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }


        [Test(Description = "Exception 8 where the check passes")]
        public void Exception8_Passes()
        {
            var validator = "06774744".IsValid("en-GB");
            var result = validator.BankAccount("086090");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }



        [Test(Description = "Exception 2 & 9 where the first check passes.")]
        public void Exception2ANd9()
        {
            var validator = "02355688".IsValid("en-GB");
            var result = validator.BankAccount("309070");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }


        [Test(Description = "Exception 2 & 9 where the first check fails and second check passes with substitution.")]
        public void Exception2And9_FailPass()
        {
            var validator = "12345668".IsValid("en-GB");
            var result = validator.BankAccount("309070");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }
        
        [Test(Description = "Exception 2 & 9 where a≠0 and g≠9 and passes.")]
        public void Exception2And9_An0_Gn9_FailPass()
        {
            var validator = "12345677".IsValid("en-GB");
            var result = validator.BankAccount("309070");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 2 & 9 where a≠0 and g=9 and passes.")]
        public void Exception2And9_An0_G9_FailPass()
        {
            var validator = "99345694".IsValid("en-GB");
            var result = validator.BankAccount("309070");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }


        [Test(Description = "Exception 5 where the first checkdigit is correct and the second incorrect.")]
        public void Exception5_FirstCorrect_SendondIncorrect()
        {
            var validator = "15764273".IsValid("en-GB");
            var result = validator.BankAccount("938063");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }
        [Test(Description = "Exception 5 where the first checkdigit is incorrect and the second correct.")]
        public void Exception5_FirstinCorrect_SendondCorrect()
        {
            var validator = "15764264".IsValid("en-GB");
            var result = validator.BankAccount("938063");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 5 where the first checkdigit is incorrect with a remainder of 1")]
        public void Exception5_first_remainder1()
        {
            var validator = "15763217".IsValid("en-GB");
            var result = validator.BankAccount("938063");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 5 where the first checkdigit is incorrect with a remainder of 1")]
        public void Exception1_fail_double_alt()
        {
            var validator = "64371388".IsValid("en-GB");
            var result = validator.BankAccount("118765");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }


        [Test(Description = "Pass modulus 11 check and fail double alternate check.")]
        public void PassM11_FailDA()
        {
            var validator = "66831036".IsValid("en-GB");
            var result = validator.BankAccount("203099");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Fail modulus 11 check and pass double alternate check.")]
        public void FailM11_PassDA()
        {
            var validator = "58716970".IsValid("en-GB");
            var result = validator.BankAccount("203099");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Fail modulus 10 check")]
        public void FailM10()
        {
            var validator = "66374959".IsValid("en-GB");
            var result = validator.BankAccount("089999");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }
        [Test(Description = "Fail modulus 11 check")]
        public void FailM11()
        {
            var validator = "88837493".IsValid("en-GB");
            var result = validator.BankAccount("107999");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 12/13 where passes modulus 11 check (in this example, modulus 10 check fails, however, there is no need for it to be performed as the first check passed).")]
        public void Except12OR13()
        {
            var validator = "12345112".IsValid("en-GB");
            var result = validator.BankAccount("074456");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 12/13 where passes the modulus 11check (in this example, modulus 10 check passes as well, however, there is no need for it to be performed as the first check passed).")]
        public void Except12OR13_v2()
        {
            var validator = "34012583".IsValid("en-GB");
            var result = validator.BankAccount("070116");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 12/13 where fails the modulus 11 check, but passes the modulus 10 check.")]
        public void Except12OR13_FailPass()
        {
            var validator = "11104102".IsValid("en-GB");
            var result = validator.BankAccount("074456");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }

        [Test(Description = "Exception 14 where the first check fails and the second check passes.")]
        public void Except14_FailPass()
        {
            var validator = "00000190".IsValid("en-GB");
            var result = validator.BankAccount("180002");
            Assert.IsTrue(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }


        [Test(Description = "account code too short")]
        public void shortAccountCode()
        {
            var validator = "8888888".IsValid("en-GB");
            var result = validator.BankAccount("180002");
            Assert.IsFalse(result, validator.Errors.Select(x => x.ErrorMessage).FirstOrDefault());
        }
    }
}
