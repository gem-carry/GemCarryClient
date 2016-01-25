using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Thread mServerSocketListener;
        private Thread mServerHeartbeat;
        private Socket mServerSocket;

        private const int CLIENT_THREAD_TIMEOUT = 40;
        private const int CLIENT_HEARTBEAT_WAIT = 5000;
        private const int BUFFER_SIZE = 8192;

        public SocketManager()
        {
            mServerSocketListener = null;
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

                    mServerSocketListener = new Thread(ListenLoop);
                    mServerSocketListener.Start();

                    mServerHeartbeat = new Thread(Heartbeat);
                    mServerHeartbeat.Start();
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
            mServerSocket.Shutdown(SocketShutdown.Both);
            mServerSocket.Close();
        }

        public void ListenLoop()
        {
            StateObject state = new StateObject();
            state.workSocket = mServerSocket;

            while (mServerSocket.Connected)
            {
                try
                {
                    mServerSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(ReadCallback), state);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }

                Thread.Sleep(CLIENT_THREAD_TIMEOUT);
            }

            EndServerConnection();
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
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

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
                        // There  might be more data, so store the data received so far.
                        byte[] oldData = state.data;
                        if (null != oldData)
                        {
                            int newLength = bytesRead + state.dataCount;
                            state.data = new byte[newLength];
                            Array.Copy(oldData, 0, state.data, 0, state.dataCount);
                            Array.Copy(state.buffer, 0, state.data, state.dataCount, bytesRead);
                            state.dataCount = newLength;
                        }
                        else
                        {
                            state.data = new byte[bytesRead];
                            Array.Copy(state.buffer, 0, state.data, 0, bytesRead);
                            state.dataCount = bytesRead;
                        }

                        // Check for end-of-file tag. If it is not there, read 
                        // more data.
                        if (MessageHelper.FindEOM(state.data) > -1)
                        {
                            // All the data has been read from the 
                            // client. Display it on the console.
                            Console.WriteLine("Read {0} bytes from socket. \n Data : {2}",
                                state.data.Length, state.data.ToString());

                            byte[] dataMsg;
                            MessageHelper.RemoveEOM(state.data, out dataMsg);

                            // Do something with client message
                            MessageHandler.HandleMessage(dataMsg);
                        }
                        else
                        {
                            // Not all data received. Get more.
                            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReadCallback), state);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
    }
}
