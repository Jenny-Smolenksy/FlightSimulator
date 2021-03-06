﻿using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    public class SettingsWindowViewModel : BaseNotify
    {
        private ISettingsModel model;
        /// <summary>
        /// constuctor
        /// </summary>
        /// <param name="model"></param>
        public SettingsWindowViewModel(ISettingsModel model)
        {
            this.model = model;
        }
        #region Properties
        public string FlightServerIP
        {
            get { return model.FlightServerIP; }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        public int FlightCommandPort
        {
            get { return model.FlightCommandPort; }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        public int FlightInfoPort
        {
            get { return model.FlightInfoPort; }
            set
            {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }
        #endregion

        /// <summary>
        /// save configs
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }
        /// <summary>
        /// reload config from previous time
        /// </summary>
        public void ReloadSettings()
        {
            model.ReloadSettings();
        }

        #region Commands
        #region ClickCommand
        /// <summary>
        /// click command
        /// </summary>
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }
        /// <summary>
        /// handle click
        /// </summary>
        private void OnClick()
        {
            model.SaveSettings();
            MainModel.Instance.RequestToCloseSettings();
        }
        #endregion

        #region CancelCommand
        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }
        /// <summary>
        /// handle cancel
        /// </summary>
        private void OnCancel()
        {
            model.ReloadSettings();
        }
        #endregion
        #endregion
    }
}

