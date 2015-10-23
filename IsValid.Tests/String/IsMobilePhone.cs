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
    public class IsMobilePhone
    {
        public IEnumerable<TestCaseData> TestCases
        {
            get
            {
                //copied from https://github.com/chriso/validator.js/blob/master/test/validators.js
                yield return new TestCaseData("07999999999", "en-GB").Returns(true);


                yield return new TestCaseData("15323456787", "zh-CN").Returns(true);
                yield return new TestCaseData("13523333233", "zh-CN").Returns(true);
                yield return new TestCaseData("13898728332", "zh-CN").Returns(true);
                yield return new TestCaseData("+086-13238234822", "zh-CN").Returns(true);
                yield return new TestCaseData("08613487234567", "zh-CN").Returns(true);
                yield return new TestCaseData("8617823492338", "zh-CN").Returns(true);
                yield return new TestCaseData("86-17823492338", "zh-CN").Returns(true);
                yield return new TestCaseData("12345", "zh-CN").Returns(false);
                yield return new TestCaseData("", "zh-CN").Returns(false);
                yield return new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "zh-CN").Returns(false);
                yield return new TestCaseData("010-38238383", "zh-CN").Returns(false);

                yield return new TestCaseData("0987123456", "zh-TW").Returns(true);
                yield return new TestCaseData("+886987123456", "zh-TW").Returns(true);
                yield return new TestCaseData("886987123456", "zh-TW").Returns(true);
                yield return new TestCaseData("+886-987123456", "zh-TW").Returns(true);
                yield return new TestCaseData("886-987123456", "zh-TW").Returns(true);
                yield return new TestCaseData("12345", "zh-TW").Returns(false);
                yield return new TestCaseData("", "zh-TW").Returns(false);
                yield return new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "zh-TW").Returns(false);
                yield return new TestCaseData("0-987123456", "zh-TW").Returns(false);

                yield return new TestCaseData("15323456787", "en").Returns(false);
                yield return new TestCaseData("13523333233", "en").Returns(false);
                yield return new TestCaseData("13898728332", "en").Returns(false);
                yield return new TestCaseData("+086-13238234822", "en").Returns(false);
                yield return new TestCaseData("08613487234567", "en").Returns(false);
                yield return new TestCaseData("8617823492338", "en").Returns(false);
                yield return new TestCaseData("86-17823492338", "en").Returns(false);

                yield return new TestCaseData("0821231234", "en-ZA").Returns(true);
                yield return new TestCaseData("+27821231234", "en-ZA").Returns(true);
                yield return new TestCaseData("27821231234", "en-ZA").Returns(true);
                yield return new TestCaseData("082123", "en-ZA").Returns(false);
                yield return new TestCaseData("08212312345", "en-ZA").Returns(false);
                yield return new TestCaseData("21821231234", "en-ZA").Returns(false);
                yield return new TestCaseData("+21821231234", "en-ZA").Returns(false);
                yield return new TestCaseData("+0821231234", "en-ZA").Returns(false);

                yield return new TestCaseData("61404111222", "en-AU").Returns(true);
                yield return new TestCaseData("+61411222333", "en-AU").Returns(true);
                yield return new TestCaseData("0417123456", "en-AU").Returns(true);
                yield return new TestCaseData("082123", "en-AU").Returns(false);
                yield return new TestCaseData("08212312345", "en-AU").Returns(false);
                yield return new TestCaseData("21821231234", "en-AU").Returns(false);
                yield return new TestCaseData("+21821231234", "en-AU").Returns(false);
                yield return new TestCaseData("+0821231234", "en-AU").Returns(false);

                yield return new TestCaseData("0612457898", "fr-FR").Returns(true);
                yield return new TestCaseData("+33612457898", "fr-FR").Returns(true);
                yield return new TestCaseData("33612457898", "fr-FR").Returns(true);
                yield return new TestCaseData("0712457898", "fr-FR").Returns(true);
                yield return new TestCaseData("+33712457898", "fr-FR").Returns(true);
                yield return new TestCaseData("33712457898", "fr-FR").Returns(true);

                yield return new TestCaseData("061245789", "fr-FR").Returns(false);
                yield return new TestCaseData("06124578980", "fr-FR").Returns(false);
                yield return new TestCaseData("0112457898", "fr-FR").Returns(false);
                yield return new TestCaseData("0212457898", "fr-FR").Returns(false);
                yield return new TestCaseData("0312457898", "fr-FR").Returns(false);
                yield return new TestCaseData("0412457898", "fr-FR").Returns(false);
                yield return new TestCaseData("0512457898", "fr-FR").Returns(false);
                yield return new TestCaseData("0812457898", "fr-FR").Returns(false);
                yield return new TestCaseData("0912457898", "fr-FR").Returns(false);
                yield return new TestCaseData("+34612457898", "fr-FR").Returns(false);
                yield return new TestCaseData("+336124578980", "fr-FR").Returns(false);
                yield return new TestCaseData("+3361245789", "fr-FR").Returns(false);

                yield return new TestCaseData("2102323234", "el-GR").Returns(true);
                yield return new TestCaseData("+302646041461", "el-GR").Returns(true);
                yield return new TestCaseData("+306944848966", "el-GR").Returns(true);
                yield return new TestCaseData("6944848966", "el-GR").Returns(true);

                yield return new TestCaseData("120000000", "el-GR").Returns(false);
                yield return new TestCaseData("20000000000", "el-GR").Returns(false);
                yield return new TestCaseData("68129485729", "el-GR").Returns(false);
                yield return new TestCaseData("6589394827", "el-GR").Returns(false);
                yield return new TestCaseData("298RI89572", "el-GR").Returns(false);

                yield return new TestCaseData("91234567", "en-HK").Returns(true);
                yield return new TestCaseData("9123-4567", "en-HK").Returns(true);
                yield return new TestCaseData("61234567", "en-HK").Returns(true);
                yield return new TestCaseData("51234567", "en-HK").Returns(true);
                yield return new TestCaseData("+85291234567", "en-HK").Returns(true);
                yield return new TestCaseData("+852-91234567", "en-HK").Returns(true);
                yield return new TestCaseData("+852-9123-4567", "en-HK").Returns(true);
                yield return new TestCaseData("852-91234567", "en-HK").Returns(true);

                yield return new TestCaseData("999", "en-HK").Returns(false);
                yield return new TestCaseData("+852-912345678", "en-HK").Returns(false);
                yield return new TestCaseData("123456789", "en-HK").Returns(false);
                yield return new TestCaseData("+852-1234-56789", "en-HK").Returns(false);

                // Lack of test data in original Validator.js.
                yield return new TestCaseData("+351919706735", "pt-PT").Returns(true);

                yield return new TestCaseData("447789345856", "en-GB").Returns(true);
                yield return new TestCaseData("+447861235675", "en-GB").Returns(true);
                yield return new TestCaseData("07888814488", "en-GB").Returns(true);
                yield return new TestCaseData("67699567", "en-GB").Returns(false);
                yield return new TestCaseData("0773894868", "en-GB").Returns(false);
                yield return new TestCaseData("077389f8688", "en-GB").Returns(false);
                yield return new TestCaseData("+07888814488", "en-GB").Returns(false);
                yield return new TestCaseData("0152456999", "en-GB").Returns(false);
                yield return new TestCaseData("442073456754", "en-GB").Returns(false);
                yield return new TestCaseData("+443003434751", "en-GB").Returns(false);
                yield return new TestCaseData("05073456754", "en-GB").Returns(false);
                yield return new TestCaseData("08001123123", "en-GB").Returns(false);

                yield return new TestCaseData("19876543210", "en-US").Returns(true);
                yield return new TestCaseData("8005552222", "en-US").Returns(true);
                yield return new TestCaseData("+15673628910", "en-US").Returns(true);
                yield return new TestCaseData("564785", "en-US").Returns(false);
                yield return new TestCaseData("0123456789", "en-US").Returns(false);
                yield return new TestCaseData("1437439210", "en-US").Returns(false);
                yield return new TestCaseData("8009112340", "en-US").Returns(false);
                yield return new TestCaseData("+10345672645", "en-US").Returns(false);
                yield return new TestCaseData("11435213543", "en-US").Returns(false);
                yield return new TestCaseData("2436119753", "en-US").Returns(false);
                yield return new TestCaseData("16532116190", "en-US").Returns(false);

                yield return new TestCaseData("0956684590", "en-ZM").Returns(true);
                yield return new TestCaseData("0966684590", "en-ZM").Returns(true);
                yield return new TestCaseData("0976684590", "en-ZM").Returns(true);
                yield return new TestCaseData("+260956684590", "en-ZM").Returns(true);
                yield return new TestCaseData("+260966684590", "en-ZM").Returns(true);
                yield return new TestCaseData("+260976684590", "en-ZM").Returns(true);
                yield return new TestCaseData("12345", "en-ZM").Returns(false);
                yield return new TestCaseData("", "en-ZM").Returns(false);
                yield return new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "en-ZM").Returns(false);
                yield return new TestCaseData("010-38238383", "en-ZM").Returns(false);
                yield return new TestCaseData("966684590", "en-ZM").Returns(false);
                yield return new TestCaseData("260976684590", "en-ZM").Returns(false);

                yield return new TestCaseData("+79676338855", "ru-RU").Returns(true);
                yield return new TestCaseData("79676338855", "ru-RU").Returns(true);
                yield return new TestCaseData("89676338855", "ru-RU").Returns(true);
                yield return new TestCaseData("9676338855", "ru-RU").Returns(true);
                yield return new TestCaseData("12345", "ru-RU").Returns(false);
                yield return new TestCaseData("", "ru-RU").Returns(false);
                yield return new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "ru-RU").Returns(false);
                yield return new TestCaseData("010-38238383", "ru-RU").Returns(false);
                yield return new TestCaseData("+9676338855", "ru-RU").Returns(false);
                yield return new TestCaseData("19676338855", "ru-RU").Returns(false);
                yield return new TestCaseData("6676338855", "ru-RU").Returns(false);
                yield return new TestCaseData("+99676338855", "ru-RU").Returns(false);

                yield return new TestCaseData("+4796338855", "nb-NO").Returns(true);
                yield return new TestCaseData("+4746338855", "nb-NO").Returns(true);
                yield return new TestCaseData("4796338855", "nb-NO").Returns(true);
                yield return new TestCaseData("4746338855", "nb-NO").Returns(true);
                yield return new TestCaseData("46338855", "nb-NO").Returns(true);
                yield return new TestCaseData("96338855", "nb-NO").Returns(true);
                yield return new TestCaseData("12345", "nb-NO").Returns(false);
                yield return new TestCaseData("", "nb-NO").Returns(false);
                yield return new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "nb-NO").Returns(false);
                yield return new TestCaseData("+4676338855", "nb-NO").Returns(false);
                yield return new TestCaseData("19676338855", "nb-NO").Returns(false);
                yield return new TestCaseData("+4726338855", "nb-NO").Returns(false);
                yield return new TestCaseData("4736338855", "nb-NO").Returns(false);
                yield return new TestCaseData("66338855", "nb-NO").Returns(false);

                yield return new TestCaseData("+4796338855", "nn-NO").Returns(true);
                yield return new TestCaseData("+4746338855", "nn-NO").Returns(true);
                yield return new TestCaseData("4796338855", "nn-NO").Returns(true);
                yield return new TestCaseData("4746338855", "nn-NO").Returns(true);
                yield return new TestCaseData("46338855", "nn-NO").Returns(true);
                yield return new TestCaseData("96338855", "nn-NO").Returns(true);
                yield return new TestCaseData("12345", "nn-NO").Returns(false);
                yield return new TestCaseData("", "nn-NO").Returns(false);
                yield return new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "nn-NO").Returns(false);
                yield return new TestCaseData("+4676338855", "nn-NO").Returns(false);
                yield return new TestCaseData("19676338855", "nn-NO").Returns(false);
                yield return new TestCaseData("+4726338855", "nn-NO").Returns(false);
                yield return new TestCaseData("4736338855", "nn-NO").Returns(false);
                yield return new TestCaseData("66338855", "nn-NO").Returns(false);
            }
        }

        [Test]
        [TestCaseSource("TestCases")]
        public bool IsMobilePhoneTestPassingLocale(string number, string locale)
        {
            return number.IsValid().MobilePhone(locale);
        }

        [Test]
        [TestCaseSource("TestCases")]
        public bool IsMobilePhoneTestPassingLocaleOnThread(string number, string locale)
        {

            var tst = new tmp()
            {
                phone = number,
                locale = locale
            };
            Thread t = new Thread(new ParameterizedThreadStart(ThreadExecute));
            t.Start(tst);

            t.Join();

            return tst.result;
        }

        public void ThreadExecute(object test)
        {
            var t = (tmp)test;
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(t.locale);
                t.result = t.phone.IsValid().MobilePhone();
            }
            catch (CultureNotFoundException ex)
            {
                //this is so the test cases still pass even when we are dealing with a Culture that .net/windows doesn't understand
                t.result = t.phone.IsValid().MobilePhone(t.locale);
            }
        }

        public class tmp
        {
            public string phone;
            public string locale;
            public bool result;
        }
    }
}
