using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1.GameClasses
{
    class Rock
    {
        public Rectangle rectangle;
        public Point point { get; set; }

        public Rock(Point point)
        {
            this.point = point;
            rectangle = new Rectangle();
            rectangle.Height = rectangle.Width = 30;
            rectangle.Fill = Brushes.Gray;

        }
        public void Create()
        {
            Canvas.SetLeft(rectangle, point.X);
            Canvas.SetTop(rectangle, point.Y);
        }
    }
}
