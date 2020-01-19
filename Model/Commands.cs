using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model
{

    class Commands
    {
        #region Singleton
        private static Commands m_Instance = null;
        private IClient _client;

        private Commands()
        {
            _client = new Client(ApplicationSettingsModel.Instance.FlightServerIP, 
                ApplicationSettingsModel.Instance.FlightCommandPort);
        }

        public static Commands Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Commands();
                }
                return m_Instance;
            }
        }
        #endregion

        public void ConnectClient()
        {
            _client.Connect();
        }

        public void Send(string data)
        {
            // TODO: Check if Threda is needed .
            _client.Send(data);
            
        }

        public void Close()
        {
            _client.Stop();
        }

    }
}
