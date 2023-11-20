using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;

namespace WPF_MVVM
{
    class ViewModel_Songs
    {
        public ICommand LoadDataCommand { get; set; }
        public ICommand SaveDataCommand { get; set; }
        public ObservableCollection<Model_Song> Songs
        {
            get { return songs; }
            set
            {
                songs = value;
            }
        }
        Dictionary<int, Model_Song> songDictionary = new Dictionary<int, Model_Song>();
        private ObservableCollection<Model_Song> songs = new ObservableCollection<Model_Song>();
        private readonly int Port = 2403;
        private readonly string serverAddress = "127.0.0.1";
        private string? response;

        public ViewModel_Songs()
        {
            LoadDataCommand = new RelayCommand(LoadData);
            SaveDataCommand = new RelayCommand(SaveData);   
        }

        public void SendRequest(string request)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Parse(serverAddress), Port);   

            byte[] requestBytes = Encoding.UTF8.GetBytes(request);
            udpClient.Send(requestBytes, requestBytes.Length, serverEndpoint);

            IPEndPoint senderEndpoint = new IPEndPoint(IPAddress.Any, Port);
            byte[] receiveData = udpClient.Receive(ref senderEndpoint);
            response = Encoding.UTF8.GetString(receiveData);
            
            udpClient.Close();
        }

        private void LoadData()
        {
            SendRequest("1");
            foreach (string line in response.Split(new[] {'\n'}))
            {
                string str = line.ToString();
                string[] parts = str.Split(';');
                if (parts.Length == 7)
                {
                    Model_Song song = new Model_Song
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Author = parts[2],
                        Genre = parts[3],
                        Year_Of_Release = int.Parse(parts[4]),
                        Rating_Number = int.Parse(parts[5]),
                        Is_Top100 = bool.Parse(parts[6]),
                    };
                    if (!songDictionary.ContainsKey(song.Id))
                    {
                        songDictionary.Add(song.Id, song);
                        songs.Add(song);
                    }
                }
            }
        }
        private void SaveData()
        {
            StringBuilder builder = new();
            for(int i = 0; i < songs.Count; i++)
            {
                string songString = $"{songs[i].Name};{songs[i].Author};{songs[i].Genre};{songs[i].Year_Of_Release};{songs[i].Rating_Number};{songs[i].Is_Top100}";
                builder.AppendLine(songString);
            }
            string requestData = builder.ToString();
            SendRequest("2" + ";" + requestData);
        }
    }
}
