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

            switch(msg.mType)
            {
                case MessageType.LOGIN: { EventManager.GetInstance().HandleMessage((LoginMessage)msg); break; }
                case MessageType.CHAT: { EventManager.GetInstance().HandleMessage((ChatMessage)msg); break; }
                case MessageType.HEARTBEAT: // Fall-through intentional
                default: { EventManager.GetInstance().HandleMessage(msg); break; }
            }
        }
    }
}
