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
using Brushes = System.Windows.Media.Brushes;

namespace WpfApp1
{
    public class SnakeElement : ICreature
    {
       public Rectangle rectangle;
        public Point point { get; set; }
        
        public SnakeElement(Point point)
        {
            this.point = point;
            rectangle = new Rectangle();
            rectangle.Height = rectangle.Width = 30;
            rectangle.Fill = SettingsClass.ColorOfPlayer;
            rectangle.StrokeDashCap = PenLineCap.Round;
            rectangle.Stroke = Brushes.Black;
        }
        public void Create(Canvas canvas)
        {
            Canvas.SetLeft(rectangle, point.X);
            Canvas.SetTop(rectangle, point.Y);
            canvas.Children.Add(rectangle);
        }
    }

  
}
