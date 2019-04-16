using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ManualModel
    {
        public delegate void valueChanged( string msg);
        public event valueChanged valueChange;


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

        public void SendValueMessage( string msg)
        {
            valueChange?.Invoke(msg);
        }


    }
}
