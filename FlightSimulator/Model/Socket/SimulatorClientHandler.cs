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
        #region events
        public event ParamsChanged _onParamsChanged;
        public event ServerEvent clientDisconnected;
        #endregion
        #region Implementations
        /// <summary>
        /// handle connecting client
        /// </summary>
        /// <param name="client"></param>
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
                        //check if client is connected
                        while (client.Connected)
                        {
                            //read a line from client
                            inputLine = reader.ReadLine();
                            string[] values = inputLine.Split(',');
                            //get the params needed
                            double lon = Convert.ToDouble(values[0]);
                            double lat = Convert.ToDouble(values[1]);
                            //raise event
                            _onParamsChanged?.Invoke(lon, lat);
                        }
                        //raise event
                        clientDisconnected?.Invoke();
                    }
                } catch(Exception)
                {
                    //raise event for error
                    clientDisconnected?.Invoke(); }
                
                //client.Close();
            }).Start();
        }
        #endregion
    }
}
