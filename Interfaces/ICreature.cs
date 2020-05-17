using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public interface ICreature
    {
        Point point { get; set; }
        void Create(Canvas canvas);
    }
}