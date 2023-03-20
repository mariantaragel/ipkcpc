using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ipkcpc;

internal class Tcp
{
    public string Host;
    public int Port;
    private Socket? _socket;
    private NetworkStream? _stream;
    private StreamReader? _reader;

    public Tcp(string host, int port)
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
            _socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(address, Port);

            // Establish a connection with a server
            _socket.Connect(endPoint);

            // Create a stream
            _stream = new NetworkStream(_socket);
            _reader = new StreamReader(_stream, Encoding.UTF8);

            string message;
            do
            {
                SendMessage();
                message = ReceiveMessage();
            } while (message != "BYE");

            FreeResources();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void SendMessage()
    {
        string input = GetInput();
        input += "\n";
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        _stream?.Write(inputBytes);
    }

    private string ReceiveMessage()
    {
        string message = _reader?.ReadLine() ?? string.Empty;
        Console.WriteLine(message);
        return message;
    }

    private string GetInput()
    {
        string input = Console.ReadLine() ?? string.Empty;

        return input;
    }

    private void CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        // Send a bye message
        byte[] closingMessage = Encoding.ASCII.GetBytes("BYE\n");
        _stream?.Write(closingMessage);
        Console.WriteLine("BYE");
        string? answer = _reader?.ReadLine();
        Console.Write(answer);

        // End application
        FreeResources();
        Environment.Exit(0);
    }

    private void FreeResources()
    {
        using (_stream)
        using (_socket)
        using (_reader)
        {
            _socket?.Close();
            _stream?.Close();
            _reader?.Close();
        }
    }
}