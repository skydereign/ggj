using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ggj_engine.Source.Network
{
    public class Address
    {
        public IPEndPoint endPoint;

        private ushort port;
        private uint address;

        public Address()
        {
            address = 0;
            port = 0;
        }

        public Address( char a, char b, char c, char d, ushort port)
        {
            this.port = port;

            this.address = (uint)(a << 24 | b << 16 | c << 8 | d);
        }

        public Address(uint address, ushort port)
        {
            this.address = address;
        }

        public uint GetAdress()
        {
            return address;
        }

        public char GetA()
        {
            return (char)(this.address >> 24);
        }

        public char GetB()
        {
            return (char)(this.address >> 16);
        }

        public char GetC()
        {
            return (char)(this.address >> 8);
        }

        public char GetD()
        {
            return (char)(this.address);
        }

        public ushort GetPort()
        {
            return this.port;
        }
    }
}
