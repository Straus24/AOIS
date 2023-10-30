namespace Lab1
{
    class Song
    {
        public string Name { get  => name;  set => name = value;  }
        public string Author { get => author; set => author = value; }
        public string Genre { get => genre;  set => genre = value;  }
        public int Year_Of_Release { get => year_of_release;  set => year_of_release = value;  }
        public int Rating_Number { get => rating_number;  set => rating_number = value;  }
        public bool Is_Top100
        {
            get  => (rating_number >= 1) && (rating_number <= 100); 
        }

        private string name;
        private string author;
        private string genre;
        private int year_of_release;
        private int rating_number;
    }
}
