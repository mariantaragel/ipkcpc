using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ipkcpc
{
    internal class Client
    {
        public Client(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public string Host;
        public int Port;

        public void Tcp()
        {
            IPAddress address = IPAddress.Parse(Host);
            Socket socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(address, Port);

            socket.Connect(endPoint);
            NetworkStream stream = new NetworkStream(socket);

            // Text protocol
            TextWriter writer = new StreamWriter(stream);
            writer.WriteLine("Hello");

            TextReader reader = new StreamReader(stream);
            var message = reader.ReadLine();

            Console.WriteLine(message);
        }

        public void Udp()
        {
            IPAddress address = IPAddress.Parse(Host);
            Socket socket = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            EndPoint endPoint = new IPEndPoint(address, Port);

            byte[] recBuffer = new byte[1024];
            byte[] sendBuffer = GetRequestMessage();

            // Send data to the server
            socket.SendTo(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, endPoint);

            // Receive data from the server
            socket.ReceiveFrom(recBuffer, 0, recBuffer.Length, SocketFlags.None, ref endPoint);

            Console.WriteLine(Encoding.UTF8.GetString(recBuffer));
        }

        private static byte[] GetRequestMessage()
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
        static void Main(string[] args)
        {
            string host = args[1];
            int port = int.Parse(args[3]);
            string mode = args[5];

            Client client = new Client(host, port);

            if (mode == "tcp")
            {
                client.Tcp();
            }
            else if (mode == "udp")
            {
                client.Udp();
            }
        }
    }
}