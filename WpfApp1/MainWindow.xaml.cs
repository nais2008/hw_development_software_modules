using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSayHello_Click(object sender, RoutedEventArgs e)
        {
            TextName.Text = $"Привет, {TextBoxName.Text}";
        }

        private void TextBoxLen_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlockLen.Text = TextBoxLen.Text.Length.ToString();
        }
        private void ButtonConcat_Click(object sender, RoutedEventArgs e)
        {
            TextBoxResult.Text =
                TextBoxFirst.Text + " " + TextBoxSecond.Text;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int a;
            int b;

            if (!int.TryParse(TextBoxA.Text, out a) ||
                !int.TryParse(TextBoxB.Text, out b))
            {
                MessageBox.Show("Введите корректные числа");
                return;
            }

            int result = a + b;

            TextBlockResult.Text = result.ToString();
        }


    }
}
