using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Socket
{
    public class TcpServer
    {
        private TcpListener listener;
        private List<SimulatorClientHandler> clientsList;
        public delegate void ParamsChanged(double lon, double lat);
        public delegate void ServerEvent();
        public event ServerEvent clientConnected;
        public event ServerEvent clientDisconnected;
        public event ServerEvent failedToOpen;
        public event ParamsChanged onClientHandlerParamsChanged;

        public TcpServer()
        {
        }

        public void Connect(int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            //opening server
            try
            {
                listener.Start();
                StartClientsListening();
            } catch (SocketException)
            {
                failedToOpen?.Invoke();
            }
            clientsList = new List<SimulatorClientHandler>();
        }
        

        private void StartClientsListening()
        {
            Thread thread = new Thread(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        clientConnected?.Invoke();

                        SimulatorClientHandler clientHandler = new SimulatorClientHandler();
                        clientHandler._onParamsChanged += ClientHandlerParamsChanged;
                        clientHandler.clientDisconnected += this.clientDisconnected;
                        clientsList.Add(clientHandler);
                        clientHandler.HandleClient(client);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                //Console.WriteLine("Server stopped");
            });
            thread.Start();
        }

        private void ClientHandlerParamsChanged(double lon, double lat)
        {
            onClientHandlerParamsChanged?.Invoke(lon, lat);
        }

        public bool IsConnected()
        {
            if (listener == null)
                return false;
            return listener.Server.Connected;
        }

        public void StopListening()
        {
            if (IsConnected())
              listener.Stop();
        }

        public int GetPort()
        {
            if (!IsConnected()) return -1;
            string endPoint = listener.Server.LocalEndPoint.ToString();
            string port = endPoint.Split(':')[1];
            return Convert.ToInt32(port);
        }
    }
}
