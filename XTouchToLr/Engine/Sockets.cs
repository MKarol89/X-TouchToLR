using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XTouchToLr.Data;

namespace XTouchToLr.Engine
{
    public static class Sockets
    {
                
        private static IPAddress ipAddress2 = IPAddress.Parse("127.0.0.1");

        private static Socket sender2 = new Socket(ipAddress2.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

        public static void StartUploadClient()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                string LocalHostName = Dns.GetHostName();

                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPEndPoint remoteEP = new IPEndPoint(ipAddress2, 53164);

                // Create a TCP/IP  socket.  
                

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender2.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender2.RemoteEndPoint.ToString());

                    


                    // Receive the response from the remote device.  


                    // Release the socket.  
                    // sender.Shutdown(SocketShutdown.Both);
                    //sender.Close();

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

        public static void sendToLr(string key, string value)
        {
            // Encode the data string into a byte array.  
            byte[] msg = Encoding.ASCII.GetBytes(key+" = " + value.Replace(',', '.') + "\n");

            // Send the data through the socket.  
            int bytesSent = sender2.Send(msg);
        }

        public static void StartClient()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            
            
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                string LocalHostName = Dns.GetHostName();

                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 51761);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                
                
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    //byte[] msg = Encoding.ASCII.GetBytes("select = next /n");

                    // Send the data through the socket.  
                    //int bytesSent = sender.Send(msg);

                    while (true)
                    {
                        int bytesRec = sender.Receive(bytes);
                        //Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                        //Console.WriteLine("==================================================");


                        //Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRec));

                        var str = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        

                        str = str.Replace("\r", "");
                        str = str.Replace("\n", "");
                        str = str.Replace("\t", "");
                        str = str.Replace("\0", "");
                        str = str.Replace("\"", "");
                        str = str.Replace("}", "");


                        GlobalSettings.strSplit = str.ToString().Split('{');

                        if (GlobalSettings.strSplit != null)
                        {
                            LrParameters.SetParameters(GlobalSettings.strSplit);
                        }

                    }

                    // Receive the response from the remote device.  


                    // Release the socket.  
                    // sender.Shutdown(SocketShutdown.Both);
                    //sender.Close();

                
                //catch (ArgumentNullException ane)
                //{
                //    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                //}
                //catch (SocketException se)
                //{
                //    Console.WriteLine("SocketException : {0}", se.ToString());
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                //}

            
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}
        }
    }
}
