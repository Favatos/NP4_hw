using System.Net;
using System.Net.Sockets;
using TcpLib;

namespace Server;

internal class Program
{
    static async Task Main(string[] args)
    {
        using TcpListener listener = new TcpListener(IPAddress.Any, 2025);
        listener.Start();
        Console.WriteLine($"Server is running on {listener.Server.LocalEndPoint}");

        while (true)
        {
            using TcpClient client = await listener.AcceptTcpClientAsync();
            Console.WriteLine($"The client {client.Client.RemoteEndPoint} has connected to the server");

            string title = Path.GetFileName(await client.ReceiveStringAsync());
            using Stream stream = File.Create(title);
            await client.ReceiveFileAsync(stream);
        }
    }
}
