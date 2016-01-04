using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using GCMessaging;

namespace GemCarryClient
{
    class Program
    {
        public static void StartClient()
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try {
                // Establish the remote endpoint for the socket.
                IPAddress ipAddress = IPAddress.Parse("52.34.24.170");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress,1025);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Stream, ProtocolType.Tcp );

                // Connect the socket to the remote endpoint. Catch any errors.
                try {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new MemoryStream();
                    byte[] compressed;

                    // TODO: Change to message queue system
                    LoginMessage outMsg = new LoginMessage();
                    outMsg.mUsername = "test@test.com";
                    outMsg.mPassword = "test";
/*
                    CreateUserMessage userMsg = new CreateUserMessage();
                    userMsg.mUsername = "test@test.com";
                    userMsg.mPassword = "test1";
                    userMsg.mPasswordValidate = "test";
                    */
                    formatter.Serialize(stream, outMsg);
                    formatter.Serialize(stream, userMsg);

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

                    // Send the data through the socket.
                    Console.WriteLine("Sending message:" + Encoding.ASCII.GetString(msg));
                    int bytesSent = sender.Send(msg);


                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes,0,bytesRec));

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                } catch (ArgumentNullException ane) {
                    Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
                } catch (SocketException se) {
                    Console.WriteLine("SocketException : {0}",se.ToString());
                } catch (Exception e) {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            } catch (Exception e) {
                Console.WriteLine( e.ToString());
            }
        }


        static void Main(string[] args)
        {
            StartClient();
        }
    }
}
