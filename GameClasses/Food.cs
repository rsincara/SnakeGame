using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class Food : IFood
    {
        
        ImageBrush food = new ImageBrush(new BitmapImage(new Uri("images/apple.png", UriKind.Relative)));
        
        public Ellipse circle;
        public Point point { get; set; }

        public Food(Point point)
        {
            this.point = point;
            circle = new Ellipse();
            circle.Width = circle.Height = 30;
            circle.Fill = food;
            
            
        }
        public void Create(Canvas canvas)
        {
            Canvas.SetLeft(circle, point.X);
            Canvas.SetTop(circle, point.Y);
            canvas.Children.Add(circle);
        }
    }
}
