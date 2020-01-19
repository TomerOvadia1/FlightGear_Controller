using FlightSimulator.Model;
using FlightSimulator.Model.EventArgs;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class ManualPilotViewModel 
        //: BaseNotify
    {
        private Commands _model;

        public ManualPilotViewModel(Commands model)
        {
            _model = model;
        }

        const string set_cmd = "set ";
        const string aileron_location = "/controls/flight/aileron ";
        const string elevator_location = "/controls/flight/elevator ";
        const string rudder_location = "/controls/flight/rudder ";
        const string throttle_location = "/controls/engines/current-engine/throttle ";

        public void Knob_Moved(Joystick sender, VirtualJoystickEventArgs args)
        {
            //string[] locations = { aileron_location, elevator_location };
            //double[] values = { args.Aileron, args.Elevator };
            //for (int i= 0 ; i < locations.Length ; i++)
            //{
            //    string to_send = set_cmd + locations[i] + values[i] + "\r\n";
            //    _model.Send(to_send);
            //    //to_send = get_cmd + locations[i] + "/r/n";
            //    //client.Send(to_send);
            //}

            //send to properties
            VM_Aileron = args.Aileron;
            VM_Elevator = args.Elevator;
        }

        private double VM_aileron;
        public double VM_Aileron
        {
            get { return VM_aileron; }
            set
            {
                VM_aileron = value;
                //send comand to model
                string to_send = set_cmd + aileron_location + value + "\r\n";
                _model.Send(to_send);

                // NotifyPropertyChanged("VM_aileron");
            }
        }

        private double VM_elevator;
        public double VM_Elevator
        {
            get { return VM_elevator; }
            set
            {
                VM_elevator = value;
                string to_send = set_cmd + elevator_location + value + "\r\n";
                //send comand to model
                _model.Send(to_send);
                // NotifyPropertyChanged("VM_elevator");
            }
        }


        private double VM_rudder;
        public double VM_Rudder
        {
            get { return VM_rudder; }
            set
            {
                VM_rudder = value;
                string to_send = set_cmd + rudder_location + value + "\r\n";
                //send comand to model
                _model.Send(to_send);
               // NotifyPropertyChanged("VM_rudder");
            }
        }


        private double VM_throttle;
        public double VM_Throttle
        {
            get { return VM_throttle; }
            set
            {
                VM_throttle = value;
                string to_send = set_cmd + throttle_location + value + "\r\n";
                //send comand to model
                _model.Send(to_send);
               // NotifyPropertyChanged("VM_throttle");

            }
        }


    }
}
