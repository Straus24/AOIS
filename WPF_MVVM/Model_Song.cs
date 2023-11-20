using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM
{
    class Model_Song: INotifyPropertyChanged
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
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }
        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
                OnPropertyChanged("Genre");
            }
        }
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
                    OnPropertyChanged("Year_Of_Release");
                }
                else if (value > DateTime.Now.Year)
                {
                    year_of_release = DateTime.Now.Year;
                    OnPropertyChanged("Year_Of_Release");
                }
                else
                {
                    year_of_release = value;
                    OnPropertyChanged("Year_Of_Release");
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
                    OnPropertyChanged("Rating_Number");
                }
                else
                {
                    rating_number = value;
                    OnPropertyChanged("Rating_Number");
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
                    OnPropertyChanged("Is_Top100");
                }
                else
                {
                    is_top100 = false;
                    OnPropertyChanged("Is_Top100");
                }
            }
        }

        private string? genre;
        private string? author;
        private bool is_top100;
        private string name;
        private int year_of_release;
        private int rating_number;


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
