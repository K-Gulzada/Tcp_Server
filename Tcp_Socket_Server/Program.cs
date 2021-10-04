using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tcp_Socket_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            //host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            try
            {
                Socket server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                server.Bind(localEndPoint);
                server.Listen(10);
                Socket handler = server.Accept();

                string data = null;
                while (true)
                {
                                      
                    byte[] bytes = new byte[1024];

                    int bytesRec = handler.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    if (data.IndexOf("<eof>") > -1)
                    {
                        break;
                    }
                   
                }
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
