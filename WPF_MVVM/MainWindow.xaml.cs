using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace WPF_MVVM
{
    public partial class MainWindow : Window
    {
        ViewModel_Songs VMS = new ViewModel_Songs();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VMS;
        }
    }
}
