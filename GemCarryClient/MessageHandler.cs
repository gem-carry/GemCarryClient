using System;
using System.Runtime.Serialization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using GCMessaging;

namespace GemCarryClient
{

    class MessageHandler
    {
        public static void HandleMessage(Byte[] msgData)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream dataStream = new MemoryStream();
            using (MemoryStream compressedStream = new MemoryStream(msgData))
            {
                using (DeflateStream ds = new DeflateStream(compressedStream, CompressionMode.Decompress))
                {
                    ds.CopyTo(dataStream);
                    ds.Close();
                }
                dataStream.Position = 0;
            }

            MessageBase msg = (MessageBase)formatter.Deserialize(dataStream);
            ChatMessage c = (ChatMessage)msg;
            Console.WriteLine(c.mMessage);

            switch(msg.mType)
            {
                case MessageType.CHAT:
                    {
                        ChatMessage chatMsg = (ChatMessage)msg;
                        Console.WriteLine(String.Format("{0}: {1}", chatMsg.mSender, chatMsg.mMessage));
                        return;
                    }

                case MessageType.HEARTBEAT:
                default:
                    {
                        Console.WriteLine("Received Heartbeat from client");
                        return;
                    }
            }
        }
    }
}
