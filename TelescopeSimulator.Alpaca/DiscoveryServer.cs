using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TelescopeSimulator.Alpaca
{
    public class DiscoveryServer
    {
        private readonly int port;
        private const int DiscoveryPort = 32227;
        public const string DiscoveryMessage = "alpacadiscovery1";

        public DiscoveryServer(int AlpacaPort)
        {
            port = AlpacaPort;
            UdpClient UDPClient = new UdpClient();

            UDPClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            UDPClient.EnableBroadcast = true;
            UDPClient.MulticastLoopback = false;
            UDPClient.ExclusiveAddressUse = false;

            UDPClient.Client.Bind(new IPEndPoint(IPAddress.Any, DiscoveryPort));

            // This uses begin receive rather then async so it works on net 3.5
            UDPClient.BeginReceive(ReceiveCallback, UDPClient);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient udpClient = (UdpClient)ar.AsyncState;

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, DiscoveryPort);

            // Obtain the UDP message body and convert it to a string, with remote IP address attached as well
            string ReceiveString = Encoding.ASCII.GetString(udpClient.EndReceive(ar, ref endpoint));

            if (ReceiveString.Contains(DiscoveryMessage))//Contains rather then equals because of invisible padding garbage
            {
                //For testing only
                Console.WriteLine(string.Format("Received a discovery packet from {0} at {1}", endpoint.Address, DateTime.Now));

                byte[] response = Encoding.ASCII.GetBytes(string.Format("{{\"alpacaport\": {0}}}", port));

                udpClient.Send(response, response.Length, endpoint);
            }

            // Configure the UdpClient class to accept more messages, if they arrive
            udpClient.BeginReceive(ReceiveCallback, udpClient);
        }
    }
}
