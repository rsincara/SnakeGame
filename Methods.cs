using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WpfApp1.GameClasses;

namespace WpfApp1
{
    public partial class MultiPlayer
    {
        private void PlaySound(string soundName)
        {
            gameSounds.Open(new Uri("sounds/" + soundName, UriKind.RelativeOrAbsolute));
            gameSounds.Play();
        }

        private void AddFoodInCanvas()
        {
            var correctPoint = new Point();
            var flag1 = true;
            var flag2 = true;
            while (flag1 || flag2)
            {
                correctPoint = new Point(rnd.Next(0, width / (moveSize + 2)) * moveSize,
                                         rnd.Next(0, height / (moveSize + 2)) * moveSize);
                flag1 = snakeBody.Any(x => x.point == correctPoint);
                flag2 = snakeBodyEnemy.Any(x => x.point == correctPoint);
            }
            food = new Food(correctPoint);
            food.Create(foodCanvas);
            foodCanvas.Children.Clear();
            foodCanvas.Children.Add(food.circle);
        }


        private void AddSnakeInCanvas(List<SnakeElement> snakebody, Canvas canvas)
        {
            foreach (var snakeElement in snakebody)
            {
                snakeElement.Create(canvas);
            }
        }

        private void MoveSnake(List<SnakeElement> snakebody)
        {
            for (int i = snakebody.Count - 1; i > 0; i--)
            {
                snakebody[i] = snakebody[i - 1];
            }
        }

        private void CheckAndIncreasePlayer(List<SnakeElement> snakebody)
        {
            if (snakebody[0].point == food.point)
            {
                PlaySound("apple.mp3");
                snakebody.Add(new SnakeElement(food.point));
                AddFoodInCanvas();
            }

            if (snakebody.Count == 16) GameOver("Первый игрок съел 15 яблок!");
        }

        private void CheckAndIncreaseEnemy(List<SnakeElement> snakebody)
        {
            if (snakebody[0].point == food.point)
            {
                PlaySound("apple.mp3");
                var color = snakebody[0].rectangle.Fill;
                var newSnake = new SnakeElement(food.point);
                newSnake.rectangle.Fill = color;
                snakebody.Add(newSnake);
                AddFoodInCanvas();
            }

            if (snakebody.Count == 16) GameOver("Второй игрок съел 15 яблок!");
        }

        private static SnakeElement CreateSnakeBody(SnakeElement snakeElement, Point newPoint)
        {
            var color = snakeElement.rectangle.Fill;
            var newSnake = new SnakeElement(newPoint);
            newSnake.rectangle.Fill = color;
            return newSnake;
        }

        private void CheckAndChangeDirectory()
        {
            switch (currentPlayerDirection)
            {
                case Directions.Down:
                    snakeBody[0] = CreateSnakeBody(snakeBody[0],
                                                   new Point(snakeBody[0].point.X, snakeBody[0].point.Y + moveSize));
                    break;
                case Directions.Left:
                    snakeBody[0] = CreateSnakeBody(snakeBody[0],
                                                   new Point(snakeBody[0].point.X - moveSize, snakeBody[0].point.Y));
                    break;
                case Directions.Right:
                    snakeBody[0] = CreateSnakeBody(snakeBody[0],
                                                   new Point(snakeBody[0].point.X + moveSize, snakeBody[0].point.Y));
                    break;
                case Directions.Up:
                    snakeBody[0] = CreateSnakeBody(snakeBody[0],
                                                   new Point(snakeBody[0].point.X, snakeBody[0].point.Y - moveSize));
                    break;
            }
            switch (currentEnemyDirections)
            {
                case Directions.Down:
                    snakeBodyEnemy[0] = CreateSnakeBody(snakeBodyEnemy[0],
                                                        new Point(snakeBodyEnemy[0].point.X,
                                                                  snakeBodyEnemy[0].point.Y + moveSize));
                    break;
                case Directions.Left:
                    snakeBodyEnemy[0] = CreateSnakeBody(snakeBodyEnemy[0],
                                                        new Point(snakeBodyEnemy[0].point.X - moveSize,
                                                                  snakeBodyEnemy[0].point.Y));
                    break;
                case Directions.Right:
                    snakeBodyEnemy[0] = CreateSnakeBody(snakeBodyEnemy[0],
                                                        new Point(snakeBodyEnemy[0].point.X + moveSize,
                                                                  snakeBodyEnemy[0].point.Y));
                    break;
                case Directions.Up:
                    snakeBodyEnemy[0] = CreateSnakeBody(snakeBodyEnemy[0],
                                                        new Point(snakeBodyEnemy[0].point.X,
                                                                  snakeBodyEnemy[0].point.Y - moveSize));
                    break;
            }
        }

