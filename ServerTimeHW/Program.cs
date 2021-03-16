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
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 57650));
            Console.WriteLine($"{NetHelper.UsefulThings.GetPublicIpAddress()}:57650");
            socket.Listen(10);
            while (true)
            {
                Socket temp = socket.Accept();
                Data data = NetHelper.TcpSocketHelper.Receive(temp);
                if (data.GetString() == "d")
                {
                    temp.Send(NetHelper.UsefulThings.ToBytes(DateTime.Now.Date.ToShortDateString()));
                    Console.WriteLine("Date Sent!");
                }
                else if (data.GetString() == "t")
                {
                    temp.Send(NetHelper.UsefulThings.ToBytes(DateTime.Now.TimeOfDay.ToString()));
                    Console.WriteLine("Time Sent!");
                }
                else
                {
                    temp.Send(NetHelper.UsefulThings.ToBytes(DateTime.Now.ToString(CultureInfo.InvariantCulture)));
                    Console.WriteLine("Full Sent!");
                }

                temp.Shutdown(SocketShutdown.Both);
                
            }
        }
    }
}
