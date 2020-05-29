using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp1.GameClasses;

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
            var scores = ScoreClass.GetFirstScoresArray();
            
            for (int i = 0; i < scores.Length; i++)
            {
                var txtbl = new TextBlock();
                txtbl.FontSize = 24;
                txtbl.Text = (i + 1).ToString() + ". " + scores[i];
                stackPanel.Children.Add(txtbl);
            }
            stackPanel.Children.Add(new TextBlock {Text = "Игр сыграно: " + ScoreClass.GetGamesCount(), FontSize = 26});
            myGrid.Children.Add(stackPanel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Owner.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Owner.Show();
        }
    }
}
