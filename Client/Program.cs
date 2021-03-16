using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetHelper;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = NetHelper.TcpSocketHelper.CreateSocket();
            Console.Write("IP: ");
            IPAddress ip = IPAddress.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write("PORT: ");
            int port = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Date ('d') or Time ('t') or Full ('f')");
            string choice;
            while (true)
            {
                choice = Console.ReadLine();
                if (choice.ToLower() == "d" || choice.ToLower() == "t" || choice.ToLower() == "f") break;
            }

            socket.Connect(new IPEndPoint(ip,port));
            socket.Send(NetHelper.UsefulThings.ToBytes(choice));
            Data data = NetHelper.TcpSocketHelper.Receive(socket);
            Console.WriteLine(data.GetString());
            Console.Read();
        }
    }
}