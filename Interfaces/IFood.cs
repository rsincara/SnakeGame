using System;
using System.Windows;

namespace WpfApp1
{
    public interface IFood
    {
        Point point { get; set; }
        void Create();

    }
}
