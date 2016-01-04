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

            /*
            MessageBase msg = (MessageBase) formatter.Deserialize(stream);

            switch(msg.mType)
            {
                case MessageType.LOGIN:
                    {
                        MessageLogin loginMsg = (MessageLogin) formatter.Deserialize(msg.PullMessage());

                        // For Testing
                        Console.WriteLine("User login Message is u:" + loginMsg.mUsername + ", p:" + loginMsg.mPassword);
                        // End Testing

                        int status = LoginManager.AttemptLoginForClient(loginMsg/*, out UserDetails user*//*);

                        if(0 == status)
                        {
                            // Success!
                            //OutMessageUserDetails userMsg = new OutMessageUserDetails(
                            //client.DispatchMessage();
                        }
                        else
                        {
                            Console.WriteLine("Failed with reason " + status);
                        }

                        break;
                    }
                case MessageType.HEARTBEAT:
                default:
                    {
                        Console.WriteLine("Received Heartbeat from client");
                        return;
                    }
            }
            */
        }
    }
}
