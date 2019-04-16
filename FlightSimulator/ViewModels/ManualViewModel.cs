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
       
            set
            {
                model.SendValueMessage(buildMsg("controls/engines/current-engine/throttle", value));
            }
        }

        public double Rudder
        {
          
            set
            {
                model.SendValueMessage(buildMsg("controls/flight/rudder", value));
            }
        }


      

        public void Joystick_Moved(Joystick sender, Model.EventArgs.VirtualJoystickEventArgs args)
        {
            model.SendValueMessage(buildMsg("controls/flight/aileron", args.Aileron));
           
            model.SendValueMessage(buildMsg("controls/flight/elevator", args.Elevator));
        }

        private string buildMsg(string arg, double val)
        {
            string msg = "set ";
            return  msg +arg +" "+ val;
           

        }
    }
}


