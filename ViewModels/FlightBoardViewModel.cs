using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        double lat, lon;
        private FlightBoardViewModel flightBoardVM;
        public FlightBoardViewModel()
        {
            Info.Instance.FlightBoardVM = this;
        }

        public double Lon
        {
            set
            {
                Console.WriteLine($"lon: {lon}");
                this.lon = value;
                NotifyPropertyChanged("Lon");
            }
            get { return this.lon; }
        }

        public double Lat
        {
            set
            {
                Console.WriteLine($"lat: {lat}");
                this.lat = value;
                NotifyPropertyChanged("Lat");
            }
            get
            {
                return this.lat;
            }
        }
    }
}
