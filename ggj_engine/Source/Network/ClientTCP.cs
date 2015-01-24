using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ggj_engine.Source.Network
{
    public class ClientTCP
    {
        private string ip;
        private string _hostIP;
        private int _port;

        private Socket _s;

        private byte[] buf = new byte[256];

        public ClientTCP()
        {

        }

        public void Connect(string hostIP, int port)
        {
            _hostIP = hostIP;
            _port = port;
            
            _s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress addr = IPAddress.Parse(_hostIP);
            IPEndPoint hostAddr = new IPEndPoint(addr, _port);
            
            _s.Connect(hostAddr);
        }

        public int Read()
        {
            int bytesRead = 0;

            byte[] buf = new byte[256];

            bytesRead = _s.Receive(buf);

            string msg = Encoding.ASCII.GetString(buf);

            Console.WriteLine("Received from host: " + msg);

            return bytesRead;
        }

        public void Write(byte[] data)
        {

        }

        public void Write(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            Console.WriteLine("Writing to host: " + data);

            _s.Send(bytes);
        }
    }
}
