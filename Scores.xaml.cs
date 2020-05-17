using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class Scores : Window
    {
        public Scores()
        {
            InitializeComponent();
            Backgr.ImageSource = new BitmapImage(new Uri("images/main.jpg", UriKind.Relative));
            StackPanel stackPanel = new StackPanel();
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel.Margin = new Thickness(10, 30, 0, 0);
            stackPanel.Height = Height - 100;
            var records = File.ReadAllLines("scores/scores.txt").OrderByDescending(int.Parse);
            string[] scores;
            scores = records.Count() > 10 ? records.Take(10).ToArray() : records.ToArray();

            for (int i = 0; i < scores.Length; i++)
            {
                var txtbl = new TextBlock();
                txtbl.FontSize = 24;
                txtbl.Text = (i + 1).ToString() + ". " + scores[i];
                stackPanel.Children.Add(txtbl);
            }
            myGrid.Children.Add(stackPanel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Owner.Show();
        }
    }
}
