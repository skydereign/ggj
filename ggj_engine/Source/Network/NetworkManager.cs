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

        private Thread _acceptThread;
        private Thread _sendThread;
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
            HostTCP _host = new HostTCP();

            _host.Start(_localHostIP, _port);

            Log("Host created.");

            _isHost = true;

            Log("Host is now listening.");

            _host.Listen();
        }

        public void ConnectToHost()
        {
            if (_isHost)
            {
                Log("Cannot connect to host when this is the host");
                return;
            }

            ClientTCP _client = new ClientTCP();
            _client.Connect(_localHostIP, _port);

            _client.Write("Hello, Host! Love, Client");
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

        public void Log(string msg)
        {
            Console.WriteLine("ALERT: NetworkManager:  " + msg);
        }

        public void Close()
        {
            //close threads

            //close sockets
        }
    }
}
