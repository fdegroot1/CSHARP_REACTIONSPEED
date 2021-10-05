using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ReactionSpeed_Server
{
    class Communication
    {
        private TcpListener listener;
        private List<TcpClient> clients;

        public Communication(TcpListener listener)
        {
            this.listener = listener;
            this.clients = new List<TcpClient>();
        }

        public void Start()
        {
            this.listener.Start();
            Console.WriteLine($"==========================================================================\n" +
                $"\tstarted accepting clients at {DateTime.Now}\n" +
                $"==========================================================================");
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        private void OnConnect(IAsyncResult ar)
        {
            var tcpClient = listener.EndAcceptTcpClient(ar);
            Console.WriteLine($"Client connected from {tcpClient.Client.RemoteEndPoint}");

            this.clients.Add(new Client(this, tcpClient));
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        public void RemoveClient(Client client)
        {
            clients.Remove(client);
        }

    }
}
