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
        }
        private double _lon;
        public double Lon
        {
            get { return model.Lon; }
            set
            {
                model.Lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        public double Lat
        {
            get { return model.Lat; }
            set
            {
                model.Lat = value;
                NotifyPropertyChanged("Lat");
            }
        }
    }
}
