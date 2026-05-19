using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = (comboBoxColors.SelectedItem as ComboBoxItem).Content.ToString();

            switch (selected)
            {
                case "Красный":
                    textBoxSample.Background = Brushes.Red;
                    break;
                case "Зеленый":
                    textBoxSample.Background = Brushes.Green;
                    break;
                case "Синий":
                    textBoxSample.Background = Brushes.Blue;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxEdenichi.SelectedItem == null)
            {
                MessageBox.Show("Выберите единицу измерения!");
                return;
            }

            string selected = (comboBoxEdenichi.SelectedItem as ComboBoxItem).Content.ToString();
            double inputNumber;

            if (!double.TryParse(TextBoxEdenichi.Text, out inputNumber))
            {
                MessageBox.Show("Введите корректное число!");
                return;
            }

            double resultInMeters = 0;

            switch (selected)
            {
                case "Метры":
                    resultInMeters = inputNumber;
                    break;

                case "Километры":
                    resultInMeters = inputNumber * 1000;
                    break;

                case "Сантиметры":
                    resultInMeters = inputNumber / 100;
                    break;

                default:
                    MessageBox.Show("Неизвестная единица измерения");
                    return;
            }
            TextBlockEdenichi.Text = $"{resultInMeters} м.";
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            buttonTask3.IsEnabled = checkBoxTask3.IsChecked == true;
        }


        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxTask3.Text;
            textBlockTask3.Text = $"Спасибо, {name}! Данные приняты.";
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(textBoxAmount.Text, out double amount))
            {
                textBlockResult.Text = "Ошибка: введите сумму";
                return;
            }

            string method = "";
            if (radioCash.IsChecked == true)
                method = "Наличные";
            else if (radioCard.IsChecked == true)
                method = "Карта";
            else if (radioOnline.IsChecked == true)
                method = "Онлайн";
            else
            {
                textBlockResult.Text = "Выберите способ оплаты";
                return;
            }

            textBlockResult.Text = $"Оплата {amount} руб. через {method}";
        }

        private void UpdateTextStyle(object sender, RoutedEventArgs e)
        {
            if (textBoxTextSample == null || comboBoxSize == null || checkBoxBold == null)
                return;
            if (comboBoxSize.SelectedItem is ComboBoxItem selectedItem)
            {
                if (double.TryParse(selectedItem.Content.ToString(), out double size))
                {
                    textBoxTextSample.FontSize = size;
                }
            }

            if (checkBoxBold.IsChecked == true)
            {
                textBoxTextSample.FontWeight = FontWeights.Bold;
            }
            else
            {
                textBoxTextSample.FontWeight = FontWeights.Normal;
            }
        }
    }
}
