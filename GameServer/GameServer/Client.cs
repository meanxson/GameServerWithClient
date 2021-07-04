using System;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    public class Client
    {
        public static int DataBufferSize = 4096;
        
        public int Id;
        public TCP Tcp;

        public Client(int id)
        {
            Id = id;
            Tcp = new TCP(Id);
        }
        
        public class TCP
        {
            public TcpClient Socket;
        
            private readonly int _id;
            private NetworkStream _stream;
            private byte[] _receiveBuffer;
        
            public TCP(int id)
            {
                _id = id;
            }

            public void Connect(TcpClient socket)
            {
                Socket = socket;
                Socket.ReceiveBufferSize = dataBufferSize;
                Socket.SendBufferSize = dataBufferSize;

                _stream = socket.GetStream();

                _receiveBuffer = new byte[dataBufferSize];

                _stream.BeginRead(_receiveBuffer, 0, dataBufferSize, ReceiveCallBack, null);
            }

            private void ReceiveCallBack(IAsyncResult result)
            {
                try
                {
                    int byteLenght = _stream.EndRead(result);
                    if (byteLenght <= 0)
                    {
                        //TODO: Disconnect
                        return;
                    }

                    byte[] _data = new byte[byteLenght];
                    Array.Copy(_receiveBuffer,_data, byteLenght);
                    _stream.BeginRead(_receiveBuffer, 0, dataBufferSize, ReceiveCallBack, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error receiving TCP data {e}");
                    //TODO: Disconnect
                }
            }
        }
        
       
    }
    
   

}
