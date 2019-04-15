using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Socket
{
    public class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (client.Connected)
                    {
                        string commandLine = reader.ReadLine();
                        //Console.WriteLine("Got command: {0}", commandLine);
                        //split by generic and send lat and lon to flight board
                    }
                }
                
                //client.Close();
            }).Start();
        }
        
    }
}
