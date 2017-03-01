using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace timeServer
{
    class timeServerProgram
    {
        private const int broadcastPort = 50255;
        public static void StartBroadcasting()
        {
            Socket senderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            senderSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            IPEndPoint sendEndPoint = new IPEndPoint(IPAddress.Broadcast, broadcastPort);
            senderSocket.Connect(sendEndPoint);
            while (true)
            {                
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("Server is running");
                string currentTime = DateTime.Now.ToLongTimeString();
                byte[] sendBuffer = Encoding.ASCII.GetBytes(currentTime);
                senderSocket.Send(sendBuffer);                
                Console.WriteLine("Current time: {0}", currentTime);                
            }
        }
        
        static void Main(string[] args)
        {
            StartBroadcasting();            
        }
    }
}
