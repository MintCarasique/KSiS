using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace timeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket currentSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            currentSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            IPEndPoint endP = new IPEndPoint(IPAddress.Broadcast, 12209);
            currentSocket.Connect(endP);

            Console.WriteLine("Server is working!");
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                string curTime = DateTime.Now.ToLongTimeString();
                byte[] sendbuf = Encoding.ASCII.GetBytes(curTime);
                currentSocket.Send(sendbuf);
            }
        }
    }
}
