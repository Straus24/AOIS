using System.Net.Sockets;
using System.Text;
using NLog;

namespace UdpServer
{
    public class Server
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const int Port = 2403;
        
        private static DataController dataController = new DataController();
        static async Task Main(string[] args)
        {
            using UdpClient udpClient = new(Port);

            logger.Info($"Server started. Listening on port {Port}");

            while (true)
            {
                var result = await udpClient.ReceiveAsync();
                string request = Encoding.UTF8.GetString(result.Buffer);

                logger.Info($"Received request from {result.RemoteEndPoint}: {request}");

                string response = ProcessRequest(request);
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                _ = udpClient.SendAsync(responseBytes, responseBytes.Length, result.RemoteEndPoint);

                logger.Info($"Sent response to {result.RemoteEndPoint}: {response}");
            }
        }

        private static string ProcessRequest(string request)
        {
            string[] parts = request.Split(';');
            string command = parts[0];
            string resultString = request.Replace(command + ";", string.Empty);
            switch (command)
            {
                case "1":
                    return GetAllSongs();
                case "2":
                    dataController.DeleteDB();
                    AddSongs(resultString);  
                    return "Песни изменены.";
                default:
                    return "Недопустимая команда.";
            }
        }

        private static string GetAllSongs()
        {
            StringBuilder builder = new();
            for (int i = 0; i < dataController.GetSongs().Count; i++)
            {
                string songString = $"{dataController.GetSongs()[i].Id};{dataController.GetSongs()[i].Name};{dataController.GetSongs()[i].Author};{dataController.GetSongs()[i].Genre};" +
                                    $"{dataController.GetSongs()[i].Year_Of_Release};{dataController.GetSongs()[i].Rating_Number};{dataController.GetSongs()[i].Is_Top100}";
                builder.AppendLine(songString);
            }
            return builder.ToString();
        }

        private static void AddSong(string name, string author, string genre, int year_of_release, int rating_number)
        {
            dataController.AddSong(new Song { Name = name, Author = author, Genre = genre, Year_Of_Release = year_of_release, Rating_Number = rating_number });
        }

        private static void AddSongs(string requestString)
        {
            foreach(string line in requestString.Split(new char[] { '\n' }))
            {
                if (String.IsNullOrEmpty(line))
                {

                }
                else
                {
                    string str = line.ToString();
                    string[] parts = str.Split(';');
                    AddSong(parts[0], parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]));
                } 
            }
        }
    }
}
