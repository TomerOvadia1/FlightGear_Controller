using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
using FlightSimulator.Views.Windows;

namespace FlightSimulator.ViewModels
{
    class FlightBoardButtons
    {
        private ICommand connectCommand;
        private ICommand settingsCommand;
        private ICommand exitCommand;


        public ICommand ConnectCmd
        {
            get
            {
                return connectCommand ?? (connectCommand =
                new Model.CommandHandler(() => Connect()));
            }
        }

        public ICommand OpenSettings
        {
            get
            {
                return settingsCommand ?? (settingsCommand =
                new Model.CommandHandler(() => OpenSettingsWindow()));
            }
        }

        public ICommand Exit
        {
            get
            {
                return exitCommand ?? (exitCommand =
                    new Model.CommandHandler(() => Quit()));
            }
        }

        private void Connect()
        {
            Commands.Instance.ConnectClient();
            Info.Instance.ConnectServer();
        }

        private void OpenSettingsWindow()
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
        }

        private void Quit()
        {
            Commands.Instance.Close();
            Info.Instance.Close();
        }
    }
}
