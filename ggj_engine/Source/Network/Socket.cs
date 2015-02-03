using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ggj_engine.Source.Network
{
    public class UDPSocket
    {
        private Socket s;

        public UDPSocket()
        {

        }

        public bool Open(ushort port)
        {
            if (IsOpen())
            {
                Console.WriteLine("Socket could not be opened. It is already open!!");
                return false;
            }

            if(null == (s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)))
            {
                Console.WriteLine("Error creating socket!!");
                s = null;
                return false;
            }

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);

            s.Bind(localEndPoint);

            return true;
        }

        public void Close()
        {
            s.Close();
        }

        public bool IsOpen()
        {
            if (null != s)
                return true;

            return false;
        }

        public bool Send(Address destination, byte[] data, int size)
        {
            int bytesSent = s.SendTo(data, size, SocketFlags.None, destination.endPoint);

            return bytesSent == size;
        }

        public int Receive(Address sender, byte[] data, int size)
        {
            int bytesRead = 0;

            if (null == s)
                return 0;

            EndPoint remoteEP = sender.endPoint;

            bytesRead = s.ReceiveFrom(data, size, SocketFlags.None, ref remoteEP);

            if (bytesRead <= 0)
                return 0;

            return bytesRead;
        }
    }
}
