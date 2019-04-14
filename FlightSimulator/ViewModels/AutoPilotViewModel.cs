using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {        

        public AutoPilotViewModel()
        {
            IsSent = false;
        }

        public bool IsSent
        {
            get;
            private set;
        }

        public string Text
        {
            set
            {
                //NotifyPropertyChanged("FlightServerIP");
                IsSent = false;
            }
        }

        
        private void ClearText()
        {
            Text = "";
        }

        private void SendText()
        {
            IsSent = true;
        }

        #region Commands
        private ICommand _clickOkCommand;
        public ICommand ClickOkCommand
        {
            get
            {
                return _clickOkCommand ?? (_clickOkCommand = new CommandHandler(() => SendText()));
            }
        }
        
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => ClearText()));
            }
        }
        #endregion
    }
}
