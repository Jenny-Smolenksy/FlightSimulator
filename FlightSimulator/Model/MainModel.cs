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
        #region params
        private TcpClientSimulator tcpClient;
        private TcpServer tcpServer;
        private AutoPilotModel autoPilot;
        private ManualModel manual;
        #endregion

        #region events
        public delegate void UserMessageChanged(string message);
        public event UserMessageChanged onUserMessage;

        public delegate void onSettingCloseRequestDel();
        public event onSettingCloseRequestDel onSettingCloseRequest;
        #endregion

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

        #region constructor
        private MainModel()
        {
            //create tcp server
            tcpServer = new TcpServer();
            //sign to events
            tcpServer.clientConnected += OnSimulatorConnected;
            tcpServer.clientDisconnected += OnSimulatorDisconnected;
            tcpServer.failedToOpen += OnServerFailedToOpen;
            tcpServer.onClientHandlerParamsChanged +=
                FlightBoardModel.Instance.ParamsChanged;

            //create client
            tcpClient = new TcpClientSimulator();
            tcpClient.onClientEvent += OnClientEventHandler;

            //sign to events
            autoPilot = AutoPilotModel.Instance;
            autoPilot.onMessageRequest += OnManualSend;

            manual = ManualModel.Instance;
            manual.valueChange += OnManualSend;

        }
        #endregion

        #region Methods
        /// <summary>
        /// conncet to simulator
        /// </summary>
        public void Connect()
        {
            //get connection port
            int port = Properties.Settings.Default.FlightInfoPort;

            //open info channel 

            //check if server is opened
            if (tcpServer.IsConnected())
            {
                //if connected on requsted port
                if (tcpServer.GetPort() == port)
                {
                    onUserMessage?.Invoke("Already conected at requested port to info channel");
                } 
                else
                {
                    //case connected, but asked to be conncted to different port
                    //stop
                    tcpServer.StopListening();
                    //reconncet on new port
                    tcpServer.Connect(port);
                }
            }
            else
            {
                tcpServer.Connect(port);
            }

            //open command channel
            //check if connected
            if (tcpClient.IsConnected())
            {
                tcpClient.CloseClient();
            }
            //connect client
            tcpClient.Connect(Properties.Settings.Default.FlightServerIP,
                Properties.Settings.Default.FlightCommandPort);
        }
        /// <summary>
        /// disconnected from both channels
        /// </summary>
        public void DisConnect()
        {
            //close server
            if (tcpServer != null)
            {
                tcpServer.StopListening();
            }
            //close client
            if (tcpClient != null)
            {
                tcpClient.CloseClient();
            }
        }
        #endregion

        #region Events Handlers methods
        /// <summary>
        /// handle server failed opening event
        /// </summary>
        private void OnServerFailedToOpen()
        {
            //notify to view 
            onUserMessage?.Invoke("failed to open info channel");
        }
        /// <summary>
        /// handle simulator disconnected
        /// </summary>
        private void OnSimulatorDisconnected()
        {
            //notify to view
            onUserMessage?.Invoke("simulator disconnected");
        }
        /// <summary>
        /// handle simulator info channel conncted event
        /// </summary>
        private void OnSimulatorConnected()
        { 
            //open command channel
            if (tcpClient.IsConnected())
            {
                tcpClient.CloseClient();
            }
            tcpClient.Connect(Properties.Settings.Default.FlightServerIP,
                Properties.Settings.Default.FlightCommandPort);
            //notify gui
            onUserMessage?.Invoke("Connected to info channel of simulator");            
        }

        /// <summary>
        /// handle tcp client any event
        /// </summary>
        /// <param name="message"></param>
        private void OnClientEventHandler(string message)
        {
            //notify gui
            onUserMessage?.Invoke(message);
        }
        /// <summary>
        /// handle manual send event
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool OnManualSend(string message)
        {
            if(tcpClient == null)
            {
                return false;
            }
            //send message via tcp client, return is successed ot not
            return tcpClient.SendMessage(message);
        }
        /// <summary>
        /// handle request to close settings window
        /// </summary>
        public void RequestToCloseSettings()
        {
            //notify gui
            onSettingCloseRequest?.Invoke();
        }
        #endregion        
    }
}
