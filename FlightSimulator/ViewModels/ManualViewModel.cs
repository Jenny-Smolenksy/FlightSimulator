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

        private Model.ManualModel model;
        public ManualViewModel()
        {
            model = Model.ManualModel.Instance;

        }

        public double Throttle
        {
          //  get { }
            set
            {
                model.throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }

        public double Rudder
        {
           // get { return model.FlightCommandPort; }
            set
            {
                model.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

      

        public void Joystick_Moved(Joystick sender, Model.EventArgs.VirtualJoystickEventArgs args)
        {
            model.aileron = args.Aileron;
            model.elevator = args.Elevator;
        }
    }
}


