using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Tcp_File_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                server = new TcpListener(IPAddress.Parse("192.168.110.57"), 11000);
                server.Start();
                Console.WriteLine("Ожидаем прием файла...");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    byte[] bytes = new byte[1024];

                    var stream = client.GetStream();

                    do
                    {
                        stream.Read(bytes, 0, bytes.Length);
                    } while (stream.DataAvailable);

                    string fileName = string.Format("{0}.jpeg", Guid.NewGuid().ToString());
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    Console.WriteLine("Файл {0} принят и сохранен! ", fileName);
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
