using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    class Program
    {
        static Socket sktServer;
        static void Main(string[] args)
        {
            sktServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localAd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31001);
            sktServer.Bind(localAd);
            sktServer.Listen(100);
            Console.WriteLine("Socket !");
            sktServer = sktServer.Accept();

            Byte[] buffer = new byte[500];
            sktServer.Receive(buffer);
            string Data = Encoding.ASCII.GetString(buffer);
            Console.WriteLine(Data);

            Console.ReadKey();
        }
    }
}
