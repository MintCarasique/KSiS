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
        private int listenPort = 12205;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.startListen();           
        }
        public void startListen()
        {
            IPEndPoint EndPoint;
            IPHostEntry hostEntry = Dns.GetHostEntry("");
            IPAddress[] ipv4Addresses = Array.FindAll(hostEntry.AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
            //throw new NoIpException();
            if (ipv4Addresses.Length > 1)
            {
                Console.WriteLine("Choose IP-Address:");
                for (int i = 0; i < ipv4Addresses.Length; i++)
                    Console.WriteLine((i + 1).ToString() + "." + ipv4Addresses[i]);
                var choice = int.Parse(Console.ReadLine());
                EndPoint = new IPEndPoint(ipv4Addresses[choice - 1], listenPort);
                Console.WriteLine("Your IP: {0}", ipv4Addresses[choice - 1]);
            }
            else
            {
                EndPoint = new IPEndPoint(ipv4Addresses[0], listenPort);
                Console.WriteLine("Your IP: {0}", ipv4Addresses[0]);
            }
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(EndPoint);
                socket.Listen(10);

                Console.WriteLine("Сервер запущен.");

                while (true)
                {
                    Socket handler = socket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] receiving_data = new byte[256];

                    do
                    {
                        bytes = handler.Receive(receiving_data);
                        builder.Append(Encoding.Unicode.GetString(receiving_data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " : " + builder.ToString());

                    string message = builder.ToString(); //перестановка строки
                    string[] substrings = message.Split(' ');
                    Array.Reverse(substrings);
                    message = String.Join(" ", substrings);

                    byte[] sending_data = Encoding.Unicode.GetBytes(message);
                    handler.Send(sending_data);


                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }

            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                for (;;) ;
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
            string message = str;
            string[] substrings = message.Split(' ');
            Array.Reverse(substrings);
            message = String.Join(" ", substrings);
            return message;
        }
    }
}
