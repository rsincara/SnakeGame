using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.IO;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.GameClasses;
using Timer = System.Timers.Timer;

namespace WpfApp1
{
    public partial class SingleplayerWindow : Window
    {
        const int moveSize = 30;
        int score;
        readonly DispatcherTimer time;
        private readonly Random rnd = new Random();
        private readonly int width = SettingsClass.Width;
        private readonly int height = SettingsClass.Height; 
        private List<SnakeElement> snakeBody;
        private Food food;
        private Directions currentDurection;

        public SingleplayerWindow()
        {
            InitializeComponent();
            Backgr.ImageSource = new BitmapImage(new Uri("images/backgame.png", UriKind.Relative));
            score = 0;
            Width = width + 100;
            foodCanvas.Width = canvas.Width = width;
            canvas.Height = foodCanvas.Height = Height = height;
            myGrid.Height = height;
            myGrid.Children.Add(new Rectangle{Height = height, Width = 100, StrokeDashCap = PenLineCap.Round,
                Stroke = Brushes.Gray});
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
        
        void time_Tick(object sender, EventArgs e)
        {
            
            MoveSnake();
            CheckAndIncrease();
            CheckAndChangeDirectory();
            CheckForFails();
            DrawSnake();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddFoodInCanvas();
            AddSnakeInCanvas();
            time.Start();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Owner.Show();
        }
    }
}
