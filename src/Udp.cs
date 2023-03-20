using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ipkcpc;

internal class Udp
{
    public string Host;
    public int Port;
    private Socket? _socket;

    public Udp(string host, int port)
    {
        Host = host;
        Port = port;
    }

    public void Communicate()
    {
        Console.CancelKeyPress += CancelKeyPress;

        try
        {
            // Create IP endpoint from server IP address and port
            IPAddress address = IPAddress.Parse(Host);
            _socket = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            EndPoint endPoint = new IPEndPoint(address, Port);

            while (true)
            {
                // Buffers to send and receive messages
                byte[] recBuffer = new byte[1024];
                byte[] sendBuffer = GetUdpRequestMessage();

                // Send data to the server
                _socket.SendTo(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, endPoint);

                // Receive data from the server
                _socket.ReceiveFrom(recBuffer, 0, recBuffer.Length, SocketFlags.None, ref endPoint);

                // Print server answer
                PrintStatusCode(recBuffer[1]);
                byte[] response = GetUdpResponseMessage(recBuffer);
                Console.WriteLine(Encoding.UTF8.GetString(response));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        FreeResources();
        Environment.Exit(0);
    }

    private void FreeResources()
    {
        using (_socket)
        {
            _socket?.Close();
        }
    }

    private static void PrintStatusCode(byte statusCode)
    {
        switch (statusCode)
        {
            case 0:
                Console.Write("OK:");
                break;
            case 1:
                Console.Write("ERR:");
                break;
        }
    }

    private static byte[] GetUdpResponseMessage(byte[] recBuffer)
    {
        // Remove IPK Calculator Protocol header
        byte[] answer = new byte[recBuffer[2]];
        for (int i = 3; i < recBuffer[2] + 3; i++)
        {
            answer[i - 3] = recBuffer[i];
        }
        return answer;
    }

    private byte[] GetUdpRequestMessage()
    {
        string input = Console.ReadLine() ?? string.Empty;
        if (input == string.Empty)
        {
            byte[] send = new byte[2];
            send[0] = 0;
            send[1] = (byte)send.Length;
            return send;
        }

        byte[] message = Encoding.ASCII.GetBytes(input);
        byte[] buffer = new byte[message.Length + 2];

        // Add IPK Calculator Protocol header
        buffer[0] = 0;
        buffer[1] = (byte)buffer.Length;

        for (int i = 2; i < buffer.Length; i++)
        {
            buffer[i] = message[i - 2];
        }

        return buffer;
    }
}
