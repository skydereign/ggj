using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using ggj_engine.Source.Entities.Player;

namespace ggj_engine.Source.Network
{
    struct AddrInfo
    {
        string hostname;
    }

    public class NetworkManager
    {
        List<string> packetsReceived;
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
            packetsReceived = new List<string>();
        }

        public void DetermineHost()
        {
            Console.WriteLine("Host? yes or no");

            string response = Console.ReadLine();

            if (response.CompareTo("yes") == 0)
                NetworkManager.Instance.CreateHost();
            else
                NetworkManager.Instance.ConnectToHost();

            Console.WriteLine();
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
                ManageHostGameState(gameTime, entities);
            }
            else
            {
                ManageClientGameState(gameTime, entities);
            }
        }

        private void ManageHostGameState(Microsoft.Xna.Framework.GameTime gameTime, List<Entities.Entity> entities)
        {
            //get data from game state and send to clients
            CheckConnectedClients();
            GetDataFromClients(gameTime);

            BeginPacket(gameTime);

            AddEntityDataToPacket(entities);

            EndPacket();

            SubmitDataToClients();
            GetACKFromClients();
        }

        private void AddEntityDataToPacket(List<Entities.Entity> entities)
        {
            for(int i = 0; i < entities.Count; ++i)
            {
                //send player data
                if(entities[i] is Entities.Player.Player)
                {
                    AddDataToPacket(",P," + i + "," + (entities[i] as Player).PlayerID + "," + entities[i].Position.X + "," + entities[i].Position.Y + ",");
                }
                else if(entities[i] is Entities.Enemies.Enemy)
                {
                    AddDataToPacket(",E," + i + "," + entities[i].Position.X + "," + entities[i].Position.Y + ",");
                }
            }
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

        private void SubmitDataToClients()
        {
            Host.WriteAll(Host.buffer);

            //clear buffer
            Host.buffer = "";
        }

        private void AddDataToPacket(string data)
        {
            Host.buffer += (data);
        }

        public void BeginPacket(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //send start of packet block
            Host.buffer += (IPSOFBlock + '\n');

            //write packet order number
            Host.buffer += (Host.CurrentPacketNumber.ToString() + '\n');

            //increment host packet number
            Host.CurrentPacketNumber++;

            //write the time the packet was sent
            Host.buffer += ("Time " + MillisSinceHostCreation + '\n');
        }

        public void EndPacket()
        {
            //send end of packet block
            Host.buffer += (IPEOFBlock + '\n');
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
                //receive all packets
                int packetStart = DetectPacketHeader(Client.RecvBuffer);
                int packetEnd = DetectEOF(packetStart, Client.RecvBuffer);

                while (Client.RecvBuffer.Length > 0 && packetStart != -1 && packetEnd != -1)
                {
                    string packet = Client.FlushBufferToIndex(packetStart, packetEnd);

                    //Console.WriteLine("Packet received:\n" + packet);

                    packetsReceived.Add(packet);

                    packetStart = DetectPacketHeader(Client.RecvBuffer);
                    packetEnd = DetectEOF(packetStart, Client.RecvBuffer);
                }

                for (int p = 0; p < packetsReceived.Count; ++p)
                {
                    string packet = packetsReceived[p];
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
                            int playerID = int.Parse(words[i + 2]);
                            float x = float.Parse(words[i + 3]);
                            float y = float.Parse(words[i + 4]);

                            entities[id].Position = new Vector2(x, y);
                        }
                    }

                    //done with packet now clear it/toss it
                    packetsReceived.RemoveAt(p);
                    --p;
                }

                packetsReceived.Clear();
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
