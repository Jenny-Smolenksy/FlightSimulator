using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Socket
{
    public class TcpClientSimulator
    {
        private TcpClient client;

        public TcpClientSimulator(string serverIp, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIp), port);
            client = new TcpClient();
            try
            {
                client.Connect(ep);
            } catch(SocketException ex)
            {

            }
        }

        public void SendMessage(string message)
        {
            if (client == null) return;
            using (NetworkStream stream = client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(message);
            }
        }

        public void CloseClient()
        {
            client.Close();
        }
        
    }

    
}
