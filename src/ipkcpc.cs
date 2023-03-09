using System.Net;
using System.Net.Sockets;
using System.Text;
using System.CommandLine;

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
            while (true)
            {
                IPAddress address = IPAddress.Parse(Host);
                _socket = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                EndPoint endPoint = new IPEndPoint(address, Port);

                byte[] recBuffer = new byte[1024];
                byte[] sendBuffer = GetUdpRequestMessage();

                // Send data to the server
                _socket.SendTo(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, endPoint);

                // Receive data from the server
                _socket.ReceiveFrom(recBuffer, 0, recBuffer.Length, SocketFlags.None, ref endPoint);

                switch (recBuffer[1])
                {
                    case 0:
                        Console.Write("OK:");
                        break;
                    case 1:
                        Console.Write("ERR:");
                        break;
                }

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
        using (_socket)
        {
            _socket?.Close();
        }
        Environment.Exit(0);
    }

    private static byte[] GetUdpResponseMessage(byte[] recBuffer)
    {
        byte[] answer = new byte[recBuffer.Length - 3];
        for (int i = 3; i < recBuffer.Length; i++)
        {
            answer[i - 3] = recBuffer[i];
        }

        return answer;
    }

    private static byte[] GetUdpRequestMessage()
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

internal class Program
{
    private static async Task Main(string[] args)
    {
        var hostOption = new Option<string>(
            name: "-h",
            description: "The IPv4 address of the server")
        {
            IsRequired = true,
            ArgumentHelpName = "host"
        };

        var portOption = new Option<int>(
            name: "-p",
            description: "The server port")
        {
            IsRequired = true,
            ArgumentHelpName = "port"
        };

        var modeOption = new Option<string>(
            name: "-m",
            description: "Application mode to use")
        { IsRequired = true }.FromAmong("udp", "tcp");

        var rootCommand = new RootCommand("Client for the IPK Calculator Protocol");
        rootCommand.AddOption(hostOption);
        rootCommand.AddOption(portOption);
        rootCommand.AddOption(modeOption);

        rootCommand.SetHandler((hostOptionValue, portOptionValue, modeOptionValue) =>
        {
            switch (modeOptionValue)
            {
                case "tcp":
                    {
                        Tcp clientTcp = new Tcp(hostOptionValue, portOptionValue);
                        clientTcp.Communicate();
                        break;
                    }
                case "udp":
                    {
                        Udp clientUdp = new Udp(hostOptionValue, portOptionValue);
                        clientUdp.Communicate();
                        break;
                    }
            }
        }, hostOption, portOption, modeOption);

        await rootCommand.InvokeAsync(args);
    }
}