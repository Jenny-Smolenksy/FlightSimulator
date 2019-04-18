using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static FlightSimulator.Model.Socket.TcpServer;

namespace FlightSimulator.Model.Socket
{
    public class SimulatorClientHandler : IClientHandler
    {
        public event ParamsChanged _onParamsChanged;
        public event ServerEvent clientDisconnected;

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                try
                {
                    using (NetworkStream stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string inputLine;
                        while (client.Connected)
                        {
                            inputLine = reader.ReadLine();
                            string[] values = inputLine.Split(',');
                            double lon = Convert.ToDouble(values[0]);
                            double lat = Convert.ToDouble(values[1]);

                            _onParamsChanged?.Invoke(lon, lat);
                            //Console.WriteLine("Got command: {0}", commandLine);
                            //split by generic and send lat and lon to flight board
                        }
                        clientDisconnected?.Invoke();

                    }
                } catch(Exception) { clientDisconnected?.Invoke(); }
                
                //client.Close();
            }).Start();
        }
        
    }
}
