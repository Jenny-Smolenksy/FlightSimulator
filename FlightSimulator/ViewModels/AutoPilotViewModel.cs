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
        /// <summary>
        /// constructor
        /// </summary>
        public AutoPilotViewModel()
        {
            //connect to model
            model = AutoPilotModel.Instance;
            //initialize params
            IsSent = false;
        }
        #region Properties
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
        #endregion
        /// <summary>
        /// clear text from view
        /// </summary>
        private void ClearText()
        {
            Text = "";
        }
        /// <summary>
        /// send writen text to simulator
        /// </summary>
        private void SendText()
        {
            bool sentFlag = true;
            //get line breaks
            string[] commands = Regex.Split(Text, @"\r\n");
            //create new thread
            Thread thread = new Thread(delegate ()
            {
                //go thought the lines, send each line alone
                for (int i = 0; i < commands.Length; i++)
                {
                    //case empty line
                    if (commands[i] == String.Empty)
                        continue;
                    //send message to simulator
                    sentFlag &= model.SendMessage(commands[i]);
                    //wait 2 seconds
                    Thread.Sleep(2);
                }
                //check if info sent
                IsSent = sentFlag;
            });
            thread.Start();
          
        }

        #region Commands
        /// <summary>
        /// click ok command
        /// </summary>
        private ICommand _clickOkCommand;
        public ICommand ClickOkCommand
        {
            get
            {
                return _clickOkCommand ?? (_clickOkCommand = new CommandHandler(() => SendText()));
            }
        }
        /// <summary>
        /// click clear command
        /// </summary>
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
