using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class AutoPilotModel
    {
        public delegate bool OnMessageRequest(string message);
        public event OnMessageRequest onMessageRequest;

        #region Singleton
        private static AutoPilotModel m_Instance = null;
        public static AutoPilotModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new AutoPilotModel();
                }
                return m_Instance;
            }
        }
        #endregion
        /// <summary>
        /// send message to server
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendMessage(string message)
        {
            if (onMessageRequest == null)
                return false;
            return onMessageRequest(message);
        }
    }
}
