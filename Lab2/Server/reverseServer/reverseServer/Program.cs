using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace reverseServer
{
    class Program
    {
        private Socket socket;
        private int listenPort = 50255;
        static void Main(string[] args)
        {
                        
        }
        public void startListen()
        {
            IPEndPoint EndPoint = new IPEndPoint(IPAddress.Any, listenPort);
            socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(EndPoint);
            socket.Listen(10);
            Console.WriteLine("Server started!");
        }
    }
}
