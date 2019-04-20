using FlightSimulator.Model.Socket;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class FlightBoardModel 
    {
        public delegate void ChangedParamsHandler();
        public event ChangedParamsHandler changedParamsEvent;
        #region Singleton
        private static FlightBoardModel m_Instance = null;
        public static FlightBoardModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new FlightBoardModel();
                }
                return m_Instance;
            }
        }
        #endregion

        #region Properties
        private double _lon;
        public double Lon
        {
            get { return _lon; }
                set
            {
                _lon = value;
            }
        }
        private double _lat;
        public double Lat
        {
            get; set;
        }
        #endregion

        /// <summary>
        /// params change handle
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        public void ParamsChanged(double lon, double lat)
        {
            this.Lat = lat;
            this.Lon = lon;
            changedParamsEvent?.Invoke();
        }
    }
}