using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        private AutoPilotModel model;
        public AutoPilotViewModel()
        {
            model = AutoPilotModel.Instance;
            IsSent = false;
        }

        private bool _isSent;
        public bool IsSent
        {
            get { return _isSent; }
            private set
            {
                _isSent = value;
                NotifyPropertyChanged("IsSent");
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                NotifyPropertyChanged("Text");
                if(value != "")
                {
                    IsSent = false;
                }
            }
        }


        private void ClearText()
        {
            Text = "";
        }

        private void SendText()
        {
            bool sentFlag = true;
            //go thought the lines, send each line alone
            string[] commands = Regex.Split(Text, @"\r\n");

            Thread thread = new Thread(delegate ()
            {
                for (int i = 0; i < commands.Length; i++)
                {
                    if (commands[i] == String.Empty)
                        continue;
                    sentFlag &= model.SendMessage(commands[i]);
                    Thread.Sleep(2);
                }
                IsSent = sentFlag;
            });
            thread.Start();
          
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
