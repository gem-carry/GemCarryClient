using System;
using System.Runtime.Serialization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using GCMessaging;
using ProtoBuf;

namespace GemCarryClient
{

    class MessageHandler
    {
        public static void HandleMessage(Byte[] msgData)
        {
            using (MemoryStream stream = new MemoryStream(msgData))
            {
                BaseMessage msg = Serializer.Deserialize<BaseMessage>(stream);

                Console.WriteLine("Message received :{0}: {1} bytes ", msg.GetType().ToString(), msgData.Length);

                switch ((MessageType)msg.messageType)
                {
                    case MessageType.LoginResponse: { EventManager.GetInstance().HandleMessage((LoginResponse)msg); break; }
                    case MessageType.ChatMessage: { EventManager.GetInstance().HandleMessage((ChatMessage)msg); break; }
                    case MessageType.ConnectResponse: { EventManager.GetInstance().HandleMessage((ConnectResponse)msg); break; }
                    case MessageType.CreateUserResponse: { EventManager.GetInstance().HandleMessage((CreateUserResponse)msg); break; }
                    default: { Console.WriteLine("Cannot handle message type: {0}!", msg.messageType.ToString()); break; }
                }
            }
        }
    }
}
