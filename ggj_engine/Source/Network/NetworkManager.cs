using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using ggj_engine.Source.Entities.Player;
using ggj_engine.Source.Entities.Projectiles;
using ggj_engine.Source.Entities;

namespace ggj_engine.Source.Network
{
    public class NetworkManager
    {
        public const string IPSOFBlock = "<SOF>";
        public const string IPEOFBlock = "<EOF>";

        public Mutex mutexObj = new Mutex();
        public const int MAXNUMPLAYERS = 8;

        private static NetworkManager _instance;

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

        //private constructor so only one instance can be created
        private NetworkManager()
        {

        }

        public void Log(string msg)
        {
            Console.WriteLine("ALERT: NetworkManager:  " + msg);
        }

        public void Close()
        {

        }
    }
}
