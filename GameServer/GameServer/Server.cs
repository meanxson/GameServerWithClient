using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    public static class Server
    {
        public static int MaxPlayer { get; private set; }
        public static int Port { get; private set; }
        public static readonly Dictionary<int, Client> Clients = new Dictionary<int, Client>();
        
        private static TcpListener _tcpListener;

        public static void Start(int maxPlayer, int port)
        {
            MaxPlayer = maxPlayer;
            Port = port;

            Console.WriteLine("Server Turning On...");
            
            _tcpListener = new TcpListener(IPAddress.Any, Port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(TCPConnectCallBack, null);

            Console.WriteLine($"Server started on {Port}");
            InitializeServerData();
        }

        private static void TCPConnectCallBack(IAsyncResult result)
        {
            var client = _tcpListener.EndAcceptTcpClient(result);
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);
            Console.WriteLine($"Incoming connection from {client.Client.RemoteEndPoint}...");

            for (int i = 0; i <= MaxPlayer; i++)
            {
                if (Clients[i].Tcp.Socket == null)
                {
                    Clients[i].Tcp.Connect(client);
                    return;
                }

                Console.WriteLine($"{client.Client.RemoteEndPoint} failed to connect: Server full");
            }
        }

        private static void InitializeServerData()
        {
            for (var i = 0; i < MaxPlayer; i++)
            {
                Clients.Add(i, new Client(i));
            }
        }
    }
}
