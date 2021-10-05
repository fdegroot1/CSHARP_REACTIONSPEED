using System;
using System.Net;
using System.Net.Sockets;

namespace ReactionSpeed_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Communication communication = new Communication(new TcpListener(IPAddress.Any, 5555));
            communication.Start();
            
            while (true)
            {

            }
        }
    }
}
