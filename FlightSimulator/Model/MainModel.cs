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

        public delegate void onSettingCloseRequestDel();
        public event onSettingCloseRequestDel onSettingCloseRequest;

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

            tcpServer = new TcpServer();
            tcpServer.clientConnected += OnSimulatorConnected;
            tcpServer.clientDisconnected += OnSimulatorDisconnected;
            tcpServer.failedToOpen += OnServerFailedToOpen;
            tcpServer.onClientHandlerParamsChanged += FlightBoardModel.Instance.ParamsChanged;

            tcpClient = new TcpClientSimulator();
            tcpClient.onClientEvent += OnClientEventHandler;

            autoPilot = AutoPilotModel.Instance;
            autoPilot.onMessageRequest += OnManualSend;

            manual = ManualModel.Instance;
            manual.valueChange += OnManualSend;

        }


        public void Connect()
        {
            int port = Properties.Settings.Default.FlightInfoPort;

            //open info channel
            if (tcpServer.IsConnected())
            {
                if (tcpServer.GetPort() == port)
                {
                    onUserMessage?.Invoke("Already conected at requested port to info channel");
                } 
                else
                {
                    tcpServer.StopListening();
                    tcpServer.Connect(port);
                }
            }
            else
            {
                tcpServer.Connect(port);
            }

            //open command channel

            if (tcpClient.IsConnected())
            {
                tcpClient.CloseClient();
            }
            tcpClient.Connect(Properties.Settings.Default.FlightServerIP,
                Properties.Settings.Default.FlightCommandPort);
        }

        private void OnServerFailedToOpen()
        {
            onUserMessage?.Invoke("failed to open info channel");
        }

        private void OnSimulatorDisconnected()
        {
            onUserMessage?.Invoke("simulator disconnected");
        }

        private void OnSimulatorConnected()
        { 
            //open command channel

            if (tcpClient.IsConnected())
            {
                tcpClient.CloseClient();
            }
            tcpClient.Connect(Properties.Settings.Default.FlightServerIP,
                Properties.Settings.Default.FlightCommandPort);

            onUserMessage?.Invoke("Connected to info channel of simulator");

            //open command channel

            if (tcpClient.IsConnected())
            {
                tcpClient.CloseClient();
            }
            tcpClient.Connect(Properties.Settings.Default.FlightServerIP,
                Properties.Settings.Default.FlightCommandPort);

        }


        private void OnClientEventHandler(string message)
        {
            onUserMessage?.Invoke(message);
        }

        private bool OnManualSend(string message)
        {
            if(tcpClient == null)
            {
                return false;
            }

            return tcpClient.SendMessage(message);
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

        public void RequestToCloseSettings()
        {
            onSettingCloseRequest?.Invoke();
        }
    }
}
