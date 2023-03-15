using System.CommandLine;

namespace ipkcpc;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Parse command-line arguments
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

        // Create instance of TCP or UDP client
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