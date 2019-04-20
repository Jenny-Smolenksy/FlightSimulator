using FlightSimulator.Model;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class MainWindowViewModel : BaseNotify
    {
        private Settings settingsWindow;
        private MainModel model;
        /// <summary>
        /// constuctor
        /// </summary>
        public MainWindowViewModel()
        {
            //connect to model layer
            this.model = MainModel.Instance;
            //sign to events
            model.onUserMessage += OnUserMessageRecived;
            model.onSettingCloseRequest += SettingCloseRequestHandle;
            //set info status for user
            _info = "welcome to flight  simulator, set settings and press ok to connect..";      
        }
        /// <summary>
        /// user information line
        /// </summary>
        private string _info;
        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                NotifyPropertyChanged("Info");
            }
        }
        /// <summary>
        /// handle setting closing window
        /// </summary>
        private void SettingCloseRequestHandle()
        {
            //close settings window
            settingsWindow.Close();
        }
        /// <summary>
        /// handle message to user
        /// </summary>
        /// <param name="message"></param>
        private void OnUserMessageRecived(string message)
        {
            Info = message;
        }       

        #region Commands
        /// <summary>
        /// click settings button
        /// </summary>
        private System.Windows.Input.ICommand _clickSettingsCommand;
        public System.Windows.Input.ICommand ClickSettingsCommand
        {
            get
            {
                return _clickSettingsCommand ??
                    (_clickSettingsCommand = new CommandHandler(() => OnSettingsClick()));
            }
        }
        /// <summary>
        /// settings click handle
        /// </summary>
        private void OnSettingsClick()
        {
            //create window, open it
            settingsWindow = new Settings();
            settingsWindow.Show();         
        }
        /// <summary>
        /// connect command
        /// </summary>
        private System.Windows.Input.ICommand _clickConnectCommand;       
        public System.Windows.Input.ICommand ClickConnectCommand
        {
            get
            {
                return _clickConnectCommand ??
                    (_clickConnectCommand = new CommandHandler(() => OnConnectCommand()));
            }
        }
        /// <summary>
        /// handle connect command
        /// </summary>
        private void OnConnectCommand()
        {
            Info = "opening info channel, waiting for simulator to connect..";
            model.Connect();
        }

        /// <summary>
        /// exit command
        /// </summary>
        private System.Windows.Input.ICommand _clickExitCommand;
        public System.Windows.Input.ICommand ClickExitCommand
        {
            get
            {
                return _clickExitCommand ??
                    (_clickExitCommand = new CommandHandler(() => OnExitCommand()));
            }
        }
    
        /// <summary>
        /// exit command handle
        /// </summary>
        private void OnExitCommand()
        {
            if(settingsWindow != null &&
                settingsWindow.Visibility == System.Windows.Visibility.Visible)
            {
                //close settings command
                settingsWindow.Close();
            }
            //disconnect from server
            model.DisConnect();
        }

        #endregion
    }
}
