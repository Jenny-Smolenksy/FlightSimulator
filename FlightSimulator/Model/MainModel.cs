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
        private TcpClientSimulator tcpClient;
        private TcpServer tcpServer;
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

        private MainModel()
        {
            tcpServer = null;
            tcpClient = null;
        }

        public void Connect()
        {
            //open info channel
            if (tcpServer != null)
            {
                tcpServer.StopListening();
            }
            tcpServer = new TcpServer(Properties.Settings.Default.FlightInfoPort);
            tcpServer.StartClientsListening(FlightBoardModel.Instance.ParamsChanged);

            //open command channel

            if(tcpClient != null)
            {
                tcpClient.CloseClient();
            }
            tcpClient = new TcpClientSimulator(Properties.Settings.Default.FlightServerIP,
                Properties.Settings.Default.FlightCommandPort);
            
        }

        public void DisConnect()
        {
            if (tcpServer != null)
            {
                tcpServer.StopListening();
            }
            if (tcpClient != null)
            {
                tcpClient.CloseClient();
            }
        }
    }
}
