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

        public FlightBoardViewModel()
        {
            model = FlightBoardModel.Instance;
            model.changedParamsEvent += ChangedParams;
        }

        private void ChangedParams()
        {
            NotifyPropertyChanged("Lon");
            NotifyPropertyChanged("Lat");
        }

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
    }
}
