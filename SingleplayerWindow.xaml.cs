using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.GameClasses;

namespace WpfApp1
{
    public partial class SingleplayerWindow : Window
    {
        const int moveSize = 30;
        int score;
        DispatcherTimer time;
        private Random rnd = new Random();
        private int width = SettingsClass.Width; // Ширина окна
        private int height = SettingsClass.Height; // Высота окна
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

            time = new DispatcherTimer();
            currentDurection = Directions.Stay;
            snakeBody = new List<SnakeElement>();

            snakeBody.Add(new SnakeElementHead(new Point(moveSize, moveSize)));
            snakeBody[0].rectangle.Fill = SettingsClass.ColorOfPlayer;
            time.Interval =
                new TimeSpan(0, 0, 0, 0, SettingsClass.Difficulty); /*you can change speed of the snake here */
            time.Tick += time_Tick;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && currentDurection != Directions.Down)
                currentDurection = Directions.Up;
            if (e.Key == Key.Down && currentDurection != Directions.Up)
                currentDurection = Directions.Down;
            if (e.Key == Key.Left && currentDurection != Directions.Right)
                currentDurection = Directions.Left;
            if (e.Key == Key.Right && currentDurection != Directions.Left)
                currentDurection = Directions.Right;
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
    }
}
