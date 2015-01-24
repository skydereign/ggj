using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ggj_engine.Source.Network
{
    public class ClientTCP
    {
        private string ip;
        private string _hostIP;
        private int _port;

        private SocketInfo sInfo;

        private string _buffer;

        public string Buffer
        {
            get { return _buffer; }
        }

        public ClientTCP()
        {

        }

        public void Connect(string hostIP, int port)
        {
            _hostIP = hostIP;
            _port = port;

            sInfo = new SocketInfo();

            sInfo.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress addr = IPAddress.Parse(_hostIP);
            IPEndPoint hostAddr = new IPEndPoint(addr, _port);

            sInfo.sock.Connect(hostAddr);

            sInfo.ip = sInfo.sock.LocalEndPoint as IPEndPoint;
            sInfo.host = sInfo.sock.RemoteEndPoint as IPEndPoint;
        }

        public void ReadOnThread()
        {
            ThreadStart ts = new ThreadStart(
                () =>
                {
                    //if there is an exception thrown, catch it and continue looking for connections
                    while (true)
                    {
                        Read();
                    }
                });

            Thread t = new Thread(ts);
            t.Start();
        }

        public int Read()
        {
            int bytesRead = 0;

            byte[] buf = new byte[256];

            bytesRead = sInfo.sock.Receive(buf);

            if (bytesRead > 0)
            {
                lock (NetworkManager.Instance.mutexObj)
                {
                    string msg = Encoding.ASCII.GetString(buf);

                    //Console.WriteLine("Received from host: " + msg);

                    _buffer += msg;
                }
            }

            return bytesRead;
        }

        public void FlushBuffer()
        {
            _buffer = "";
        }

        public void Write(byte[] data)
        {

        }

        public void Write(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            
            Console.WriteLine("Writing to host: " + data);

            sInfo.sock.Send(bytes);
        }
    }
}
