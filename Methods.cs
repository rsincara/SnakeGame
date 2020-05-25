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
        void AddFoodInCanvas()
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

      
        void AddSnakeInCanvas(List<SnakeElement> snakebody, Canvas canvas)
        {
            foreach (var snakeElement in snakebody)
            {
                snakeElement.Create(canvas);
            }
        }
        
        void MoveSnake(List<SnakeElement> snakebody)
        {
            for (int i = snakebody.Count - 1; i > 0; i--)
            {
                snakebody[i] = snakebody[i - 1];
            }
        }

        void CheckAndIncreasePlayer(ref List<SnakeElement> snakebody)
        {
            if (snakebody[0].point == food.point)
            {
                snakebody.Add(new SnakeElement(food.point));
                AddFoodInCanvas();
                score += 10;
                resultBlock.Text = resultBlock.Text.Substring(0, 6) + score;
            }
        }

        void CheckAndIncreaseEnemy(ref List<SnakeElement> snakebody)
        {
            if (snakebody[0].point == food.point)
            {
                var color = snakebody[0].rectangle.Fill;
                var newSnake = new SnakeElement(food.point);
                newSnake.rectangle.Fill = color;
                snakebody.Add(newSnake);
                AddFoodInCanvas();
              
            }

            if(snakebody.Count == 16) GameOver("Второй игрок съел 15 яблок!");
        }

        SnakeElement CreateSnakeBody(SnakeElement snakeElement, Point newPoint)
        {
            var color = snakeElement.rectangle.Fill;
            var newSnake = new SnakeElement(newPoint);
            newSnake.rectangle.Fill = color;
            return newSnake;

        }

        void CheckAndChangeDirectory()
        {
            switch (currentPlayerDirection)
            {
                case Directions.Down:
                    snakeBody[0] = CreateSnakeBody(snakeBody[0],
                                                   new Point(snakeBody[0].point.X, snakeBody[0].point.Y + moveSize));
                    break;
                case Directions.Left:
                    snakeBody[0] = CreateSnakeBody(snakeBody[0],
                                                   new Point(snakeBody[0].point.X- moveSize, snakeBody[0].point.Y));
                    break;
                case Directions.Right:
                    snakeBody[0] = CreateSnakeBody(snakeBody[0],
                                                   new Point(snakeBody[0].point.X+moveSize, snakeBody[0].point.Y));
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
                                                   new Point(snakeBodyEnemy[0].point.X, snakeBodyEnemy[0].point.Y + moveSize));
                    break;
                case Directions.Left:
                    snakeBodyEnemy[0] = CreateSnakeBody(snakeBodyEnemy[0],
                                                   new Point(snakeBodyEnemy[0].point.X- moveSize, snakeBodyEnemy[0].point.Y));
                    break;
                case Directions.Right:
                    snakeBodyEnemy[0] = CreateSnakeBody(snakeBodyEnemy[0],
                                                   new Point(snakeBodyEnemy[0].point.X+moveSize, snakeBodyEnemy[0].point.Y));
                    break;
                case Directions.Up:
                    snakeBodyEnemy[0] = CreateSnakeBody(snakeBodyEnemy[0],
                                                   new Point(snakeBodyEnemy[0].point.X, snakeBodyEnemy[0].point.Y - moveSize));
                    break;
            }
        }

        void RespawnPlayer(ref List<SnakeElement> snakebody)
        {
            --playerLifes;
            firstPlayerLifes.Text = firstPlayerLifes.Text.Substring(0,10) + playerLifes;
            currentPlayerDirection = Directions.Stay;
            var color = snakebody[0].rectangle.Fill;
            snakebody = new List<SnakeElement> {new SnakeElement(playerStartPosition)};
            snakebody[0].rectangle.Fill = color;
        }

        void RespawnEnemy()
        {
            --enemyLifes;
            secondPlayerLifes.Text = secondPlayerLifes.Text.Substring(0,10) + enemyLifes;
            currentEnemyDirections = Directions.Stay;
            var color = snakeBodyEnemy[0].rectangle.Fill;
            snakeBodyEnemy = new List<SnakeElement> {new SnakeElement(enemyStartPosition)};
            snakeBodyEnemy[0].rectangle.Fill = color;
        }


        void CheckForFailsPlayer()
        {
            if (snakeBody[0].point.X < 0 || snakeBody[0].point.Y < 0 || snakeBody[0].point.X > width - moveSize ||
                snakeBody[0].point.Y > height - moveSize)
            {
                if (playerLifes > 1)
                    RespawnPlayer(ref snakeBody);
                else
                {
                    GameOver("Жизни кончились. Ваш счет: " + score);
                }
            }

            for (int i = 1; i < snakeBody.Count; i++)
            {
                if (snakeBody[0].point == snakeBody[i].point)
                {
                    GameOver("Себя есть нельзя! Ваш счет: " + score);
                }
            }

            for (int i = 0; i < snakeBodyEnemy.Count; i++)
            {
                if (snakeBody[0].point == snakeBodyEnemy[i].point)
                {
                    if (playerLifes > 1)
                        RespawnPlayer(ref snakeBody);
                    else
                    {
                        GameOver("Жизни кончились. Ваш счет: " + score);
                    }
                }
            }
        }

        void CheckForFailsEnemy()
        {
            for (int i = 1; i < snakeBodyEnemy.Count; i++)
            {
                if (snakeBodyEnemy[0].point == snakeBodyEnemy[i].point)
                {
                    if (enemyLifes > 1)
                        RespawnEnemy();

                    else
                    {
                        GameOver("Первый игрок победил! Ваш счет: " + score);
                    }
                }
            }
            
            if(snakeBodyEnemy[0].point.X < 0 || snakeBodyEnemy[0].point.Y < 0 || snakeBodyEnemy[0].point.X > width - moveSize ||
               snakeBodyEnemy[0].point.Y > height - moveSize)
            {
                if (enemyLifes > 1)
                    RespawnEnemy();
                else
                {
                    GameOver("Первый игрок победил! Ваш счет: " + score);
                }
            }


            for (int i = 0; i < snakeBody.Count; i++)
            {
                if (snakeBodyEnemy[0].point == snakeBody[i].point)
                {
                    if (enemyLifes > 1)
                    {
                        score += 100;
                        resultBlock.Text = resultBlock.Text.Substring(0, 6) + score;
                        RespawnEnemy();
                    }
                    else
                    {
                        GameOver("Первый игрок победил! Ваш счет: " + score);
                    }
                }
            }
        }

        void DrawSnake(List<SnakeElement> snakebody, Canvas canvas)
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
            Owner.Show();
        }
    }
}
