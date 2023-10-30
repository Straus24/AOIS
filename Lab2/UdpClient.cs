using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab2
{
    class Client
    {
        private const int Port = 2403;

        public static async Task SendRequest(string request)
        {
            UdpClient udpClient = new();
            byte[] requestBytes = Encoding.UTF8.GetBytes(request);

            await udpClient.SendAsync(requestBytes, requestBytes.Length, new IPEndPoint(IPAddress.Loopback, Port));

            var result = await udpClient.ReceiveAsync();
            string response = Encoding.UTF8.GetString(result.Buffer);

            Console.WriteLine(response);
        }
    }
}