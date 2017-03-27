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
            Program program = new Program();
            program.startListen();           
        }
        public void startListen()
        {
            IPEndPoint EndPoint = new IPEndPoint(IPAddress.Any, listenPort);
            socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(EndPoint);
            socket.Listen(10);
            Console.WriteLine("Server started!");
            while (true)
            {
                ProcessingClientData();
            }
        }
        public void ProcessingClientData()
        {
            Socket handler = socket.Accept();
            byte[] buffer = new byte[1024];
            int byterec = handler.Receive(buffer);
            string str;
            str = StringReverse(Encoding.UTF8.GetString(buffer, 0, byterec));
            handler.Send(Encoding.UTF8.GetBytes(str));
            if (Encoding.UTF8.GetString(buffer, 0, byterec).IndexOf("<TheEnd>") > -1)
            {
                Console.WriteLine("MessageProcessed");
            }
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
        public string StringReverse(string str)
        {
            string message = str.ToString();
            string[] substrings = message.Split(' ');
            Array.Reverse(substrings);
            message = String.Join(" ", substrings);
            return message;
        }
    }
}
