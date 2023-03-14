﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ipkcpc;

public class Udp
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
            while (true)
            {
                var endPoint = CreateEndPoint();

                byte[] recBuffer = new byte[1024];
                byte[] sendBuffer = GetUdpRequestMessage();

                // Send data to the server
                _socket?.SendTo(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, endPoint);

                // Receive data from the server
                _socket?.ReceiveFrom(recBuffer, 0, recBuffer.Length, SocketFlags.None, ref endPoint);

                switch (recBuffer[1])
                {
                    case 0:
                        Console.Write("OK:");
                        break;
                    case 1:
                        Console.Write("ERR:");
                        break;
                }

                PrintUdpResponseMessage(recBuffer);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public EndPoint CreateEndPoint()
    {
        IPAddress address = IPAddress.Parse(Host);
        _socket = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        EndPoint endPoint = new IPEndPoint(address, Port);
        return endPoint;
    }

    private void CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        using (_socket)
        {
            _socket?.Close();
        }
        Environment.Exit(0);
    }

    public void PrintUdpResponseMessage(byte[] recBuffer)
    {
        byte[] answer = new byte[recBuffer.Length - 3];
        for (int i = 3; i < recBuffer.Length; i++)
        {
            answer[i - 3] = recBuffer[i];
        }
        Console.WriteLine(Encoding.UTF8.GetString(recBuffer));
    }

    public byte[] GetUdpRequestMessage()
    {
        string input = Console.ReadLine() ?? string.Empty;
        byte[] message = Encoding.ASCII.GetBytes(input);
        byte[] buffer = new byte[message.Length + 2];
        buffer[0] = 0;
        buffer[1] = (byte)buffer.Length;
        for (int i = 2; i < buffer.Length; i++)
        {
            buffer[i] = message[i - 2];
        }
        return buffer;
    }
}