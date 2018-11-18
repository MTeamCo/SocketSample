using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket
{
    class Program
    {
        public class GetSocket
        {
            private static System.Net.Sockets.Socket ConnectSocket(string server, int port)
            {
                port = 8412;
                System.Net.Sockets.Socket s = null;
                IPHostEntry hostEntry = null;

                // Get host related information.
                hostEntry = Dns.GetHostEntry(server);

                // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
                // an exception that occurs when the host IP Address is not compatible with the address family
                // (typical in the IPv6 case).
                foreach (IPAddress address in hostEntry.AddressList)
                {
                    IPEndPoint ipe = new IPEndPoint(address, port);
                    System.Net.Sockets.Socket tempSocket =
                        new System.Net.Sockets.Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    tempSocket.Bind(ipe);
                    tempSocket.Connect(ipe);

                    if (tempSocket.Connected)
                    {
                        s = tempSocket;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                return s;
            }

            // This method requests the home page content for the specified server.
            private static string SocketSendReceive(string server, int port)
            {
                try
                {
                    port = 8412;
                    string request = "GET / HTTP/1.1\r\nHost: " + server +
                                     "\r\nConnection: Close\r\n\r\n";
                    Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
                    Byte[] bytesReceived = new Byte[256];

                    // Create a socket connection with the specified server and port.

                    System.Net.Sockets.Socket s = ConnectSocket(server, port);
                        if (s == null)
                            return ("Connection failed");

                        // Send request to the server.
                        s.Send(bytesSent, bytesSent.Length, 0);

                        // Receive the server home page content.
                        int bytes = 0;
                        string page = "1";

                        // The following will block until the page is transmitted.
                        do
                        {
                            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                            page =  Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                        }
                        while (bytes > 0);
                        return "1";
                    }
                
                catch (Exception e)
                {
                    return "oh Exception: "+e.Message;
                }
               

                
            }

            public static void Main(string[] args)
            {
                string host="localhost";
                int port = 10500;

                if (args.Length == 0)
                    // If no server name is passed as argument to this program, 
                    // use the current host name as the default.
                    host = Dns.GetHostName();
                else
                    host = args[0];

                string result = SocketSendReceive(host, port);
                Console.WriteLine(result);
            }
        }

    }
}
