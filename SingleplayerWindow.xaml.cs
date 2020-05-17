﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace WpfApp1
{
    public partial class SingleplayerWindow : Window
    {
        Stack<Directions> lastDirection;
        private TextBlock scoreText;
        const int moveSize = 30;
        int score;
        int count = 0;
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

            this.canvas.Width = width;
            this.canvas.Height = height;
            this.Width = width;
            this.Height = height;

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

        void AddSnakeInCanvas()
        {
            foreach (var snake in snakeBody)
            {
                snake.Create(canvas);
            }
        }

        void AddFoodInCanvas()
        {
            var correctPoint = new Point();
            var flag = true;
            while (flag)
            {
                correctPoint = new Point(rnd.Next(0, width / (moveSize + 2)) * moveSize,
                                         rnd.Next(0, height / (moveSize + 2)) * moveSize);
                flag = snakeBody.Any(x => x.point == correctPoint);
            }
            food = new Food(correctPoint);
            food.Create();
            canvas.Children.Insert(0, food.circle);
        }


        void time_Tick(object sender, EventArgs e)
        {
            for (int i = snakeBody.Count - 1; i > 0; i--)
            {
                snakeBody[i] = snakeBody[i - 1];
            }

            if (snakeBody[0].point == food.point)
            {
                snakeBody.Add(new SnakeElement(food.point));
                AddFoodInCanvas();
                score += 10;
                scoreText.Text = score.ToString();
            }

            switch (currentDurection)
            {
                case Directions.Down:
                    snakeBody[0] = new SnakeElement(new Point(snakeBody[0].point.X, snakeBody[0].point.Y + moveSize));
                    break;
                case Directions.Left:
                    snakeBody[0] = new SnakeElement(new Point(snakeBody[0].point.X - moveSize, snakeBody[0].point.Y));
                    break;
                case Directions.Right:
                    snakeBody[0] = new SnakeElement(new Point(snakeBody[0].point.X + moveSize, snakeBody[0].point.Y));
                    break;
                case Directions.Up:
                    snakeBody[0] = new SnakeElement(new Point(snakeBody[0].point.X, snakeBody[0].point.Y - moveSize));
                    break;
            }

            if (snakeBody[0].point.X < 0 || snakeBody[0].point.Y < 0 || snakeBody[0].point.X > width - moveSize ||
                snakeBody[0].point.Y > height - moveSize)
            {
                GameOver("Вы проиграли, ваш рекорд: " + score);
            }

            for (int i = 1; i < snakeBody.Count; i++)
            {
                if (snakeBody[0].point == snakeBody[i].point)
                {
                    GameOver("Вы съели сами себя! Ваш результат: " + score);
                }
            }

            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is Rectangle)
                    count++;
            }

            canvas.Children.RemoveRange(1, count);
            count = 0;
            AddSnakeInCanvas();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddFoodInCanvas();
            AddSnakeInCanvas();
            time.Start();
        }


        public enum Directions
        {
            Up,
            Down,
            Left,
            Right,
            Stay
        }
    }
}
