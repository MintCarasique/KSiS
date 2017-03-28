using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace reverseClient
{
    class clientTCP
    {
        private string message;
        private int sendingPort = 50255;
        public clientTCP(string message)
        {
            this.message = message;
        }
        public string SendMessage()
        {
            byte[] buffer = new byte[1024];
            IPHostEntry HostEntry = Dns.GetHostEntry("");
            IPAddress[] ipv4Adresses = Array.FindAll(HostEntry.AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);            
            IPEndPoint EndPoint = new IPEndPoint(ipv4Adresses[0], sendingPort);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(EndPoint);
            string SendingMessage = message;
            byte[] byteMessage = Encoding.UTF8.GetBytes(SendingMessage);
            int byteSent = socket.Send(byteMessage);
            int byteReceived = socket.Receive(buffer);
            message = Encoding.UTF8.GetString(buffer, 0, byteReceived);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return message; 
        }
    }
}