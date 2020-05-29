using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public static MediaPlayer player = new MediaPlayer {Volume = 0.1};

        public MainWindow()
        {
            InitializeComponent();
            Backgr.ImageSource = new BitmapImage(new Uri("images/main.jpg", UriKind.Relative));
            player.Open(new Uri("sounds/menu.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            player.MediaEnded += (s, e) =>
            {
                player.Stop();
                player.Play();
            };
        }


        private void SinglePlayerButtonClick(object sender, RoutedEventArgs e)
        {
            SingleplayerWindow window = new SingleplayerWindow();
            window.Owner = this;
            Hide();
            window.Show();
            player.Stop();
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
            player.Stop();
        }

        private void RulesButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "В одиночной игре: не врезайтесь в края карты и себя. Собирайте как можно больше яблок и растите! За каждое яблоко дается 10 очков!" +
                "\nВ мультиплеере: игра оканчивается если: " +
                "\n1) Какой-либо игрок растратил все жизни" +
                "\n2) Какой-либо игрок съел подряд 15 яблок" +
                "\nНе дайте противнику съесть нужное количество яблок!" +
                "\nТакже нельзя врезаться в противника. У каждого игрока по 3 жизни." +
                "\nУдачи!", "Правила игры");
        }
    }
}
