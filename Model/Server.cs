using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FlightSimulator.Model
{
    class Server : IServer
    {
        //4Hz rate
        const int FREQ = 250;
        const int TIMEOUT = 5000;

        string _IPAdress;
        int _port;
        TcpListener _listener;
        TcpClient _client;
        volatile bool stop;
        bool connected;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="portno"></param>
        public Server(string ip, int portno)
        {
            _IPAdress = ip;
            _port = portno;
            _listener = null;
            _client = null;
            connected = false;
        }

        /// <summary>
        /// setup a server
        /// </summary>
        /// <param name="viewModel"></param>
        public void Setup(FlightBoardViewModel viewModel)
        {
            /**
             * Open();
            Thread t = new Thread(() =>
            {
                AcceptCall();
                accepted = true;
                RecieveCall();
            });
            t.Start();
            Thread.Sleep(TIMEOUT);
            if (!accepted)
            {
                Console.WriteLine("Server Could not connect . Timeout Occurred .");

            }
            */
            if (!connected)
            {
                Open();
                AcceptCall();
                RecieveCall(viewModel);
            }


        }

        /// <summary>
        /// start listening for clients
        /// </summary>
        public void Open()
        {
            if (_listener == null)
            {
                try
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(_IPAdress), _port);
                    _listener = new TcpListener(ep);
                    _listener.Start();
                    Console.WriteLine("Waiting for client connections...");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        /// <summary>
        /// accepts call from client
        /// </summary>
        public void AcceptCall()
        {
            if (_listener != null)
            {
                try
                {
                    _client = _listener.AcceptTcpClient();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Client connected to server (Info interface connected)");
                    Console.ResetColor();
                    connected = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// recives data from client
        /// </summary>
        /// <param name="viewModel"></param>
        public void RecieveCall(FlightBoardViewModel viewModel)
        {
            new Thread(() =>
            {
                int msg_num = 0;
                NetworkStream stream = _client.GetStream();
                //loop until stop raises
                while (!stop)
                {
                    if (_client != null && _client.Available > 0)
                    {
                        Console.WriteLine("Recieved info , attemp num = {0}.", msg_num);
                        int recieved_len = _client.Available;
                        Console.WriteLine("Recieved len = {0}.", recieved_len);
                        Byte[] bytes = new byte[recieved_len];
                        stream.Read(bytes, 0, recieved_len);
                        string data = Encoding.ASCII.GetString(bytes);
                        Console.WriteLine(data.Length.ToString());
                        // Console.WriteLine("[{0}]", data);

                        string[] info = data.Split(',');
                        double lon = Convert.ToDouble(info[0]);
                        double lat = Convert.ToDouble(info[1]);
                        viewModel.Lon = lon;
                        viewModel.Lat = lat;

                        msg_num++;
                    }
                    //recieve massages frequencie
                    Thread.Sleep(FREQ);
                }

            }).Start();


        }

        /// <summary>
        /// close server and sockets
        /// </summary>
        public void Close()
        {
            stop = true;
            if (_client != null && _client.Connected)
            {
                _client.Close();

            }
            if (_listener != null)
            {
                _listener.Stop();
            }
            _client = null;
            _listener = null;
        }

    }
}