using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {

        private string instructs;
        private bool isRed = false;

        public bool IsRed
        {
            get => isRed;
            set
            {
                isRed = value;
                NotifyPropertyChanged("IsRed");
            }
        }

        public string Instructs
        {
            get
            {
                return instructs;
            }
            set
            {
                instructs = value;
                if (value.Any())
                {
                    IsRed = true;
                }
                else
                {
                    IsRed = false;
                }
                NotifyPropertyChanged("Instructs");
            }
        }

        private ICommand _sendInstructs;
        public ICommand sendInstructs
        {
            get
            {
                return _sendInstructs ?? (_sendInstructs =
                    new CommandHandler(() => Send()));
            }
        }
        private void Send()
        {
            new Thread(() =>
            {
                string[] commands = Instructs.Split(new[] { "\r\n", "\r", "\n" },
                            StringSplitOptions.None);
                foreach (string cmd in commands)
                {
                    Commands.Instance.Send(cmd + "\r\n");
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
                IsRed = false;
            }).Start();
        }

        private ICommand _clearCmd;
        public ICommand clearCmd
        {
            get
            {
                return _clearCmd ?? (_clearCmd =
                    new CommandHandler(() => Clear()));
            }
        }
        private void Clear()
        {
            Instructs = "";
            IsRed = false;
        }

    }
}
