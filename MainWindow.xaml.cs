using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            Backgr.ImageSource = new BitmapImage(new Uri("images/main.jpg", UriKind.Relative));


        }


        private void SinglePlayerButtonClick(object sender, RoutedEventArgs e)
        {
            SingleplayerWindow window = new SingleplayerWindow();
            window.Owner = this;
            Hide();
            window.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings window = new Settings();
            window.Owner = this;
            Hide();
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Scores window = new Scores();
            window.Owner = this;
            Hide();
            window.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MultiPlayer window = new MultiPlayer();
            window.Owner = this;
            Hide();
            window.Show();
        }
    }
}
