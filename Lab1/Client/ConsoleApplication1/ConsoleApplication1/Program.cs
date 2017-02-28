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
            var listener = new UdpClient(listenPort);
            var groupEndPoint = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                while (!done)
                {
                    //Console.WriteLine("I'm client.");
                    byte[] bytes = listener.Receive(ref groupEndPoint);
                    Console.Clear();
                    Console.WriteLine("Current time by server: {0} ", Encoding.ASCII.GetString(bytes, 0, bytes.Length));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }
        static void Main(string[] args)
        {
            StartListen();

        }
    }
}
