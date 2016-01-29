using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using GCMessaging;

namespace GemCarryClient
{
    public class SocketManager
    {
        private Thread mServerHeartbeat;
        private Socket mServerSocket;
        private AsyncCallback mReadCallback;

        private const int CLIENT_THREAD_TIMEOUT = 40;
        private const int CLIENT_HEARTBEAT_WAIT = 5000;
        private const int BUFFER_SIZE = 8192;

        public SocketManager()
        {
            mServerSocket = null;
        }

        public void StartServerConnection()
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); //IPAddress.Parse("52.34.24.170");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 1025);

                // Create a TCP/IP  socket.
                mServerSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    mServerSocket.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        mServerSocket.RemoteEndPoint.ToString());

                    ListenLoop();

                    mServerHeartbeat = new Thread(Heartbeat);
                    //mServerHeartbeat.Start();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void EndServerConnection()
        {
            // Release the socket.
            mServerSocket.Close();
        }

        public void ListenLoop()
        {
            if (true == mServerSocket.Connected)
            {
                try
                {
                    if (null == mReadCallback)
                    {
                        mReadCallback = new AsyncCallback(ReadCallback);
                    }

                    SocketPacket packet = new SocketPacket(mServerSocket);

                    mServerSocket.BeginReceive(packet.buffer, 0, SocketPacket.BufferSize, SocketFlags.None, mReadCallback, packet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
            else
            {
                EndServerConnection();
            }
        }

        public void Heartbeat()
        {
            while(mServerSocket.Connected)
            {
                try
                {
                    MessageBase msg = new MessageBase();
                    DispatchMessage(msg);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }

                Thread.Sleep(CLIENT_HEARTBEAT_WAIT);
            }
        }

        public void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            SocketPacket packet = (SocketPacket)ar.AsyncState;
            Socket handler = packet.workSocket;

            // Read data from the client socket. 
            try
            {
                if (true == handler.Connected)
                {
                    SocketError errorCode;
                    int bytesRead = handler.EndReceive(ar, out errorCode);
                    if (SocketError.Success != errorCode)
                    {
                        bytesRead = 0;
                    }

                    if (bytesRead > 0)
                    {
                        // All the data has been read from the 
                        // client. Display it on the console.
                        Console.WriteLine("Read {0} bytes from socket.",
                            bytesRead);

                        // Check for end-of-file tag. If it is not there, read 
                        // more data.
                        int msgEnd = MessageHelper.FindEOM(packet.buffer);

                        if (msgEnd > -1)
                        {
                            // At least one full message read
                            while (msgEnd > -1)
                            {
                                byte[] dataMsg;
                                byte[] newMsg;

                                // Sorts out the message data into at least one full message, saves any spare bytes for next message
                                MessageHelper.ClearMessageFromStream(msgEnd, packet.buffer, out dataMsg, out newMsg);

                                // Do something with client message
                                MessageHandler.HandleMessage(dataMsg);

                                packet.buffer = newMsg;

                                msgEnd = MessageHelper.FindEOM(packet.buffer);
                            }
                        }
                        else
                        {
                            // Not all data received. Get more.
                            handler.BeginReceive(packet.buffer, 0, SocketPacket.BufferSize, 0,
                            mReadCallback, packet);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            ListenLoop();
        }

        public void DispatchMessage(MessageBase outMsg)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            byte[] compressed;

            formatter.Serialize(stream, outMsg);

            using (MemoryStream resultStream = new MemoryStream())
            {
                using (DeflateStream compressionStream = new DeflateStream(resultStream, CompressionMode.Compress))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(compressionStream);
                    compressionStream.Close();
                    compressed = resultStream.ToArray();
                }
            }

            byte[] msg;
            MessageHelper.AppendEOM(compressed, out msg);

            mServerSocket.Send(msg, msg.Length, SocketFlags.None);
        }

        public bool IsConnected()
        {
            return (null != mServerSocket) && mServerSocket.Connected;
        }
    }
}
