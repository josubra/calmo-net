using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Calmo.Net
{
	/// <summary>
	/// Network utilities
	/// </summary>
    public class NetworkHelper
    {
		/// <summary>
		/// Get the machine MAC Address
		/// </summary>
		/// <returns>MAC Address from the first found network interface</returns>
        public static string GetMACAddress()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            var macAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (macAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    macAddress = adapter.GetPhysicalAddress().ToString();
                }
            } 
            
            return macAddress;
        }
    }
}
