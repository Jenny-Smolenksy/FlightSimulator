using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ManualModel
    {
        private double rudder;
        private double throttle;
        private double aileron;
        private double elevator;

        #region Singleton
        private static ManualModel m_Instance = null;
        public static ManualModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ManualModel();
                }
                return m_Instance;
            }
        }
        #endregion
    }
}
