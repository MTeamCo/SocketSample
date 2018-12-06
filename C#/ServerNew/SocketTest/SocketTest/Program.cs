using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketTest
{
    class Program
    {

        static void Main(string[] args)
        {
            AsyncSocket.StartListening();
            Console.ReadKey();
        }

    }
}
