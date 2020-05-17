using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
namespace WpfApp1
{
    public class SnakeElementHead : SnakeElement
    {
        private Point point { get; set; }
        Rectangle head;

        public SnakeElementHead(Point point) : base(point)
        {
            this.point = point;
            head = new Rectangle();
            head.Width = head.Height = 30;
            head.Fill = Brushes.DarkGreen;
        }
        
        public new void Create(Canvas canvas)
        {
            Canvas.SetLeft(head, point.X);
            Canvas.SetTop(head, point.Y);
            canvas.Children.Add(head);
        }
    }
}
