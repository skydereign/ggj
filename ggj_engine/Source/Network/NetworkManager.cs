using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;

namespace ggj_engine.Source.Network
{
    struct AddrInfo
    {
        string hostname;
    }

    public class NetworkManager
    {
        public const string IPSOFBlock = "<SOF>";
        public const string IPEOFBlock = "<EOF>";

        public float MillisSinceHostCreation = 0.0f;

        public Mutex mutexObj = new Mutex();
        public const int MAXNUMPLAYERS = 8;

        private static NetworkManager _instance;

        private bool _isHost;

        private string _hostIP = "127.0.0.1";
        private string _localHostIP = "127.0.0.1";
        private int _port = 1337;

        private HostTCP _host;
        private ClientTCP _client;

        public HostTCP Host
        {
            get
            {
                return _host;
            }
        }

        public ClientTCP Client
        {
            get
            {
                return _client;
            }
        }

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

        public void CreateHost()
        {
            _host = new HostTCP();

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

            _client = new ClientTCP();
            _client.Connect(_localHostIP, _port);
            
            _client.Write("Hello, Host! Love, Client");

            _client.ReadOnThread();
        }

        public void Solve(Microsoft.Xna.Framework.GameTime gameTime, List<Entities.Entity> entities)
        {
            MillisSinceHostCreation += gameTime.ElapsedGameTime.Milliseconds;

            if (IsHost)
            {
                ManageHostGameState(gameTime);
            }
            else
            {
                ManageClientGameState(gameTime, entities);
            }
        }

        private void ManageHostGameState(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //get data from game state and send to clients
            CheckConnectedClients();
            GetDataFromClients(gameTime);
            SendDataToClients(gameTime);
            GetACKFromClients();
        }

        private void CheckConnectedClients()
        {
            _host.CheckConnectedClients();
        }

        private void GetDataFromClients(Microsoft.Xna.Framework.GameTime gameTime)
        {
            for (int i = 0; i < Host.NumConnectedPlayers; ++i)
            {
                Host.ReadFromClient(i);
            }
        }

        private void SendDataToClients(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //if data to send > 0

            //send start of packet block
            Host.WriteAll(IPSOFBlock + '\n');

            //write packet order number
            Host.WriteAll(Host.CurrentPacketNumber.ToString() + '\n');

            //increment host packet number
            Host.CurrentPacketNumber++;

            //write the time the packet was sent
            Host.WriteAll("Time " + gameTime.ElapsedGameTime.Milliseconds + '\n');

            Host.WriteAll("This should be one packet\n");

            //send end of packet block
            Host.WriteAll(IPEOFBlock + '\n');
        }

        private void GetACKFromClients()
        {
            //get ACK from clients

            //compare latest packet number
        }

        private void ManageClientGameState(Microsoft.Xna.Framework.GameTime gameTime, List<Entities.Entity> entities)
        {
            lock (mutexObj)
            {

                int packetStart = DetectPacketHeader(Client.RecvBuffer);
                int packetEnd = DetectEOF(packetStart, Client.RecvBuffer);

                if (Client.RecvBuffer.Length > 0 && packetStart != -1 && packetEnd != -1)
                {
                    string packet = Client.FlushBufferToIndex(packetStart, packetEnd, Client.RecvBuffer);

                    Console.WriteLine("Packet received:\n" + packet);

                    string[] words = packet.Split(new char[] { ':', ' ', ',', '\0' });

                    for (int i = 0; i < words.Length; ++i)
                    {
                        if (words[i] == "E" && i + 4 < words.Length)
                        {
                            int id = int.Parse(words[i + 1]);
                            float x = float.Parse(words[i + 2]);
                            float y = float.Parse(words[i + 3]);

                            entities[id].Position = new Vector2(x, y);
                        }
                        else if (words[i] == "P" && i + 4 < words.Length)
                        {
                            int id = int.Parse(words[i + 1]);
                            float x = float.Parse(words[i + 2]);
                            float y = float.Parse(words[i + 3]);

                            entities[id].Position = new Vector2(x, y);
                        }
                    }
                }
            }
        }

        private int DetectPacketHeader(string packet)
        {
            int invalidIndex = -1;

            for (int i = 0; i + IPSOFBlock.Length < packet.Length; ++i)
            {
                for (int h = 0; h < IPSOFBlock.Length; ++h)
                {
                    if (packet[i + h] == IPSOFBlock[h])
                    {
                        if (h == IPSOFBlock.Length - 1)
                        {
                            //Log("Start of TCP HEADER detected!!!");
                            return i;
                        }
                    }
                    else break;
                }
            }

            return invalidIndex;
        }

        private int DetectEOF(int packetStart, string packet)
        {
            int invalidIndex = -1;

            if (packetStart != -1)
            {

                for (int i = packetStart; i + IPEOFBlock.Length < packet.Length; ++i)
                {
                    for (int h = 0; h < IPEOFBlock.Length; ++h)
                    {
                        if (packet[i + h] == IPEOFBlock[h])
                        {
                            if (h == IPEOFBlock.Length - 1)
                            {
                                Log("END of TCP HEADER detected!!!");
                                return i + h + 1;
                            }
                        }
                        else break;
                    }
                }
            }

            return invalidIndex;
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
