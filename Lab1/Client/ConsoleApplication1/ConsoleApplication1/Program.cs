using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace timeClient
{
    class Program
    {
        private const int listenPort = 12209;

        public static void StartListen()
        {
            bool done = false;
            Socket receiverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            receiverSocket.Bind(groupEP);
            var EP = groupEP as EndPoint;
            try
            {
                while (!done)
                {
                    byte[] bytes = new byte[1024];
                    receiverSocket.ReceiveFrom(bytes, ref EP);
                    Console.Clear();
                    Console.WriteLine("Server time: {0} ", Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                receiverSocket.Close();
            }
        }
        static void Main(string[] args)
        {
            StartListen();

        }
    }
}
