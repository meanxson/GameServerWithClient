using System;
using System.Net.Sockets;

public class TCP
{
    private TcpClient _socket;
    private NetworkStream _stream;
    private byte[] _receivedBuffer;

    public void Connect()
    {
        _socket = new TcpClient()
        {
            ReceiveBufferSize = Client.Instance.DataBufferSize,
            SendBufferSize =  Client.Instance.DataBufferSize
        };

        _receivedBuffer = new byte[Client.Instance.DataBufferSize];

        _socket.BeginConnect(Client.Instance.IP, Client.Instance.Port, ConnectCallBack, _socket);
    }

    private void ConnectCallBack(IAsyncResult result)
    {
        _socket.EndConnect(result);

        if (!_socket.Connected)
            return;

        _stream = _socket.GetStream();
        _stream.BeginRead(_receivedBuffer, 0, Client.Instance.DataBufferSize, ReceivedCallBack, null);
    }

    private void ReceivedCallBack(IAsyncResult ar)
    {
        try
        {

        }
        catch (Exception)
        {
            //TODO: Disconnect
        }
    }
}