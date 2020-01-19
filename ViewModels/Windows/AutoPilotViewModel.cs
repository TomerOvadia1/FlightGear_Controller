using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Views;
using System.Windows.Input;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModels.Windows
{
    class AutoPilotViewModel : BaseNotify
    {
        private UC_AutoPilot autoPilot;

        public AutoPilotViewModel(UC_AutoPilot instance)
        {
            autoPilot = instance;
        }

        #region Commands
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => autoPilot.TB_Commands.Text="" ));
            }
        }
        #endregion
    }
}
