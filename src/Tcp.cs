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
            IPAddress address = IPAddress.Parse(Host);
            _socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(address, Port);

            _socket.Connect(endPoint);
            _stream = new NetworkStream(_socket);

            // Text protocol
            _reader = new StreamReader(_stream, Encoding.UTF8);

            string? message = string.Empty;
            while (message != "BYE")
            {
                string input = Console.ReadLine() ?? string.Empty;
                input += "\n";
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                _stream.Write(inputBytes);
                message = _reader.ReadLine();
                Console.WriteLine(message);
            }

            using (_stream)
            using (_socket)
            using (_reader)
            {
                _socket.Close();
                _stream.Close();
                _reader.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        byte[] closingMessage = Encoding.ASCII.GetBytes("BYE\n");
        _stream?.Write(closingMessage);
        Console.WriteLine("BYE");
        string? answer = _reader?.ReadLine();
        Console.Write(answer);

        using (_stream)
        using (_socket)
        using (_reader)
        {
            _stream?.Close();
            _socket?.Close();
            _reader?.Close();
        }

        Environment.Exit(0);
    }
}