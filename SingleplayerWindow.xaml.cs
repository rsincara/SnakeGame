using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.GameClasses;

namespace WpfApp1
{
    public partial class SingleplayerWindow : Window
    {
        MediaPlayer gameSounds = new MediaPlayer {Volume = 0.1};
        MediaPlayer backgroundMusic = new MediaPlayer {Volume = 0.1};
        const int moveSize = 30;
        int score;
        readonly DispatcherTimer time;
        readonly Random rnd = new Random();
        readonly int width = SettingsClass.Width;
        readonly int height = SettingsClass.Height;
        List<SnakeElement> snakeBody;
        Food food;
        Directions currentDurection;

        public SingleplayerWindow()
        {
            InitializeComponent();
            backgroundMusic.Open(new Uri("sounds/gameMusic.mp3", UriKind.RelativeOrAbsolute));
            backgroundMusic.Play();
            Backgr.ImageSource = new BitmapImage(new Uri("images/backgame.png", UriKind.Relative));
            score = 0;
            Width = width + 100;
            foodCanvas.Width = canvas.Width = width;
            canvas.Height = foodCanvas.Height = Height = height;
            myGrid.Height = height;
            myGrid.Children.Add(new Rectangle
            {
                Height = height, Width = 100, StrokeDashCap = PenLineCap.Round,
                Stroke = Brushes.Gray
            });
            bestScore.Text = ScoreClass.GetBestScore();
            time = new DispatcherTimer();
            currentDurection = Directions.Stay;
            snakeBody = new List<SnakeElement>();
            snakeBody.Add(new SnakeElement(new Point(moveSize, moveSize)));
            snakeBody[0].rectangle.Fill = SettingsClass.PlayerColor;
            time.Interval =
                new TimeSpan(0, 0, 0, 0, SettingsClass.Difficulty);
            time.Tick += time_Tick;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && currentDurection != Directions.Down)
            {
                currentDurection = Directions.Up;
            }
            if (e.Key == Key.Down && currentDurection != Directions.Up)
            {
                currentDurection = Directions.Down;
            }
            if (e.Key == Key.Left && currentDurection != Directions.Right)
            {
                currentDurection = Directions.Left;
            }
            if (e.Key == Key.Right && currentDurection != Directions.Left)
            {
                currentDurection = Directions.Right;
            }
        }

        private void time_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            CheckAndIncrease();
            CheckAndChangeDirectory();
            CheckForFails();
            DrawSnake();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddFoodInCanvas();
            AddSnakeInCanvas();
            time.Start();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            time.Stop();
            backgroundMusic.Stop();
            MainWindow.player.Play();
            Owner.Show();
        }
    }
}
