using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PcShutdownClient
{
    class Program
    {
        private const int SERVER_PORT = 161;
        private const string SERVER_IP = "224.0.0.0";

        static void Main(string[] args)
        {
            UdpClient udpRecever = new UdpClient(SERVER_PORT);
            udpRecever.JoinMulticastGroup(IPAddress.Parse(SERVER_IP),50);
            IPEndPoint endPoint = null;

            while (true)
            {
                try
                {
                    byte[] data = udpRecever.Receive(ref endPoint);

                    if (Encoding.Default.GetString(data) == "t")
                    {
                        System.Diagnostics.Process.Start("cmd", "/c shutdown -s -f -t 00");
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
