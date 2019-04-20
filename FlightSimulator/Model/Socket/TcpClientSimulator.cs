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

        /// <summary>
        /// connect to server
        /// </summary>
        /// <param name="serverIp">ip address to connect to</param>
        /// <param name="port">port to connect to</param>
        public void Connect(string serverIp, int port)
        {
            //set ip
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIp), port);
            //create new tcp client
            client = new TcpClient();
            try
            {
                //connect
                client.Connect(ep);
                //raise success event
                onClientEvent?.Invoke("Conncted to simulator, commands channel");
            } catch(SocketException)
            {
                //onClientEvent?.Invoke("Error Conncted to simulator in commands channel, check port");
            }
           
        }
        /// <summary>
        /// send message
        /// </summary>
        /// <param name="message">to send</param>
        /// <returns></returns>
        public bool SendMessage(string message)
        {
            //check if client if connected
            if (client == null || client.Connected == false)
            {
                //notify
                onClientEvent?.Invoke("socket closed, fail to send command");
                return false;
            }
            try
            {
                //get stream and bytes of message
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message + "\r\n");
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
               // stream.Flush();
                
                //notify
                onClientEvent?.Invoke("Command succesfully sent to simulator");
                return true;
                
            } catch
            {
                //notify error
                onClientEvent?.Invoke("socket closed, fail to send command");
                return false;
            }
        }
        /// <summary>
        /// close command
        /// </summary>
        public void CloseClient()
        {
            if(IsConnected())
              client.Close();
        }
        /// <summary>
        /// check if is connected
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            if (client == null)
                return false;
            return client.Connected;
        }
    }    
}
