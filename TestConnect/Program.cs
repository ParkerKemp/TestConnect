using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry("mc.spinalcraft.com");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8765);

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(remoteEP);
            byte[] message = Encoding.ASCII.GetBytes("Hello World!");
            sock.Send(message);
            sock.Shutdown(SocketShutdown.Both);
            sock.Close();
        }
    }
}
