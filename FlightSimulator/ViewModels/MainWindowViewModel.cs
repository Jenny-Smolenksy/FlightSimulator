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
            Settings settingsWindow = new Settings();
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
        }

        #endregion
    }
}
