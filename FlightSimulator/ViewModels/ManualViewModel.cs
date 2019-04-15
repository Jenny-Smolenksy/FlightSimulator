using FlightSimulator.Model.EventArgs;
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

        
        
        private ICommand _cancelCommand; //change name
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new Model.CommandHandler(() => OnChangedJoystick()));
            }
        }
        private void OnChangedJoystick()
        {
           // model.ReloadSettings();
        }
    }
}


