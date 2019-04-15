using FlightSimulator.Model.EventArgs;
using FlightSimulator.Views;
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
    
        public ManualViewModel()
        {
           
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

        private void Joystick_Moved(Joystick sender, Model.EventArgs.VirtualJoystickEventArgs args)
        {
            
        }
    }
}


