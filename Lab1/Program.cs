using System;
using System.IO;

namespace Lab1
{
    class Program
    {
        public static void ChooseByNumber(string path)
        {
            Console.Clear();
            string output ="";
            Console.Write("\nEnter the number: ");
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    for (int i = -1; i < input; i++)
                    {
                        output = sr.ReadLine();
                    }
                }

                Console.Clear();
                Console.Write("\n" + output + "\n");
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.Write("\n" + e.Message + "\n");
            }   
        }

        public static void WriteFile(Song song, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(song.Name + " " + song.Author + " " + song.Genre + " " + song.Year_Of_Release + " " + song.Rating_Number + " " + song.Top100);
            }
        }

        public static void ClearFile(string path)
        {
            Console.Clear();
            Console.WriteLine("\nAre you sure? \n(Y) - yes ; (Any button) - no");
            ConsoleKey key = ConsoleKey.Enter;
            key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.Y:
                    using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(string.Empty);
                    }
                    break;
                case ConsoleKey.N:
                    break;
            }
            Console.Clear();
        }

        public static void ShowFile(string path)
        {
            Console.Clear();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.Write(line + "\n");
                }
            }
        }

        public static void AddSong(string path)
        {
            Console.Clear();
            try
            {
                Song song = new Song();
                Console.Write("Name: ");
                song.Name = Console.ReadLine();
                Console.Write("Author: ");
                song.Author = Console.ReadLine();
                Console.Write("Genre: ");
                song.Genre = Console.ReadLine();
                Console.Write("Year of release: ");
                song.Year_Of_Release = Convert.ToInt32(Console.ReadLine());
                Console.Write("Number in the rating: ");
                song.Rating_Number = Convert.ToInt32(Console.ReadLine());
                WriteFile(song, path);
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }     
        }


        public static void PrintMenu()
        {
            Console.WriteLine("\nCHOOSE AN ACTION:");
            Console.WriteLine("\n1)Add song to file");
            Console.WriteLine("2)Show songs");
            Console.WriteLine("3)Clear file");
            Console.WriteLine("4)Choose song by number");
            Console.Write("\nesc - exit");
        }

        static void Main(string[] args)
        {
            string path = @"U:\VisualStudio\Architecture Korovkin\Lab1\Music_info.csv";

            ConsoleKey key = ConsoleKey.Enter;
            bool running = true;

                while (running)
                {
                    if (key != ConsoleKey.Escape)
                    {
                        PrintMenu();
                        key = Console.ReadKey().Key;
                        switch (key)
                        {
                            case ConsoleKey.D1:
                                AddSong(path);
                                break;
                            case ConsoleKey.D2:
                                ShowFile(path);
                                break;
                            case ConsoleKey.D3:
                                ClearFile(path);
                                break;
                            case ConsoleKey.D4:
                                ChooseByNumber(path);
                                break;

                            default:
                                Console.Clear();
                                break;
                        }
                    }
                    else
                    {
                        running = false;
                    }  
                }
            }
        }
}
