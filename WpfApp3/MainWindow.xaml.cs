using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace WpfApp3
{
    public class Visit
    {
        public string ClientName { get; set; }

        public TrainingType TrainingType { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public HallType Hall { get; set; }

        public override string ToString()
        {
            return $"{Date:dd.MM.yyyy} | " +
                   $"{ClientName} | " +
                   $"{TrainingType} | " +
                   $"{Duration} мин | " +
                   $"{GetHallName()}";
        }

        private string GetHallName()
        {
            if (Hall == HallType.VIP)
            {
                return "VIP-зал";
            }

            if (Hall == HallType.Бассейн)
            {
                return "Бассейн";
            }

            return "Основной зал";
        }
    }

    public enum TrainingType
    {
        Силовая,
        Кардио,
        Йога,
        Бассейн
    }

    public enum HallType
    {
        Основной,
        VIP,
        Бассейн
    }

    public partial class MainWindow : Window
    {
        private List<Visit> visits = new List<Visit>();

        public MainWindow()
        {
            InitializeComponent();
            TrainingTypeComboBox.ItemsSource =
                Enum.GetValues(typeof(TrainingType));

            UpdateData();
        }

        private void AddVisitButton_Click(object sender, RoutedEventArgs e)
        {
            string clientName = NameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(clientName))
            {
                MessageBox.Show(
                    "Введите имя клиента.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                return;
            }

            if (!DateTime.TryParseExact(
                    DateTextBox.Text,
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date))
            {
                MessageBox.Show(
                    "Введите дату в формате ДД.ММ.ГГГГ.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                return;
            }

            if (!int.TryParse(DurationTextBox.Text, out int duration) ||
                duration <= 0)
            {
                MessageBox.Show(
                    "Длительность должна быть положительным числом.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                return;
            }

            TrainingType trainingType =
                (TrainingType)TrainingTypeComboBox.SelectedItem;

            HallType hall;

            if (VipHallRadioButton.IsChecked == true)
            {
                hall = HallType.VIP;
            }
            else if (PoolHallRadioButton.IsChecked == true)
            {
                hall = HallType.Бассейн;
            }
            else
            {
                hall = HallType.Основной;
            }

            Visit visit = new Visit
            {
                ClientName = clientName,
                TrainingType = trainingType,
                Date = date,
                Duration = duration,
                Hall = hall
            };

            visits.Add(visit);

            ClearFields();
            UpdateData();
        }

        private void DeleteVisitButton_Click(object sender, RoutedEventArgs e)
        {
            Visit selectedVisit =
                VisitsListBox.SelectedItem as Visit;

            if (selectedVisit == null)
            {
                MessageBox.Show(
                    "Выберите посещение для удаления.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );

                return;
            }

            visits.Remove(selectedVisit);

            UpdateData();
        }

        private void LongVisitsCheckBox_Changed(
            object sender,
            RoutedEventArgs e)
        {
            UpdateList();
        }

        private void UpdateData()
        {
            UpdateList();
            UpdateStatistics();
        }

        private void UpdateList()
        {
            VisitsListBox.ItemsSource = null;

            if (LongVisitsCheckBox.IsChecked == true)
            {
                VisitsListBox.ItemsSource =
                    visits
                        .Where(v => v.Duration > 60)
                        .ToList();
            }
            else
            {
                VisitsListBox.ItemsSource = visits;
            }
        }

        private void UpdateStatistics()
        {
            int count = visits.Count;

            int totalDuration =
                visits.Sum(v => v.Duration);

            double price =
                CalculatePrice();

            CountTextBlock.Text =
                $"Количество посещений: {count}";

            DurationTextBlock.Text =
                $"Общая длительность: {totalDuration} мин";

            PriceTextBlock.Text =
                $"Стоимость абонемента: {price:F0} руб.";
        }

        private double CalculatePrice()
        {
            int count = visits.Count;

            if (count == 0)
            {
                return 0;
            }

            double price;

            if (count <= 5)
            {
                price = 2000;
            }
            else if (count <= 10)
            {
                price = 3500;
            }
            else if (count <= 15)
            {
                price = 4500;
            }
            else
            {
                price = 5000;
            }

            bool hasVip =
                visits.Any(v => v.Hall == HallType.VIP);

            bool hasPoolHall =
                visits.Any(v => v.Hall == HallType.Бассейн);

            if (hasVip)
            {
                price += 1500;
            }

            if (hasPoolHall)
            {
                price += 1000;
            }

            bool hasDiscount =
                visits.Any(v =>
                    v.TrainingType == TrainingType.Йога ||
                    v.TrainingType == TrainingType.Бассейн);

            if (hasDiscount)
            {
                price *= 0.9;
            }

            return price;
        }

        private void ClearFields()
        {
            NameTextBox.Clear();
            DateTextBox.Clear();
            DurationTextBox.Clear();

            TrainingTypeComboBox.SelectedIndex = 0;
            MainHallRadioButton.IsChecked = true;
        }
    }
}