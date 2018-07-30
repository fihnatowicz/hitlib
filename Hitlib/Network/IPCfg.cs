using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Hitlib.Network
{
    public class IPCfg
    {
        public static void PrintIPS(string host)
        {
            foreach(var ip in Dns.GetHostAddresses(host))
            {
                Console.WriteLine(ip.ToString());
            }
        }

        public static void Ping(string host)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(host, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Connected to {0} | rtt: {1}, ttl: {2}", reply.Address.ToString(), reply.RoundtripTime, reply.Options.Ttl);
            }
        }
    }
}
