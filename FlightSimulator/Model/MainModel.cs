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
        private AutoPilotModel autoPilot;
        private ManualModel manual;

        public delegate void UserMessageChanged(string message);
        public event UserMessageChanged onUserMessage;

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
            autoPilot = AutoPilotModel.Instance;
            manual = ManualModel.Instance;
            autoPilot.onMessageRequest += OnManualSend;
            manual.valueChange += OnManualSend;

        }


        public void Connect()
        {
            //open info channel
            if (tcpServer != null)
            {
                tcpServer.StopListening();
                tcpServer.clientConnected -= OnSimulatorConnected;
            }
            tcpServer = new TcpServer(Properties.Settings.Default.FlightInfoPort);
            tcpServer.clientConnected += OnSimulatorConnected;
            tcpServer.clientDisconnected += OnSimulatorDisconnected;
            tcpServer.StartClientsListening(FlightBoardModel.Instance.ParamsChanged);

            //open command channel

            if(tcpClient != null)
            {
                tcpClient.CloseClient();
            }
            tcpClient = new TcpClientSimulator(Properties.Settings.Default.FlightServerIP,
                Properties.Settings.Default.FlightCommandPort);
            
        }

        private void OnSimulatorDisconnected()
        {

            onUserMessage?.Invoke("simulator disconnected");
        }

        private void OnSimulatorConnected()
        {
            onUserMessage?.Invoke("Connected to info channel of simulator");
        }


        private void OnManualSend(string message)
        {
            if(tcpClient == null)
            {
                return;
            }

            tcpClient.SendMessage(message);
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
