using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for UC_ManualPilot.xaml
    /// </summary>
    public partial class UC_ManualPilot : UserControl
    {
        ManualPilotViewModel vm;

        public UC_ManualPilot()
        {
            InitializeComponent();
            vm = new ManualPilotViewModel(Commands.Instance);
            //adds event
            Joystick_Element.Moved += vm.Knob_Moved;
            this.DataContext = vm;
        }

    }
}
