using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            switch(msg.mType)
            {
                case MessageType.CHAT:
                    {
                        ChatMessage chatMsg = (ChatMessage)msg;
                        Console.WriteLine("{0}: {1}", chatMsg.mSender, chatMsg.mMessage);
                        break;
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
