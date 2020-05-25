using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            Backgr.ImageSource = new BitmapImage(new Uri("images/main.jpg", UriKind.Relative));
            height.Text = SettingsClass.Height.ToString();
            width.Text = SettingsClass.Width.ToString();
            var colors = typeof(Brushes).GetProperties().Select(x => x.Name);
            snakeColors.ItemsSource = colors;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int wid = SettingsClass.Width;
            int heig = SettingsClass.Height;
            if (!int.TryParse(this.width.Text, out wid) || !int.TryParse(this.height.Text, out heig))
            {
                MessageBox.Show("Введенные данные имеют неверный формат");
                return;
            }

            var rb = diffStack.Children.OfType<RadioButton>().Where(x => (bool) x.IsChecked);
            if (rb.Any())
            {
                var rbut = rb.First();
                switch (rbut.Content)
                {
                    case "Easy":
                        SettingsClass.Difficulty = 300;
                        break;
                    case "Medium":
                        SettingsClass.Difficulty = 200;
                        break;
                    default:
                        SettingsClass.Difficulty = 100;
                        break;
                }
            }

            var mode = modeStack.Children.OfType<RadioButton>().Where(x => (bool) x.IsChecked);
            if (mode.Any())
            {
                switch (mode.First().Name)
                {
                    case "friendMode":
                        SettingsClass.Mode = 0;
                        break;
                    default:
                        SettingsClass.Mode = 1;
                        break;
                }
            }

            if (snakeColors.SelectedValue != null)
            {
                var selectedColor = snakeColors.SelectedValue.ToString();
                var clr = typeof(Brushes).GetRuntimeProperty(selectedColor);
                SettingsClass.PlayerColor = (Brush) clr.GetValue(null, null);
            }
            
            SettingsClass.Width = wid;
            SettingsClass.Height = heig;
            Close();
            Owner.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
            Owner.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Owner.Show();
        }
    }
}
