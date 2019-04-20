using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface IClientHandler
    {
        /// <summary>
        /// handle incomming client
        /// </summary>
        /// <param name="client"></param>
        void HandleClient(TcpClient client);
    }
}
