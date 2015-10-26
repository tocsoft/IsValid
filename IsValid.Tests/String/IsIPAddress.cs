using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using IsValid;
using System.Threading;
using System.Globalization;
using System.Net.Sockets;

namespace IsValid.Tests.String
{
    [TestFixture]
    public class IsIPAddress
    {
        public IEnumerable<string> IPv4
        {
            get
            {
                yield return "127.0.0.1";
                yield return "0.0.0.0";
                yield return "255.255.255.255";
            }
        }

        public IEnumerable<string> NotIPv4
        {
            get
            {
                yield return "256.0.0.0";
                yield return "26.0.0.256";
                yield return "abc";
                yield return "::1";
            }
        }
        public IEnumerable<string> IPv6
        {
            get
            {
                yield return "::1";
                yield return "2001:db8:0000:1:1:1:1:1";
            }
        }

        public IEnumerable<string> NotIPv6
        {
            get
            {
                yield return "127.0.0.1";
                yield return "0.0.0.0";
            }
        }

        public IEnumerable<string> AnyIP
        {
            get
            {
                return IPv6.Union(IPv4);
            }
        }
        public IEnumerable<string> NotAnyIP
        {
            get
            {
                var allNots = NotIPv6.Union(NotIPv4);
                return allNots.Where(x => !AnyIP.Contains(x));
            }
        }



        [Test]
        [TestCaseSource("IPv4")]
        public void IsIPv4(string value)
        {
            Assert.IsTrue(value.IsValid().IPAddressV4());
        }

        [Test]
        [TestCaseSource("IPv4")]
        public void IsIPv4ViaFamily(string value)
        {
            Assert.IsTrue(value.IsValid().IPAddress(AddressFamily.InterNetwork));
        }

        [Test]
        [TestCaseSource("NotIPv4")]
        [TestCaseSource("IPv6")]
        public void IsNotIPv4(string value)
        {
            Assert.IsFalse(value.IsValid().IPAddressV4());
        }

        [Test]
        [TestCaseSource("NotIPv4")]
        [TestCaseSource("IPv6")]
        public void IsNotIPv4ViaFamily(string value)
        {
            Assert.IsFalse(value.IsValid().IPAddress(AddressFamily.InterNetwork));
        }

        [Test]
        [TestCaseSource("IPv6")]
        public void IsIPv6(string value)
        {
            Assert.IsTrue(value.IsValid().IPAddressV6());
        }
        [Test]
        [TestCaseSource("IPv6")]
        public void IsIPv6ViaFamily(string value)
        {
            Assert.IsTrue(value.IsValid().IPAddress(AddressFamily.InterNetworkV6));
        }

        [Test]
        [TestCaseSource("NotIPv6")]
        [TestCaseSource("IPv4")]
        public void IsNotIPv6(string value)
        {
            Assert.IsFalse(value.IsValid().IPAddressV6());
        }

        [Test]
        [TestCaseSource("NotIPv6")]
        [TestCaseSource("IPv4")]
        public void IsNotIPv6ViaFamily(string value)
        {
            Assert.IsFalse(value.IsValid().IPAddress(AddressFamily.InterNetworkV6));
        }

        [Test]
        [TestCaseSource("AnyIP")]
        public void IsAnyIP(string value)
        {
            Assert.IsTrue(value.IsValid().IPAddress());
        }

        [Test]
        [TestCaseSource("NotAnyIP")]
        public void IsNotAnyIP(string value)
        {
            Assert.IsFalse(value.IsValid().IPAddress());
        }

        [Test]
        public void ReturnsFalseIfNoFamiliesSpecifiedEmpty()
        {
            Assert.IsFalse("127.0.0.1".IsValid().IPAddress(new System.Net.Sockets.AddressFamily[0]));
        }
        [Test]
        public void ReturnsFalseIfNoFamiliesSpecifiedNull()
        {
            Assert.IsFalse("127.0.0.1".IsValid().IPAddress((System.Net.Sockets.AddressFamily[])null));
        }
    }
}
