using System;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;
using WpfApp1.GameClasses;

namespace WpfApp1
{
    public partial class SingleplayerWindow
    {
        private void GameOver(string cause)
        {
            backgroundMusic.Stop();
            MessageBox.Show(cause);
            Close();
            time.Stop();
            this.Owner.Show();
            ScoreClass.AddScore(score);
            MainWindow.player.Play();
        }

        private void PlaySound(string soundName)
        {
            gameSounds.Open(new Uri("sounds/" + soundName, UriKind.RelativeOrAbsolute));
            gameSounds.Play();
        }

        private void AddSnakeInCanvas()
        {
            foreach (var snake in snakeBody)
            {
                snake.Create(canvas);
            }
        }

        private void AddFoodInCanvas()
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
            food.Create(foodCanvas);
            foodCanvas.Children.Clear();
            foodCanvas.Children.Add(food.circle);
        }

        private void MoveSnake()
        {
            for (int i = snakeBody.Count - 1; i > 0; i--)
            {
                snakeBody[i] = snakeBody[i - 1];
            }
        }

        private void CheckAndIncrease()
        {
            if (snakeBody[0].point == food.point)
            {
                PlaySound("apple.mp3");
                snakeBody.Add(new SnakeElement(food.point));
                AddFoodInCanvas();
                score += 10;
                resultBlock.Text = resultBlock.Text.Substring(0, 6) + score;
            }
        }

        private void CheckAndChangeDirectory()
        {
            switch (currentDurection)
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
        }

        private static SnakeElement CreateSnakeBody(SnakeElement snakeElement, Point newPoint)
        {
            var ns = new SnakeElement(newPoint);
            ns.rectangle.Fill = snakeElement.rectangle.Fill;
            return ns;
        }
        private void CheckForFails()
        {
            if (snakeBody[0].point.X < 0 || snakeBody[0].point.Y < 0 || snakeBody[0].point.X > width - moveSize ||
                snakeBody[0].point.Y > height - moveSize)
            {
                PlaySound("eatWall.mp3");
                GameOver("Вы проиграли, ваш рекорд: " + score);
            }

            for (int i = 1; i < snakeBody.Count; i++)
            {
                if (snakeBody[0].point == snakeBody[i].point)
                {
                    PlaySound("eatSnake.mp3");
                    GameOver("Вы съели сами себя! Ваш результат: " + score);
                }
            }
        }

        private void DrawSnake()
        {
            canvas.Children.Clear();
            AddSnakeInCanvas();
        }
    }
}
