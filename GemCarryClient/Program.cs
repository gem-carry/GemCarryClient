using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using GCMessaging;

namespace GemCarryClient
{
    class Program
    {
        private static SocketManager mSocketManager;

        static void Main(string[] args)
        {
            mSocketManager = new SocketManager();
            mSocketManager.StartServerConnection();
        }
    }
}
