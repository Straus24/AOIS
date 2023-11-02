namespace UdpServer
{
    class Song
    {
        public int Id { get; set; }
        public string Name
        {
            get
            {
                return name;
            }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                else
                {
                    name = value;
                }
            }
        }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int Year_Of_Release
        {
            get
            {
                return year_of_release;
            }
            set
            {
                if (value < 1900)
                {
                    year_of_release = 1901;
                }
                else if (value > DateTime.Now.Year)
                {
                    year_of_release = DateTime.Now.Year;
                }
                else
                {
                    year_of_release = value;
                }
            }
        }
        public int Rating_Number
        {
            get
            {
                return rating_number;
            }
            set
            {
                if (value <= 0)
                {
                    rating_number = 1;
                }
                else
                {
                    rating_number = value;
                }
            }
        }
        public bool Is_Top100
        {
            get
            {
                return is_top100;
            }
            set 
            { 
                if (rating_number >= 1 && rating_number <= 100) 
                {
                    is_top100 = true;
                }
                else
                {
                    is_top100= false;
                }
            }
        }

        private bool is_top100;
        private string name;
        private int year_of_release;
        private int rating_number;
    }
}
