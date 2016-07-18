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
    public class IsMobilePhone
    {
        public IEnumerable<TestCaseData> TestCases = new[] {
                //copied from https://github.com/chriso/validator.js/blob/master/test/validators.js

                new TestCaseData("15323456787", "zh-CN", "00").Returns(true),
                new TestCaseData("13523333233", "zh-CN", "00").Returns(true),
                new TestCaseData("13898728332", "zh-CN", "00").Returns(true),
                new TestCaseData("+086-13238234822", "zh-CN", "00").Returns(true),
                new TestCaseData("08613487234567", "zh-CN", "00").Returns(true),
                new TestCaseData("8617823492338", "zh-CN", "00").Returns(true),
                new TestCaseData("86-17823492338", "zh-CN", "00").Returns(true),
                new TestCaseData("12345", "zh-CN", "00").Returns(false),
                new TestCaseData("", "zh-CN", "00").Returns(false),
                new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "zh-CN", "00").Returns(false),
                new TestCaseData("010-38238383", "zh-CN", "00").Returns(false),

                new TestCaseData("0987123456", "zh-TW", "00").Returns(true),
                new TestCaseData("+886987123456", "zh-TW", "00").Returns(true),
                new TestCaseData("886987123456", "zh-TW", "00").Returns(true),
                new TestCaseData("+886-987123456", "zh-TW", "00").Returns(true),
                new TestCaseData("886-987123456", "zh-TW", "00").Returns(true),
                new TestCaseData("12345", "zh-TW", "00").Returns(false),
                new TestCaseData("", "zh-TW", "00").Returns(false),
                new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "zh-TW", "00").Returns(false),
                new TestCaseData("0-987123456", "zh-TW", "00").Returns(false),

                new TestCaseData("15323456787", "en", "00").Returns(false),
                new TestCaseData("13523333233", "en", "00").Returns(false),
                new TestCaseData("13898728332", "en", "00").Returns(false),
                new TestCaseData("+086-13238234822", "en", "00").Returns(false),
                new TestCaseData("08613487234567", "en", "00").Returns(false),
                new TestCaseData("8617823492338", "en", "00").Returns(false),
                new TestCaseData("86-17823492338", "en", "00").Returns(false),

                new TestCaseData("0821231234", "en-ZA", "00").Returns(true),
                new TestCaseData("+27821231234", "en-ZA", "00").Returns(true),
                new TestCaseData("27821231234", "en-ZA", "00").Returns(true),
                new TestCaseData("082123", "en-ZA", "00").Returns(false),
                new TestCaseData("08212312345", "en-ZA", "00").Returns(false),
                new TestCaseData("21821231234", "en-ZA", "00").Returns(false),
                new TestCaseData("+21821231234", "en-ZA", "00").Returns(false),
                new TestCaseData("+0821231234", "en-ZA", "00").Returns(false),

                new TestCaseData("61404111222", "en-AU", "00").Returns(true),
                new TestCaseData("+61411222333", "en-AU", "00").Returns(true),
                new TestCaseData("0417123456", "en-AU", "00").Returns(true),
                new TestCaseData("082123", "en-AU", "00").Returns(false),
                new TestCaseData("08212312345", "en-AU", "00").Returns(false),
                new TestCaseData("21821231234", "en-AU", "00").Returns(false),
                new TestCaseData("+21821231234", "en-AU", "00").Returns(false),
                new TestCaseData("+0821231234", "en-AU", "00").Returns(false),

                new TestCaseData("0612457898", "fr-FR", "00").Returns(true),
                new TestCaseData("+33612457898", "fr-FR", "00").Returns(true),
                new TestCaseData("33612457898", "fr-FR", "00").Returns(true),
                new TestCaseData("0712457898", "fr-FR", "00").Returns(true),
                new TestCaseData("+33712457898", "fr-FR", "00").Returns(true),
                new TestCaseData("33712457898", "fr-FR", "00").Returns(true),

                new TestCaseData("061245789", "fr-FR", "00").Returns(false),
                new TestCaseData("06124578980", "fr-FR", "00").Returns(false),
                new TestCaseData("0112457898", "fr-FR", "00").Returns(false),
                new TestCaseData("0212457898", "fr-FR", "00").Returns(false),
                new TestCaseData("0312457898", "fr-FR", "00").Returns(false),
                new TestCaseData("0412457898", "fr-FR", "00").Returns(false),
                new TestCaseData("0512457898", "fr-FR", "00").Returns(false),
                new TestCaseData("0812457898", "fr-FR", "00").Returns(false),
                new TestCaseData("0912457898", "fr-FR", "00").Returns(false),
                new TestCaseData("+34612457898", "fr-FR", "00").Returns(false),
                new TestCaseData("+336124578980", "fr-FR", "00").Returns(false),
                new TestCaseData("+3361245789", "fr-FR", "00").Returns(false),

                new TestCaseData("2102323234", "el-GR", "00").Returns(true),
                new TestCaseData("+302646041461", "el-GR", "00").Returns(true),
                new TestCaseData("+306944848966", "el-GR", "00").Returns(true),
                new TestCaseData("6944848966", "el-GR", "00").Returns(true),

                new TestCaseData("120000000", "el-GR", "00").Returns(false),
                new TestCaseData("20000000000", "el-GR", "00").Returns(false),
                new TestCaseData("68129485729", "el-GR", "00").Returns(false),
                new TestCaseData("6589394827", "el-GR", "00").Returns(false),
                new TestCaseData("298RI89572", "el-GR", "00").Returns(false),

                new TestCaseData("91234567", "en-HK", "00").Returns(true),
                new TestCaseData("9123-4567", "en-HK", "00").Returns(true),
                new TestCaseData("61234567", "en-HK", "00").Returns(true),
                new TestCaseData("51234567", "en-HK", "00").Returns(true),
                new TestCaseData("+85291234567", "en-HK", "00").Returns(true),
                new TestCaseData("+852-91234567", "en-HK", "00").Returns(true),
                new TestCaseData("+852-9123-4567", "en-HK", "00").Returns(true),
                new TestCaseData("852-91234567", "en-HK", "00").Returns(true),

                new TestCaseData("999", "en-HK", "00").Returns(false),
                new TestCaseData("+852-912345678", "en-HK", "00").Returns(false),
                new TestCaseData("123456789", "en-HK", "00").Returns(false),
                new TestCaseData("+852-1234-56789", "en-HK", "00").Returns(false),

                // Lack of test data in original Validator.js.
                new TestCaseData("+351919706735", "pt-PT", "00").Returns(true),

                new TestCaseData("447700956823", "en-GB", "00").Returns(true),
                new TestCaseData("+447700956823", "en-GB", "00").Returns(true),
                new TestCaseData("07700956823", "en-GB", "00").Returns(true),
                new TestCaseData("00447700956823", "en-GB", "00").Returns(true),
                new TestCaseData("67699567", "en-GB", "00").Returns(false),
                new TestCaseData("0770095682", "en-GB", "00").Returns(false),
                new TestCaseData("077009f6823", "en-GB", "00").Returns(false),
                new TestCaseData("+07700956823", "en-GB", "00").Returns(false),
                new TestCaseData("0152456999", "en-GB", "00").Returns(false),
                new TestCaseData("442073456754", "en-GB", "00").Returns(false),
                new TestCaseData("+443003434751", "en-GB", "00").Returns(false),
                new TestCaseData("05073456754", "en-GB", "00").Returns(false),
                new TestCaseData("08001123123", "en-GB", "00").Returns(false),

                new TestCaseData("19876543210", "en-US", "00").Returns(true),
                new TestCaseData("8005552222", "en-US", "00").Returns(true),
                new TestCaseData("+15673628910", "en-US", "00").Returns(true),
                new TestCaseData("0015673628910", "en-US", "00").Returns(true),
                new TestCaseData("564785", "en-US", "00").Returns(false),
                new TestCaseData("0123456789", "en-US", "00").Returns(false),
                new TestCaseData("1437439210", "en-US", "00").Returns(false),
                new TestCaseData("8009112340", "en-US", "00").Returns(false),
                new TestCaseData("+10345672645", "en-US", "00").Returns(false),
                new TestCaseData("11435213543", "en-US", "00").Returns(false),
                new TestCaseData("2436119753", "en-US", "00").Returns(false),
                new TestCaseData("16532116190", "en-US", "00").Returns(false),

                new TestCaseData("0956684590", "en-ZM", "00").Returns(true),
                new TestCaseData("0966684590", "en-ZM", "00").Returns(true),
                new TestCaseData("0976684590", "en-ZM", "00").Returns(true),
                new TestCaseData("+260956684590", "en-ZM", "00").Returns(true),
                new TestCaseData("+260966684590", "en-ZM", "00").Returns(true),
                new TestCaseData("+260976684590", "en-ZM", "00").Returns(true),
                new TestCaseData("00260976684590", "en-ZM", "00").Returns(true),
                new TestCaseData("12345", "en-ZM", "00").Returns(false),
                new TestCaseData("", "en-ZM", "00").Returns(false),
                new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "en-ZM", "00").Returns(false),
                new TestCaseData("010-38238383", "en-ZM", "00").Returns(false),
                new TestCaseData("966684590", "en-ZM", "00").Returns(false),
                new TestCaseData("260976684590", "en-ZM", "00").Returns(false),

                new TestCaseData("+79676338855", "ru-RU", "00").Returns(true),
                new TestCaseData("0079676338855", "ru-RU", "00").Returns(true),
                new TestCaseData("79676338855", "ru-RU", "00").Returns(true),
                new TestCaseData("89676338855", "ru-RU", "00").Returns(true),
                new TestCaseData("9676338855", "ru-RU", "00").Returns(true),
                new TestCaseData("12345", "ru-RU", "00").Returns(false),
                new TestCaseData("", "ru-RU", "00").Returns(false),
                new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "ru-RU", "00").Returns(false),
                new TestCaseData("010-38238383", "ru-RU", "00").Returns(false),
                new TestCaseData("+9676338855", "ru-RU", "00").Returns(false),
                new TestCaseData("19676338855", "ru-RU", "00").Returns(false),
                new TestCaseData("6676338855", "ru-RU", "00").Returns(false),
                new TestCaseData("+99676338855", "ru-RU", "00").Returns(false),

                new TestCaseData("004796338855", "nb-NO", "00").Returns(true),
                new TestCaseData("+4796338855", "nb-NO", "00").Returns(true),
                new TestCaseData("+4746338855", "nb-NO", "00").Returns(true),
                new TestCaseData("4796338855", "nb-NO", "00").Returns(true),
                new TestCaseData("4746338855", "nb-NO", "00").Returns(true),
                new TestCaseData("46338855", "nb-NO", "00").Returns(true),
                new TestCaseData("96338855", "nb-NO", "00").Returns(true),
                new TestCaseData("12345", "nb-NO", "00").Returns(false),
                new TestCaseData("", "nb-NO", "00").Returns(false),
                new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "nb-NO", "00").Returns(false),
                new TestCaseData("+4676338855", "nb-NO", "00").Returns(false),
                new TestCaseData("19676338855", "nb-NO", "00").Returns(false),
                new TestCaseData("+4726338855", "nb-NO", "00").Returns(false),
                new TestCaseData("4736338855", "nb-NO", "00").Returns(false),
                new TestCaseData("66338855", "nb-NO", "00").Returns(false),

                new TestCaseData("004796338855", "nn-NO", "00").Returns(true),
                new TestCaseData("+4796338855", "nn-NO", "00").Returns(true),
                new TestCaseData("+4746338855", "nn-NO", "00").Returns(true),
                new TestCaseData("4796338855", "nn-NO", "00").Returns(true),
                new TestCaseData("4746338855", "nn-NO", "00").Returns(true),
                new TestCaseData("46338855", "nn-NO", "00").Returns(true),
                new TestCaseData("96338855", "nn-NO", "00").Returns(true),
                new TestCaseData("12345", "nn-NO", "00").Returns(false),
                new TestCaseData("", "nn-NO", "00").Returns(false),
                new TestCaseData("Vml2YW11cyBmZXJtZtesting123", "nn-NO", "00").Returns(false),
                new TestCaseData("+4676338855", "nn-NO", "00").Returns(false),
                new TestCaseData("19676338855", "nn-NO", "00").Returns(false),
                new TestCaseData("+4726338855", "nn-NO", "00").Returns(false),
                new TestCaseData("4736338855", "nn-NO", "00").Returns(false),
                new TestCaseData("66338855", "nn-NO", "00").Returns(false),

                new TestCaseData("061234567", "nl-NL", "00").Returns(true),
                new TestCaseData("+3161234567", "nl-NL", "00").Returns(true),
                new TestCaseData("003161234567", "nl-NL", "00").Returns(true),
                new TestCaseData("021234567", "nl-NL", "00").Returns(false),

                new TestCaseData("44770 0956-823", "en-GB", "00").Returns(true),
                new TestCaseData("+4477009 56823", "en-GB", "00").Returns(true),
                new TestCaseData("0770 095 6823", "en-GB", "00").Returns(true),
                new TestCaseData("00447 700-956823", "en-GB", "00").Returns(true),
                new TestCaseData("07700 95 682", "en-GB", "00").Returns(false),
                new TestCaseData("077009f 6823", "en-GB", "00").Returns(false),
                new TestCaseData("01524 56999", "en-GB", "00").Returns(false),
                new TestCaseData("44207 3456754", "en-GB", "00").Returns(false),
                new TestCaseData("+4430 03 434751", "en-GB", "00").Returns(false),
                new TestCaseData("050734567 54", "en-GB", "00").Returns(false),
                new TestCaseData("080011  23123", "en-GB", "00").Returns(false),
        };


        [Test]
        [TestCaseSource("TestCases")]
        public bool IsMobilePhoneTestPassingLocale(string number, string locale, string exitCode)
        {
            return number.IsValid(locale).MobilePhone(exitCode);
        }

        [Test]
        [TestCaseSource("TestCases")]
        public bool IsMobilePhoneTestPassingLocaleOnThread(string number, string locale, string exitCode)
        {
            var tst = new tmp
            {
                phone = number,
                locale = locale,
                exitCode = exitCode
            };
            var t = new Thread(new ParameterizedThreadStart(ThreadExecute));
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
                t.result = t.phone.IsValid().MobilePhone(t.exitCode);
            }
            catch (CultureNotFoundException)
            {
                //this is so the test cases still pass even when we are dealing with a Culture that .net/windows doesn't understand
                t.result = t.phone.IsValid(t.locale).MobilePhone(t.exitCode);
            }
        }

        public class tmp
        {
            public string phone;
            public string locale;
            public bool result;
            public string exitCode;
        }
    }
}
