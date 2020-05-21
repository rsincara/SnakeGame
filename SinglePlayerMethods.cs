using System.Collections.Generic;
using System.IO;
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
            MessageBox.Show(cause);
            Close();
            time.Stop();
            this.Owner.Show();
            ScoreClass.AddScore(score);
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
            food.Create(foodCanvas);
            foodCanvas.Children.Clear();
            foodCanvas.Children.Add(food.circle);
        }
        void MoveSnake()
        {
            for (int i = snakeBody.Count - 1; i > 0; i--)
            {
                snakeBody[i] = snakeBody[i - 1];
            }
        }

        void CheckAndIncrease()
        {
            if (snakeBody[0].point == food.point)
            {
                snakeBody.Add(new SnakeElement(food.point));
                AddFoodInCanvas();
                score += 10;
                resultBlock.Text = resultBlock.Text.Substring(0,6) + score;
            }
        }

        void CheckAndChangeDirectory()
        {
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
        }

        void CheckForFails()
        {
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
        }

        void DrawSnake()
        {
            int count = 0;
            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is Rectangle)
                    count++;
            }

            canvas.Children.RemoveRange(0, count);
            AddSnakeInCanvas();
        }
    }
}
