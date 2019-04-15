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
    public class SimulatorClientHandler : IClientHandler
    {
        public delegate void ParamsChanged(double lon, double lat);
        public event ParamsChanged _onParamsChanged;

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string inputLine;
                    while (client.Connected)
                    {
                        //		commandLine	"0.000000,394.065674,1.193444,12.000000,40.000000,0.000000,0.000000,0.000000,0.000000,0.000000,0.000000,0.000000,260.564117,0.000000,0.000000,0.000000,0.000000,0.000000,0.000000,0.000000,0.000000,0.000000,0.000000"	string

                        inputLine = reader.ReadLine();
                        string[] values = inputLine.Split(',');
                        double lon = Convert.ToDouble(values[0]);
                        double lat = Convert.ToDouble(values[1]);

                        _onParamsChanged?.Invoke(lon, lat);
                        //Console.WriteLine("Got command: {0}", commandLine);
                        //split by generic and send lat and lon to flight board
                    }
                }
                
                //client.Close();
            }).Start();
        }
        
    }
}
