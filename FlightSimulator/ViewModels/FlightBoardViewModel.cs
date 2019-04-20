using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel model;
        /// <summary>
        /// constructor
        /// </summary>
        public FlightBoardViewModel()
        {
            //connect to model layer
            model = FlightBoardModel.Instance;
            //sign to event
            model.changedParamsEvent += ChangedParams;
        }
        /// <summary>
        /// handle params changed
        /// </summary>
        private void ChangedParams()
        {
            //notify to user layer
            NotifyPropertyChanged("Lon");
            NotifyPropertyChanged("Lat");
        }

        #region params
        public double Lon
        {
            get {
                return model.Lon;
            }
        }

        public double Lat
        {
            get { return model.Lat; }
        }
        #endregion
    }
}
