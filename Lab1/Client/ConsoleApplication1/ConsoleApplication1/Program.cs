using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace timeClient
{
    class timeClientProgram
    {
        private const int listenPort = 50255;
        public static void StartListen()
        {
            Socket receiverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint recEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
            receiverSocket.Bind(recEndPoint);
            var EndPoint = recEndPoint as EndPoint;
            try
            {
                while (true)
                {
                    byte[] byteBuffer = new byte[1024];
                    receiverSocket.ReceiveFrom(byteBuffer, ref EndPoint);
                    Console.Clear();
                    Console.WriteLine("Server time: {0} ", Encoding.ASCII.GetString(byteBuffer, 0, byteBuffer.Length));
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
