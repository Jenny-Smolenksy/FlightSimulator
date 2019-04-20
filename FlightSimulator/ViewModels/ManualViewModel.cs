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
        /// <summary>
        /// constructor to class
        /// </summary>
        public ManualViewModel()
        {
            //set connceting to model layer
            model = Model.ManualModel.Instance;
        }
        /// <summary>
        /// throttle property
        /// </summary>
        public double Throttle
        {       
            set
            {
                model.SendValueMessage(buildMsg("controls/engines/current-engine/throttle", value));
            }
        }
        /// <summary>
        /// ruddet property
        /// </summary>
        public double Rudder
        {          
            set
            {
                model.SendValueMessage(buildMsg("controls/flight/rudder", value));
            }
        }
        /// <summary>
        /// function dealt with movement in joystick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Joystick_Moved(Joystick sender, Model.EventArgs.VirtualJoystickEventArgs args)
        {
            model.SendValueMessage(buildMsg("controls/flight/aileron", args.Aileron));
           
            model.SendValueMessage(buildMsg("controls/flight/elevator", args.Elevator));
        }
        /// <summary>
        /// building message to send to simulator
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private string buildMsg(string arg, double val)
        {
            string msg = "set ";
            return  msg +arg +" "+ val;
        }
    }
}


