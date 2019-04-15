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
        public delegate void ClientConnected();
        public event ClientConnected clientConnected;
        public delegate void ClientDisconnected();
        public event ClientDisconnected clientDisconnected;

        public TcpServer(int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            //opening server
            try
            {
                listener.Start();
            } catch (SocketException)
            {
            }
            clientsList = new List<SimulatorClientHandler>();
        }

        public void StartClientsListening(SimulatorClientHandler.ParamsChanged paramsChangedDel)
        {
            Thread thread = new Thread(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        clientConnected?.Invoke();

                        SimulatorClientHandler clientHandler = new SimulatorClientHandler();
                        clientHandler._onParamsChanged += paramsChangedDel;
                        clientHandler.clientDisconnected += this.clientDisconnected;
                        clientsList.Add(clientHandler);
                        clientHandler.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                //Console.WriteLine("Server stopped");
            });
            thread.Start();
        }

        

        public void StopListening()
        {
            listener.Stop();
        }
    }
}
