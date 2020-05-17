using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfApp1.GameClasses;

namespace WpfApp1
{
    public partial class MultiPlayer : Window
    {
        private TextBlock scoreText;
        const int moveSize = 30;
        int score;
        BotMoving botMoving;
        DispatcherTimer time;
        private Random rnd = new Random();
        int playerLifes = 3, enemyLifes = 3;
        private int width = SettingsClass.Width; // Ширина окна
        private int height = SettingsClass.Height; // Высота окна
        private List<SnakeElement> snakeBody;
        private List<SnakeElement> snakeBodyEnemy;
        private Food food;
        private Directions currentPlayerDirection;
        Directions currentEnemyDirections;
        private Point playerStartPosition;
        private Point enemyStartPosition;

        public MultiPlayer()
        {
            InitializeComponent();
            score = 0;
            scoreText = new TextBlock();
            TextBlock scoreBlock = new TextBlock()
            {
                Text = "Очки: ",
                Height = 50,
                Width = 30
            };
            scoreText.Width = 30;
            scoreText.Height = 30;
            scoreText.Text = score.ToString();
            myGrid.Height = 150;
            myGrid.Width = 100;
            myGrid.HorizontalAlignment = HorizontalAlignment.Right;
            myGrid.Children.Add(scoreBlock);
            myGrid.Children.Add(scoreText);
            this.foodCanvas.Width = width;
            this.foodCanvas.Height = height;
            this.enemyCanvas.Width = width;
            this.enemyCanvas.Height = height;
            this.canvas.Width = width;
            this.canvas.Height = height;
            this.Width = width;
            this.Height = height;
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


        private void GameOver(string cause)
        {
            var list = new List<string>();
            MessageBox.Show(cause);
            Close();
            time.Stop();
            this.Owner.Show();
            list.Add(score.ToString());
            File.AppendAllLines("scores/scores.txt", list);
        }


        void AddEnemySnakeInCanvas()
        {
            foreach (var snake in snakeBodyEnemy)
            {
                snake.Create(enemyCanvas);
            }
        }


        void time_Tick(object sender, EventArgs e)
        {
            botMoving = new BotMoving(food, snakeBody, snakeBodyEnemy, currentPlayerDirection,
                                      currentEnemyDirections, width, height, moveSize, canvas, enemyCanvas);
            if (SettingsClass.Mode == 1) // против компа
            {
                currentEnemyDirections = botMoving.GetNextDirection();
            }
            MoveSnake(snakeBody);
            MoveSnake(snakeBodyEnemy);

            CheckAndIncreaseEnemy(ref snakeBodyEnemy);
            CheckAndIncreasePlayer(ref snakeBody);

            CheckAndChangeDirectory();
                
            CheckForFailsPlayer();
            CheckForFailsEnemy();

         

            DrawPlayerSnake();
            DrawEnemySnake();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddFoodInCanvas();
            AddSnakeInCanvas();
            AddEnemySnakeInCanvas();
            time.Start();
        }


       // public enum Directions
       // {
           // Up,
         //   Down,
        //    Left,
         //   Right,
       //     Stay
      //  }
    }
}
