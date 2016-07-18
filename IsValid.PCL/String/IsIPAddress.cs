using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IsValid
{
    public static class IsIPAddress
    {
        /// <summary>
        /// Determine whether input is a valid IPv4 address 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddressV4(this ValidatableValue<string> input)
        {
            throw new NotSupportedException("IPAddress validation not supported this platform") ;
        }

        /// <summary>
        /// Determine whether input is a valid IPv6 address 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddressV6(this ValidatableValue<string> input)
        {
            throw new NotSupportedException("IPAddress validation not supported this platform");
        }

        /// <summary>
        /// Determine whether input is a valid IP address of the address families
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddress(this ValidatableValue<string> input, params object[] family)
        {
            throw new NotSupportedException("IPAddress validation not supported this platform");
        }

        /// <summary>
        /// Determine whether input is a valid IP address
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddress(this ValidatableValue<string> input)
        {
            throw new NotSupportedException("IPAddress validation not supported this platform");
        }
    }
}
