using System;
using System.Collections.Generic;
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
            client.Connect(ep);

            /**
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.Write("Please enter a number: ");
                int num = int.Parse(Console.ReadLine());
                writer.Write(num);
                // Get result from server
                int result = reader.ReadInt32();
                Console.WriteLine("Result = {0}", result);
            }
          **/
        }

        ~TcpClientSimulator()
        {
            client.Close();
        }
        
    }

    
}
