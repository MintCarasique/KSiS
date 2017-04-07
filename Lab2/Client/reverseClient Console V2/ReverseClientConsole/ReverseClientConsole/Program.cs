using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ReverseClientConsole
{
    class Program
    {        
        public static void SendMessage()
        {
            string message;
            IPAddress serverIp;
            IPEndPoint EndPoint;
            int sendingPort = 12205;
            byte[] buffer = new byte[1024];
            IPHostEntry HostEntry = Dns.GetHostEntry("");
            IPAddress[] ipv4Addresses = Array.FindAll(HostEntry.AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
            EndPoint = new IPEndPoint(IPAddress.Parse("192.168.56.101") , sendingPort);
            
            try
            {
                while (true)
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(EndPoint);
                    message = Console.ReadLine();
                    string SendingMessage = message;
                    byte[] byteMessage = Encoding.UTF8.GetBytes(SendingMessage);
                    int byteReceived = socket.Receive(buffer);
                    Console.WriteLine("Received Message: ", Encoding.UTF8.GetString(buffer, 0, byteReceived));
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
            finally
            {
            }
        }
        static int port = 12205;
        static string ipAddress = "169.254.101.26";
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(ipPoint);
                    string message;
                    do
                    {
                        Console.WriteLine("Введите сообщение:");
                        message = Console.ReadLine();
                    }
                    while (message == "");
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);

                    data = new byte[256];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);

                    Console.WriteLine("Ответ сервера: " + builder.ToString());


                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                for (;;) ;
            }
        }        
    }
}

