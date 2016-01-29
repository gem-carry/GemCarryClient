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
            Console.WriteLine("messageData: {0}", msgData);

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

            Console.WriteLine("Message received {0} bytes compressed into -> {1} bytes serialized ", msgData.Length, dataStream.Length);

            MessageBase msg = (MessageBase)formatter.Deserialize(dataStream);

            Console.WriteLine("Message received :{0}: {1} bytes ", msg.mType, msgData.Length);

            switch(msg.mType)
            {
                case MessageType.LOGIN: { EventManager.GetInstance().HandleMessage((LoginMessage)msg); break; }
                case MessageType.CHAT: { EventManager.GetInstance().HandleMessage((ChatMessage)msg); break; }
                case MessageType.SERVERRESPONSECODE: { EventManager.GetInstance().HandleMessage((ServerResponseCodeMessage)msg); break; }
                case MessageType.HEARTBEAT: // Fall-through intentional
                default: { EventManager.GetInstance().HandleMessage(msg); break; }
            }
        }
    }
}
