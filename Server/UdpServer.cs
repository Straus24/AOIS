using System.Net.Sockets;
using System.Text;
using NLog;

namespace UdpServer
{
    public class Server
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const int Port = 2403;
        private const string FileName = $"D:\\ПРОГРАММИРОВАНИЕ ТПУ\\Лабораторные работы C#\\3 Курс\\Architecture Korovkin\\Lab1\\Server\\Music_info.csv";
        private static List<Song> songs = new();
        static async Task Main(string[] args)
        {
            using UdpClient udpClient = new(Port);
            songs = ReadData();

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
        private static List<Song> ReadData()
        {

            if (File.Exists(FileName))
            {
                using StreamReader reader = new(FileName);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');

                    if (parts.Length == 6)
                    {
                        Song song = new()
                        {
                            Name = parts[0].Trim(),
                            Author = parts[1].Trim(),
                            Genre = parts[2].Trim(),
                            Year_Of_Release = int.Parse(parts[3]),
                            Rating_Number = int.Parse(parts[4]),
                        };
                        songs.Add(song);
                    }
                }
            }

            return songs;
        }

        private static string ProcessRequest(string request)
        {
            string[] parts = request.Split(';');
            string command = parts[0];
            switch (command)
            {
                case "1":
                    return GetAllSongs();
                case "2":
                    try
                    {
                        return GetSong(int.Parse(parts[1]) - 1);
                    }
                    catch (Exception)
                    {

                        return $"{parts[1]} не является номером!";
                    }
                case "3":
                    try
                    {
                        DeleteSong(int.Parse(parts[1]) - 1);
                        return "Песня удалена.";
                    }
                    catch (Exception)
                    {

                        return $"{parts[1]} не является номером!";
                    }

                case "4":
                    try
                    {
                        AddSong(parts[1], parts[2], parts[3], int.Parse(parts[4]), int.Parse(parts[5]));
                        return "Песня добавлена.";
                    }
                    catch (Exception)
                    {
                        return "Числовые данные введены неверно!";
                    }
                case "5":
                    return DeleteAllSongs();
                default:
                    return "Недопустимая команда.";
            }
        }

        private static string GetAllSongs()
        {
            StringBuilder builder = new();
            for (int i = 0; i < songs.Count; i++)
            {
                string songString = $"{i + 1}: {songs[i].Name} {songs[i].Author} {songs[i].Genre} {songs[i].Year_Of_Release} {songs[i].Rating_Number} {songs[i].Is_Top100}";
                builder.AppendLine(songString);
            }
            return builder.ToString();
        }

        private static string GetSong(int index)
        {
            if (index >= 0 && index < songs.Count)
            {
                return $"{index + 1}: {songs[index].Name} {songs[index].Author} {songs[index].Genre} {songs[index].Year_Of_Release} {songs[index].Rating_Number} {songs[index].Is_Top100}";
            }
            return "Недопустимый индекс.";
        }

        private static void DeleteSong(int index)
        {
            if (index >= 0 && index < songs.Count)
            {
                songs.RemoveAt(index);
                SaveData();
            }
        }

        private static string DeleteAllSongs()
        {
            File.WriteAllText(FileName, string.Empty);
            songs.Clear();
            return "Все песни удалены.";
        }

        private static void AddSong(string name, string author, string genre, int year_of_release, int rating_number)
        {
            songs.Add(new Song { Name = name, Author = author, Genre = genre, Year_Of_Release = year_of_release, Rating_Number = rating_number });
            SaveData();
        }

        private static void SaveData()
        {
            using var writer = new StreamWriter(FileName);
            foreach (Song song in songs)
            {
                string line = $"{song.Name} ; {song.Author} ; {song.Genre} ; {song.Year_Of_Release} ; {song.Rating_Number} ; {song.Is_Top100}";
                writer.WriteLine(line);
            }
        }

    }
}
