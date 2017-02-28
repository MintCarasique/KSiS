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
        public static void StartBroadcasting()
        {
            Socket senderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            senderSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            IPEndPoint endP = new IPEndPoint(IPAddress.Broadcast, 12209);
            senderSocket.Connect(endP);
            while (true)
            {
                
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("Server is running");
                string currentTime = DateTime.Now.ToLongTimeString();
                byte[] sendbuf = Encoding.ASCII.GetBytes(currentTime);
                senderSocket.Send(sendbuf);                
                Console.WriteLine("Current time: {0}", currentTime);                
            }
        }
        
        static void Main(string[] args)
        {
            StartBroadcasting();            
        }
    }
}
