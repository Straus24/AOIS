namespace Lab2
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("\nCHOOSE AN ACTION:");
            Console.WriteLine("\n1)Показать песни");
            Console.WriteLine("2)Выбор песни по номеру");
            Console.WriteLine("3)Удалить песню");
            Console.WriteLine("4)Добавить песню");
            Console.WriteLine("5)Удалить все песни\n");
            Console.WriteLine ("Esc - выход из программы");
        }
        static void Main(string[] args)
        {
           
            PrintMenu();
            ConsoleKey key = ConsoleKey.Enter;

            while (key != ConsoleKey.Escape)
            {

                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        _ = Client.SendRequest("1");
                        PrintMenu();
                        break;
                    case ConsoleKey.D2:
                        {
                            Console.Write("\nВведите номер песни:");
                            string str = Console.ReadLine();
                            Console.Clear();
                            _ = Client.SendRequest("2" + ";" + str);
                            PrintMenu();
                        }
                        break;
                    case ConsoleKey.D3:
                        {                     
                            Console.Write("\nВведите номер песни:");
                            string str = Console.ReadLine();
                            Console.Clear();
                            _ = Client.SendRequest("3" + ";" + str);
                            PrintMenu();
                        }
                        break;
                    case ConsoleKey.D4:
                        {
                            Console.Write("\nНазвание:");
                            string name = Console.ReadLine();
                            Console.Write("Автор:");
                            string author = Console.ReadLine();
                            Console.Write("Жанр:");
                            string genre = Console.ReadLine();
                            Console.Write("Год релиза:");
                            string year_of_release = Console.ReadLine();
                            Console.Write("Место в рейтинге:");
                            string rating_number = Console.ReadLine();
                            Console.Clear();
                            _ = Client.SendRequest("4" + ";" + name + ";" + author + ";" + genre + ";" + year_of_release + ";" + rating_number);
                            PrintMenu();
                        }
                        break;
                    case ConsoleKey.D5:
                        {
                            Console.Clear();
                            _ = Client.SendRequest("5");
                            PrintMenu();
                            break;
                        }
                    default:
                        break;
                }
            }

        }
    }
}