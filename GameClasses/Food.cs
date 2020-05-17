using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class Food : IFood
    {
        public Ellipse circle;
        
        public Point point { get; set; }

        public Food(Point point)
        {
            this.point = point;
            circle = new Ellipse();
            circle.Width = circle.Height = 30;
            circle.Fill = Brushes.Crimson;
        }
        public void Create()
        {
            Canvas.SetLeft(circle, point.X);
            Canvas.SetTop(circle, point.Y);
        }
    }
}