        private void GameOver(string cause)
        {
            backgroundMusic.Stop();
            MessageBox.Show(cause);
            Close();
            time.Stop();
            this.Owner.Show();
            ScoreClass.AddScore(0); //нужно только для подсчета количества игр
            MainWindow.player.Play();
        }


        private void RespawnPlayer(ref List<SnakeElement> snakebody)
        {
            --playerLifes;
            firstPlayerLifes.Text = firstPlayerLifes.Text.Substring(0, 10) + playerLifes;
            currentPlayerDirection = Directions.Stay;
            var color = snakebody[0].rectangle.Fill;
            snakebody = new List<SnakeElement> {new SnakeElement(playerStartPosition)};
            snakebody[0].rectangle.Fill = color;
        }

        private void RespawnEnemy()
        {
            --enemyLifes;
            secondPlayerLifes.Text = secondPlayerLifes.Text.Substring(0, 10) + enemyLifes;
            currentEnemyDirections = Directions.Stay;
            var color = snakeBodyEnemy[0].rectangle.Fill;
            snakeBodyEnemy = new List<SnakeElement> {new SnakeElement(enemyStartPosition)};
            snakeBodyEnemy[0].rectangle.Fill = color;
        }


        private void CheckForFailsPlayer()
        {
            if (snakeBody[0].point.X < 0 || snakeBody[0].point.Y < 0 || snakeBody[0].point.X > width - moveSize ||
                snakeBody[0].point.Y > height - moveSize)
            {
                PlaySound("eatWall.mp3");
                if (playerLifes > 1)
                    RespawnPlayer(ref snakeBody);
                else
                {
                    GameOver("Жизни кончились. Победил второй игрок");
                }
            }

            for (int i = 1; i < snakeBody.Count; i++)
            {
                if (snakeBody[0].point == snakeBody[i].point)
                {
                    PlaySound("eatSnake.mp3");
                    GameOver("Себя есть нельзя! Победил второй игрок");
                }
            }

            for (int i = 0; i < snakeBodyEnemy.Count; i++)
            {
                if (snakeBody[0].point == snakeBodyEnemy[i].point)
                {
                    PlaySound("eatSnake.mp3");
                    if (playerLifes > 1)
                        RespawnPlayer(ref snakeBody);
                    else
                    {
                        GameOver("Жизни кончились. Победил второй игрок");
                    }
                }
            }
        }

        private void CheckForFailsEnemy()
        {
            for (int i = 1; i < snakeBodyEnemy.Count; i++)
            {
                if (snakeBodyEnemy[0].point == snakeBodyEnemy[i].point)
                {
                    PlaySound("eatSnake.mp3");
                    if (enemyLifes > 1)
                        RespawnEnemy();

                    else
                    {
                        GameOver("Себя есть нельзя! Победил первый игрок");
                    }
                }
            }

            if (snakeBodyEnemy[0].point.X < 0 || snakeBodyEnemy[0].point.Y < 0 ||
                snakeBodyEnemy[0].point.X > width - moveSize ||
                snakeBodyEnemy[0].point.Y > height - moveSize)
            {
                PlaySound("eatWall.mp3");
                if (enemyLifes > 1)
                    RespawnEnemy();
                else
                {
                    GameOver("Жизни кончились. Победил первый игрок");
                }
            }

            for (int i = 0; i < snakeBody.Count; i++)
            {
                if (snakeBodyEnemy[0].point == snakeBody[i].point)
                {
                    PlaySound("eatSnake.mp3");
                    if (enemyLifes > 1)
                    {
                        RespawnEnemy();
                    }
                    else
                    {
                        GameOver("Жизни кончились. Победил первый игрок");
                    }
                }
            }
        }

        private void DrawSnake(List<SnakeElement> snakebody, Canvas canvas)
        {
            int count = 0;
            foreach (var item in canvas.Children)
            {
                if (item is Rectangle) count++;
            }
            canvas.Children.RemoveRange(0, count);
            AddSnakeInCanvas(snakebody, canvas);
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
