using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ggj_engine.Source.Network
{
    public class SocketInfo
    {
        public Socket sock;
        public IPEndPoint ip;
        public IPEndPoint host;

        public SocketInfo()
        {

        }
    };
}
