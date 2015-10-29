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
            return input.IPAddress(System.Net.Sockets.AddressFamily.InterNetwork);
        }

        /// <summary>
        /// Determine whether input is a valid IPv6 address 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddressV6(this ValidatableValue<string> input)
        {
            return input.IPAddress(System.Net.Sockets.AddressFamily.InterNetworkV6);
        }

        /// <summary>
        /// Determine whether input is a valid IP address of the address families
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddress(this ValidatableValue<string> input, params System.Net.Sockets.AddressFamily[] family)
        {

            if (family == null || !family.Any())
            {
                input.AddError("No AddressFamily specified");
                return false;
            }

            IPAddress address;
            if (System.Net.IPAddress.TryParse(input.Value, out address))
            {
                if (family.Contains(address.AddressFamily))
                {
                    return true;
                }

                input.AddError("AddressFamily doesn't match");
            }
            else
            {
                input.AddError("Failed to parse IP address");
            }

            return false;
        }

        /// <summary>
        /// Determine whether input is a valid IP address
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPAddress(this ValidatableValue<string> input)
        {
            return input.IPAddress(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.AddressFamily.InterNetworkV6);
        }
    }
}
