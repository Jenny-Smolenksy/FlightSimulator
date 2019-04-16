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
           // get { return model.throttle; }
            set
            {
                NotifyPropertyChanged("Throttle");
                model.SendValueMessage(buildMsg("throttle", value));
            }
        }

        public double Rudder
        {
           // get { return model.FlightCommandPort; }
            set
            {
                NotifyPropertyChanged("Rudder");
                model.SendValueMessage(buildMsg("rudder", value));
            }
        }


      

        public void Joystick_Moved(Joystick sender, Model.EventArgs.VirtualJoystickEventArgs args)
        {
            model.SendValueMessage(buildMsg("aileron",args.Aileron));
           
            model.SendValueMessage(buildMsg("elevator",args.Elevator));
        }

        private string buildMsg(string arg, double val)
        {
            string msg = "set controls/flight/";
            return msg + arg + val;
           

        }
    }
}


