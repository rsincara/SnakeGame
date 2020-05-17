﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    public static class SettingsClass
    {
        public static int Width = 500;
        public static int Height = 500;
        public static int Difficulty = 200;  //100 - хард 200 - медиум 300 - изи
        public static int Mode = 1;

        public static Brush ColorOfPlayer = Brushes.DarkGreen;
        public static Brush ColorOfEnemy = Brushes.Indigo;
        //0 - против человека  1 - против конченого алгоритма
        //public static Brush SnakeColor = Brushes.DarkGreen;
    }
}