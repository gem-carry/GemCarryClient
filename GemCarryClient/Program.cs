using GCMessaging;
using System;

namespace GemCarryClient
{
    class Program
    {
        private static SocketManager mSocketManager;

        static void Main(string[] args)
        {
            mSocketManager = new SocketManager();
            mSocketManager.StartServerConnection();

            System.Threading.Thread.Sleep(1000);

            ChatMessage cm = new ChatMessage();
            Console.Write("Enter Message ===>>");
            cm.mMessage = Console.ReadLine();
            cm.mSender = "Client";
            cm.mType = MessageType.CHAT;
            mSocketManager.DispatchMessage(cm);
        }
    }
}
