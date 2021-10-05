using System;
using System.Net.Sockets;
using Utils;

public class Client
{
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[1024];
    private byte[] totalbuffer = new byte[1024];
    private int totalBufferReceived = 0;
    private string username;

    public Client() : this("localhost", 5555)
    {
    }

    public Client(string adress, int port)
    {
        this.client = new TcpClient();
        this.client.BeginConnect(adress, port, new AsyncCallback(OnConnect), null);

    }

    private void OnConnect(IAsyncResult ar)
    {
        this.client.EndConnect(ar);
        this.stream = this.client.GetStream();
        this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
    }

    private void OnRead(IAsyncResult ar)
    {
        if (ar == null || (!ar.IsCompleted) || (!this.stream.CanRead))
        {
            return;
        }

        int receivedBytes = this.stream.EndRead(ar);

        if(totalBufferReceived + receivedBytes > 1024)
        {
            throw new OutOfMemoryException("Buffer overflow");
        }

        Array.Copy(buffer, 0, totalbuffer, totalBufferReceived, receivedBytes);
        totalBufferReceived += receivedBytes;

        int expectedMessageLenght = BitConverter.ToInt32(totalbuffer, 0);

        while(totalBufferReceived >= expectedMessageLenght)
        {
            byte[] messageBytes = new byte[expectedMessageLenght];
            Array.Copy(totalbuffer, 0, messageBytes, 0, expectedMessageLenght);

            string identifier = DataParser.getJsonIdentifier(messageBytes);

            switch(identifier)
            {
                case DataParser.LOGIN:
                    break;
                case DataParser.SAVEHIGHSCORE:
                    break;
                case DataParser.GETHIGHSCORELIST:
                    break;
                case DataParser.DISCONNECT:
                    break;

            }
        }

        this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
    }

    public void Login(string username)
    {
        byte[] message = DataParser.getMessage(DataParser.loginJSON(username));
        this.stream.BeginWrite(message, 0, message.Length, new AsyncCallback(OnWrite), null);
    }

    private void OnWrite(IAsyncResult ar)
    {
        this.stream.EndWrite(ar);
    }
}