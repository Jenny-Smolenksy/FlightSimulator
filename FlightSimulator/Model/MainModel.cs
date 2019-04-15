using FlightSimulator.Model.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class MainModel
    {
        #region Singleton
        private static MainModel m_Instance = null;
        public static MainModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new MainModel();
                }
                return m_Instance;
            }
        }
        #endregion


        public void StartServer()
        {
            TcpServer server = new TcpServer(7777);

            server.StartClientsListening(FlightBoardModel.Instance.ParamsChanged);
            
        }
    }
}
