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

        public MainWindowViewModel()
        {
            this.model = MainModel.Instance;
            model.onUserMessage += OnUserMessageRecived;
            model.onSettingCloseRequest += SettingCloseRequestHandle;
            _info = "welcome to flight  simulator, set settings and press ok to connect..";
           

        }

        private void SettingCloseRequestHandle()
        {
            settingsWindow.Close();
        }

        private void OnUserMessageRecived(string message)
        {
            Info = message;
        }

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

        #region Commands
        #region ClickCommand
        private System.Windows.Input.ICommand _clickSettingsCommand;
        public System.Windows.Input.ICommand ClickSettingsCommand
        {
            get
            {
                return _clickSettingsCommand ??
                    (_clickSettingsCommand = new CommandHandler(() => OnSettingsClick()));
            }
        }
        private void OnSettingsClick()
        {
            settingsWindow = new Settings();
            settingsWindow.Show();
            
          
        }
        #endregion

        private System.Windows.Input.ICommand _clickConnectCommand;
       
        public System.Windows.Input.ICommand ClickConnectCommand
        {
            get
            {
                return _clickConnectCommand ??
                    (_clickConnectCommand = new CommandHandler(() => OnConnectCommand()));
            }
        }
        private void OnConnectCommand()
        {
            Info = "opening info channel, waiting for simulator to connect..";
            model.Connect();
        }

        
        private System.Windows.Input.ICommand _clickExitCommand;
        public System.Windows.Input.ICommand ClickExitCommand
        {
            get
            {
                return _clickExitCommand ??
                    (_clickExitCommand = new CommandHandler(() => OnExitCommand()));
            }
        }
    
        private void OnExitCommand()
        {
            if(settingsWindow != null &&
                settingsWindow.Visibility == System.Windows.Visibility.Visible)
            {
                settingsWindow.Close();
            }
            model.DisConnect();
        }

        #endregion
    }
}
