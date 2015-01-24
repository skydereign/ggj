using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ggj_engine.Source.Network
{
    struct AddrInfo
    {
        string hostname;
    }

    public class NetworkManager
    {
        private static NetworkManager _instance;

        private Thread _recvThread;

        private bool _isHost;

        private string _hostIP = "127.0.0.1";
        private string _localHostIP = "127.0.0.1";
        private int _port = 1337;

        public bool IsHost
        {
            get
            {
                return _isHost;
            }
        }

        public static NetworkManager Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new NetworkManager();

                return _instance;
            }
        }

        public void CreateHost()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress addr = IPAddress.Parse(_localHostIP);
            IPEndPoint hostAddr = new IPEndPoint(addr, _port);

            s.Bind(hostAddr);
            s.Listen(int.MaxValue);
        }

        public void ConnectToHost()
        {
            if (_isHost)
            {
                Console.WriteLine("Cannot connect to host when this is the host");
                return;
            }

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress addr = IPAddress.Parse(_localHostIP);
            IPEndPoint hostAddr = new IPEndPoint(addr, _port);

            s.Connect(hostAddr);
        }

        private void Solve()
        {

        }

        private NetworkManager()
        {

        }

        private void ReadPacket()
        {

        }

        private void WriteTCPPacket()
        {

        }

        private void WriteUDPPacket()
        {

        }

        public void FlushDebug()
        {

        }

        public void Close()
        {
            //close threads

            //close sockets
        }
    }
}
