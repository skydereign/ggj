using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ggj_engine.Source.Network
{
    public class HostTCP
    {
        private string _ip;
        private int _port;

        private List<IPEndPoint> _clientIPs;

        private Socket _s;

        private Thread listeningThread;

        public HostTCP()
        {

        }

        public void Start(string ip, int port)
        {
            _ip = ip;
            _port = port;

            _clientIPs = new List<IPEndPoint>();

            _s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress addr = IPAddress.Parse(_ip);
            IPEndPoint hostAddr = new IPEndPoint(addr, _port);

            _s.Bind(hostAddr);
        }

        public void Listen()
        {   
            _s.Listen(10);

            ThreadStart ts = new ThreadStart(
                () =>
                {
                    //if there is an exception thrown, catch it and continue looking for connections
                    while (true)
                    {
                        try
                        {
                            //continuously poll for connections
                            while (true)
                            {
                                byte[] buf = new byte[1024];
                                Socket handler = _s.Accept();
                                Console.WriteLine("Client connected");
                                if (handler.Receive(buf) > 0)
                                    Console.WriteLine("Data received from client: " + Encoding.ASCII.GetString(buf));
                            }
                        }
                        catch (SocketException e)
                        {

                        }
                    }
                }
            );

            listeningThread = new Thread(ts);
            listeningThread.Start();
        }

        public int Read()
        {
            int bytesRead = 0;

            byte[] buf = new byte[256];

            bytesRead = _s.Receive(buf);

            string msg = Encoding.ASCII.GetString(buf);

            Console.WriteLine("Received from client: " + msg);

            return bytesRead;
        }

        public void Write(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            Console.WriteLine("Writing to client: " + data);

            
        }
    }
}
