using GCMessaging;
using System;
using System.Linq;

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
            cm.mSender = RandomString(5);
            cm.mType = MessageType.CHAT;
            mSocketManager.DispatchMessage(cm);            
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
