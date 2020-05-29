using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp1.GameClasses;

namespace WpfApp1
{
    public partial class MultiPlayer : Window
    {
        readonly MediaPlayer gameSounds = new MediaPlayer {Volume = 0.1};
        readonly MediaPlayer backgroundMusic = new MediaPlayer {Volume = 0.1};
        const int moveSize = 30;
        BotMoving botMoving;
        readonly DispatcherTimer time;
        readonly Random rnd = new Random();
        int playerLifes = 3, enemyLifes = 3;
        readonly int width = SettingsClass.Width; // Ширина окна
        readonly int height = SettingsClass.Height; // Высота окна
        List<SnakeElement> snakeBody;
        List<SnakeElement> snakeBodyEnemy;
        Food food;
        Directions currentPlayerDirection;
        Directions currentEnemyDirections;
        readonly Point playerStartPosition;
        readonly Point enemyStartPosition;

        public MultiPlayer()
        {
            InitializeComponent();
            backgroundMusic.Open(new Uri("sounds/gameMusic.mp3", UriKind.RelativeOrAbsolute));
            backgroundMusic.Play();
            myGrid.Height = height;
            myGrid.Children.Add(new Rectangle
            {
                Height = height, Width = 100, StrokeDashCap = PenLineCap.Round,
                Stroke = Brushes.Gray
            });
            firstPlayerLifes.Text = firstPlayerLifes.Text + playerLifes;
            secondPlayerLifes.Text = secondPlayerLifes.Text + enemyLifes;
            Backgr.ImageSource = new BitmapImage(new Uri("images/backgame.png", UriKind.Relative));
            Width = width + 100;
            foodCanvas.Width = enemyCanvas.Width = canvas.Width = width;
            foodCanvas.Height = enemyCanvas.Height = canvas.Height = Height = height;
            playerStartPosition = new Point(moveSize, moveSize);
            enemyStartPosition = new Point(width / (moveSize + 4) * moveSize, height / (moveSize + 4) * moveSize);
            time = new DispatcherTimer();
            currentPlayerDirection = Directions.Stay;
            currentEnemyDirections = Directions.Stay;
            snakeBody = new List<SnakeElement>();
            snakeBodyEnemy = new List<SnakeElement>();
            snakeBody.Add(new SnakeElement(playerStartPosition));
            snakeBodyEnemy.Add(new SnakeElement(enemyStartPosition));
            snakeBodyEnemy[0].rectangle.Fill = Brushes.Indigo;
            time.Interval =
                new TimeSpan(0, 0, 0, 0, SettingsClass.Difficulty); /*you can change speed of the snake here */
            time.Tick += time_Tick;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && currentPlayerDirection != Directions.Down)
                currentPlayerDirection = Directions.Up;
            if (e.Key == Key.Down && currentPlayerDirection != Directions.Up)
                currentPlayerDirection = Directions.Down;
            if (e.Key == Key.Left && currentPlayerDirection != Directions.Right)
                currentPlayerDirection = Directions.Left;
            if (e.Key == Key.Right && currentPlayerDirection != Directions.Left)
                currentPlayerDirection = Directions.Right;

            if (SettingsClass.Mode == 0)
            {
                if (e.Key == Key.W && currentEnemyDirections != Directions.Down)
                    currentEnemyDirections = Directions.Up;
                if (e.Key == Key.S && currentEnemyDirections != Directions.Up)
                    currentEnemyDirections = Directions.Down;
                if (e.Key == Key.A && currentEnemyDirections != Directions.Right)
                    currentEnemyDirections = Directions.Left;
                if (e.Key == Key.D && currentEnemyDirections != Directions.Left)
                    currentEnemyDirections = Directions.Right;
            }
        }


        private void time_Tick(object sender, EventArgs e)
        {
            botMoving = new BotMoving(food, snakeBody, snakeBodyEnemy,
                                      currentEnemyDirections, width, height, moveSize);
            if (SettingsClass.Mode == 1) // против компа
                currentEnemyDirections = botMoving.GetNextDirection();
            MoveSnake(snakeBody);
            MoveSnake(snakeBodyEnemy);
            CheckAndIncreaseEnemy(snakeBodyEnemy);
            CheckAndIncreasePlayer(snakeBody);
            CheckAndChangeDirectory();
            DrawSnake(snakeBody, canvas);
            DrawSnake(snakeBodyEnemy, enemyCanvas);
            CheckForFailsPlayer();
            CheckForFailsEnemy();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddFoodInCanvas();
            AddSnakeInCanvas(snakeBody, canvas);
            AddSnakeInCanvas(snakeBodyEnemy, enemyCanvas);
            time.Start();
        }
    }
}
