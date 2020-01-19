using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model
{
    class Info
    {
        #region Singleton
        private static Info m_Instance = null;
        private IServer _server;
        private FlightBoardViewModel flightBoardVM;

        private Info()
        {
            _server = new Server(ApplicationSettingsModel.Instance.FlightServerIP,
                ApplicationSettingsModel.Instance.FlightInfoPort);
        }

        public static Info Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Info();
                }
                return m_Instance;
            }
        }

        public FlightBoardViewModel FlightBoardVM
        {
            get { return flightBoardVM; }
            set { flightBoardVM = value; }
        }
        #endregion

        public void ConnectServer()
        {
            _server.Setup(flightBoardVM);
        }

        /**
         * public void Open()
        {
            _server.Open();
        }

        public void AcceptCall()
        {
            _server.AcceptCall();

        }

        public void RecieveCall()
        {
            _server.RecieveCall();
        }

        
        **/

        public void Close()
        {
            _server.Close();
        }
    }
}
