namespace Lab1
{
    class Song
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }


        private string author;
        public string Author { get { return author; } set { author = value; } }


        private string genre;
        public string Genre { get { return genre; } set { genre = value; } }


        private int year_of_release;
        public int Year_Of_Release { get { return year_of_release; } set { year_of_release = value; } }

        private int rating_number;
        public int Rating_Number { get { return rating_number; } set { rating_number = value; } }

        private bool is_top100;
        public bool Top100
        {
            get { return (rating_number >= 1) && (rating_number <= 100); }
            set { is_top100 = value; }
        }
    }
}
