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
        public static bool IPAddressV4(this IValidatableValue<string> input)
        {
            return input.IPAddress(System.Net.Sockets.AddressFamily.InterNetwork);
        }

        /// <summary>
        /// Determine whether input is a valid IPv6 address 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddressV6(this IValidatableValue<string> input)
        {
            return input.IPAddress(System.Net.Sockets.AddressFamily.InterNetworkV6);
        }

        /// <summary>
        /// Determine whether input is a valid IP address of the address families
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddress(this IValidatableValue<string> input, params System.Net.Sockets.AddressFamily[] family)
        {

            if (family == null || !family.Any())
            {
                return false;
            }

            IPAddress address;
            if (System.Net.IPAddress.TryParse(input.Value, out address))
            {
                return family.Contains(address.AddressFamily);
            }

            return false;
        }

        /// <summary>
        /// Determine whether input is a valid IP address
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddress(this IValidatableValue<string> input)
        {
            return input.IPAddress(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.AddressFamily.InterNetworkV6);
        }
    }
}
