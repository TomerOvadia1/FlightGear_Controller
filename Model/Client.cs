using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model
{
    class Client : IClient
    {
        //fgfs --altitude=5000 --heading=0 --vc=110
        const int MAX_ATTEPMS=25 ;
        string _IPAdress;
        int _port;
        TcpClient _client;
        bool stop;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public Client(string ip, int port)
        {
            _IPAdress = ip;
            _port = port;
            _client = null;
            stop = true;
        }

        /// <summary>
        /// return true if client is connected to server false otherwise
        /// </summary>
        /// <returns></returns>
        private bool Connected()
        {
            return (_client != null && _client.Connected);
        }

        /// <summary>
        /// sends data to server if connected
        /// </summary>
        /// <param name="data"></param>
        public void Send(string data)
        {

            if (!Connected())
            {
                return;
            }

            new Thread(() =>
           {

               NetworkStream stream = _client.GetStream();
               // Send data to server
               byte[] buffer = Encoding.ASCII.GetBytes(data);
               stream.Write(buffer, 0, buffer.Length);
               Console.WriteLine("To Send " + Encoding.ASCII.GetString(buffer));
               Console.WriteLine("To Send bytes " + buffer.Length);
               //Recieve();
               return;
           }).Start();
        }

        /// <summary>
        /// recive answer from server
        /// </summary>
        public void Recieve()
        {
            if ( Connected() )
            {
                NetworkStream stream = _client.GetStream();

                byte[] bytesToRead = new byte[_client.ReceiveBufferSize];
                int bytesRead = stream.Read(bytesToRead, 0, _client.ReceiveBufferSize);
                Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            }
        }

        /// <summary>
        /// connect to given server.
        /// if connection is not astablished after MAX_ATTEPMS function returns
        /// </summary>
        public void Connect()
        {
            int attemps = 1;
            stop = false;
            if (_client != null && _client.Connected) {
                Console.WriteLine("Client allready connected !");
                return;
            }

            new Thread(() =>
            {
                while (!stop)
                {
                    try
                    {
                        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(_IPAdress), _port);
                        _client = new TcpClient();
                        _client.Connect(ep);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Connected as client ! (Commands interface connected) ");
                        Console.ResetColor();
                        //stop = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        Thread.Sleep(250);
                        //Console.WriteLine(e.Message);
                        Console.WriteLine("attemp number : {0}", attemps);
                        if (attemps == MAX_ATTEPMS)
                        {
                            this.Stop();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Reached maximum number of connection attemps ({0})", MAX_ATTEPMS);
                            Thread.Sleep(100);
                            Console.WriteLine("Shutting down client...", MAX_ATTEPMS);
                            Console.ResetColor();

                        }
                        attemps++;

                    }
                    
                }
            }).Start();

        }

        /// <summary>
        /// stops connection and closes client
        /// </summary>
        public void Stop()
        {
            stop = true;
            if ( Connected() )
            {
                _client.Close();

            }
        }


    }
}
