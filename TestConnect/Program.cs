using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestConnect
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("TestConnect <address> <port> <message>");
                return 1;
            }
            string address = args[0];
            string message = args.Length > 2 ? args[2] : "Hello World!";
            int port;
            if(!int.TryParse(args[1], out port))
            {
                Console.WriteLine("TestConnect <address> <port> <message>");
                return 1;
            }

            Socket sock = connectTo(address, port);

            byte[] msgBytes = Encoding.ASCII.GetBytes(message);
            sock.Send(msgBytes);

            byte[] buffer = new byte[1024];
            sock.Receive(buffer);
            Console.WriteLine(System.Text.Encoding.Default.GetString(buffer));

            sock.Shutdown(SocketShutdown.Both);
            sock.Close();
            return 0;
        }

        static Socket connectTo(string address, int port)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(address);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(remoteEP);
            return sock;
        }
    }
}
