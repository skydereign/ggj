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

        private List<SocketInfo> _clientInfo;

        private SocketInfo hostInfo;

        private Thread listeningThread;

        public int NumConnectedPlayers
        {
            get
            {
                return _clientInfo.Count;
            }
        }

        public HostTCP()
        {

        }

        public void Start(string ip, int port)
        {
            _ip = ip;
            _port = port;

            _clientInfo = new List<SocketInfo>();

            hostInfo = new SocketInfo();

            hostInfo.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress addr = IPAddress.Parse(_ip);
            IPEndPoint hostAddr = new IPEndPoint(addr, _port);

            hostInfo.sock.Bind(hostAddr);

            //hostInfo.ip = hostInfo.sock.LocalEndPoint as IPEndPoint;
            //hostInfo.host = hostInfo.sock.RemoteEndPoint as IPEndPoint;
        }

        //Listen for any connections
        public void Listen()
        {   
            hostInfo.sock.Listen(10);

            ThreadStart ts = new ThreadStart(
                () =>
                {
                    //if there is an exception thrown, catch it and continue looking for connections
                    while (true)
                    {
                        if (_clientInfo.Count < NetworkManager.MAXNUMPLAYERS)
                        {
                            try
                            {
                                //continuously poll for connections
                                LookForConnections();
                            }
                            catch (SocketException e)
                            {
                                Console.WriteLine("EXCPN THROWN:\n" + e.Message);
                            }
                        }
                    }
                }
            );

            listeningThread = new Thread(ts);
            listeningThread.Start();
        }

        private void LookForConnections()
        {
            while (_clientInfo.Count < NetworkManager.MAXNUMPLAYERS)
            {
                byte[] buf = new byte[1024];

                Socket handler = hostInfo.sock.Accept();

                _clientInfo.Add(new SocketInfo());
                _clientInfo[NumConnectedPlayers - 1].ip = handler.LocalEndPoint as IPEndPoint;
                _clientInfo[NumConnectedPlayers - 1].host = handler.RemoteEndPoint as IPEndPoint;
                _clientInfo[NumConnectedPlayers - 1].sock = handler;


                Console.WriteLine("Client connected: " + _clientInfo.Count);

                if (handler.Receive(buf) > 0)
                {
                    Console.WriteLine("Data received from client: " + Encoding.ASCII.GetString(buf));
                    _clientInfo[NumConnectedPlayers - 1].sock.Send(Encoding.ASCII.GetBytes("Hello, Client! Love you too! - Host"));
                    _clientInfo[NumConnectedPlayers - 1].sock.Send(Encoding.ASCII.GetBytes("Hello, Client! Love you too!2222 - Host"));
                }
            }

            WriteAll("All clients loaded! Awesome!");
        }

        public int Read()
        {
            int bytesRead = 0;

            byte[] buf = new byte[256];

            bytesRead = hostInfo.sock.Receive(buf);

            string msg = Encoding.ASCII.GetString(buf);

            Console.WriteLine("Received from client: " + msg);

            return bytesRead;
        }

        public void WriteAll(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            //Console.WriteLine("Writing to all clients: " + data);

            for(int i = 0; i < _clientInfo.Count; ++i)
            {
                Write(bytes, _clientInfo[i]);
            }
        }

        public void Write(byte[] data, SocketInfo info)
        {
            info.sock.Send(data);
        }

        public void Write(string data, SocketInfo info)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            //Console.WriteLine("Writing to client: " + data);

            info.sock.Send(bytes);
        }
    }
}
