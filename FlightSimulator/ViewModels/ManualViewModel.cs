using FlightSimulator.Model.EventArgs;
using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
      public class ManualViewModel:BaseNotify
    {
        private ManualModel model;
    
        public ManualViewModel()
        {
            model = ManualModel.Instance;  
        }

        public double Throttle
        {
          //  get { }
            set
            {
               
                NotifyPropertyChanged("Throttle");
            }
        }

        public double Rudder
        {
           // get { return model.FlightCommandPort; }
            set
            {
               // model.FlightCommandPort = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        public double Aieleron
        {
            // get { return model.FlightCommandPort; }
            set
            {
                // model.FlightCommandPort = value;
                NotifyPropertyChanged("Aieleron");
            }
        }

        
        private void OnChangedJoystick()
        {
           // model.ReloadSettings();
        }
    }
}


