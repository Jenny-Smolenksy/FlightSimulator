using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class AutoPilotModel
    {
        public delegate void OnMessageRequest(string message);
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

        public void SendMessage(string message)
        {
            onMessageRequest?.Invoke(message);
        }
    }
}
