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
        public delegate void OnClientEvent(string message);
        public event OnClientEvent onClientEvent;
        
        public TcpClientSimulator()
        {

        }

        public void Connect(string serverIp, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIp), port);
            client = new TcpClient();
            try
            {
                client.Connect(ep);
                onClientEvent?.Invoke("Conncted to simulator, commands channel");
            } catch(SocketException)
            {
                onClientEvent?.Invoke("Error Conncted to simulator in commands channel, check port");
            }
        }

        public bool SendMessage(string message)
        {
            if (client == null || !client.Connected)
            {
                onClientEvent?.Invoke("socket closed, fail to send command");
                return false;
            }
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream))
                {

                    writer.Write(message);
                    onClientEvent?.Invoke("Command succesfully sent to simulator");
                    return true;
                }
            } catch {

                onClientEvent?.Invoke("socket closed, fail to send command");
                return false;
            }
        }

        public void CloseClient()
        {
            if(IsConnected())
              client.Close();
        }

        public bool IsConnected()
        {
            if (client == null)
                return false;
            return client.Connected;
        }



    }

    
}
