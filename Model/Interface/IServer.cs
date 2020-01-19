using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface IServer
    {
        void Setup(ViewModels.FlightBoardViewModel flightBoardVM);
        void Open();
        void Close();
        void AcceptCall();
        void RecieveCall(ViewModels.FlightBoardViewModel flightBoardVM);
    }
}
