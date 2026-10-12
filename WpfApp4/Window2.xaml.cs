using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp4
{
    public enum TimerState
    {
        Ожидание,
        Работа,
        Пауза,
        Завершение
    }

    public class TimerPreset
    {
        public string Name { get; private set; }
        public TimeSpan Duration { get; private set; }

        public TimerPreset(string name, TimeSpan duration)
        {
            Name = name;
            Duration = duration;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public partial class Window2 : Window
    {
        private readonly DispatcherTimer timer;
        private TimeSpan totalTime;
        private TimeSpan remainingTime;
        private TimerState state;

        public Window2()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            PresetComboBox.ItemsSource = new List<TimerPreset>
            {
                new TimerPreset("5 минут", TimeSpan.FromMinutes(5)),
                new TimerPreset("10 минут", TimeSpan.FromMinutes(10)),
                new TimerPreset("30 минут", TimeSpan.FromMinutes(30)),
                new TimerPreset("1 час", TimeSpan.FromHours(1))
            };
            state = TimerState.Ожидание;
            UpdateDisplay();
        }

        private void PresetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TimerPreset preset = PresetComboBox.SelectedItem as TimerPreset;
            if (preset == null)
            {
                return;
            }

            timer.Stop();
            state = TimerState.Ожидание;
            TimeTextBox.Text = preset.Duration.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
            totalTime = preset.Duration;
            remainingTime = preset.Duration;
            UpdateDisplay();
        }

        private void TimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (state == TimerState.Ожидание)
            {
                TimeSpan value;
                if (TryParseTime(out value))
                {
                    totalTime = value;
                    remainingTime = value;
                }
            }

            UpdateDisplay();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (state == TimerState.Ожидание)
            {
                TimeSpan value;
                if (!TryParseTime(out value) || value <= TimeSpan.Zero)
                {
                    MessageBox.Show("Введите время в формате ЧЧ:ММ:СС.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                totalTime = value;
                remainingTime = value;
            }

            if (state == TimerState.Ожидание || state == TimerState.Пауза)
            {
                state = TimerState.Работа;
                timer.Start();
                UpdateDisplay();
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (state != TimerState.Работа)
            {
                return;
            }

            timer.Stop();
            state = TimerState.Пауза;
            UpdateDisplay();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            state = TimerState.Ожидание;
            TimeSpan value;
            if (TryParseTime(out value))
            {
                totalTime = value;
                remainingTime = value;
            }
            else
            {
                totalTime = TimeSpan.Zero;
                remainingTime = TimeSpan.Zero;
            }

            UpdateDisplay();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (remainingTime > TimeSpan.Zero)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
            }

            if (remainingTime <= TimeSpan.Zero)
            {
                remainingTime = TimeSpan.Zero;
                timer.Stop();
                state = TimerState.Завершение;
                UpdateDisplay();
                MessageBox.Show("Время вышло!", "Таймер", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            UpdateDisplay();
        }

        private bool TryParseTime(out TimeSpan value)
        {
            return TimeSpan.TryParseExact(
                TimeTextBox.Text,
                @"hh\:mm\:ss",
                CultureInfo.InvariantCulture,
                out value) && value >= TimeSpan.Zero;
        }

        private void UpdateDisplay()
        {
            TimeDisplayTextBlock.Text = remainingTime.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
            TimerProgressBar.Value = totalTime.TotalSeconds <= 0
                ? 0
                : (totalTime.TotalSeconds - remainingTime.TotalSeconds) / totalTime.TotalSeconds * 100;
            StateTextBlock.Text = "Состояние: " + state;
            TimeDisplayTextBlock.Foreground = state == TimerState.Завершение ? Brushes.Red : Brushes.Black;
            CompletionTextBlock.Visibility = state == TimerState.Завершение
                ? Visibility.Visible
                : Visibility.Collapsed;
            StartButton.IsEnabled = (state == TimerState.Ожидание || state == TimerState.Пауза) && IsValidTime();
            PauseButton.IsEnabled = state == TimerState.Работа;
        }

        private bool IsValidTime()
        {
            TimeSpan value;
            return TryParseTime(out value) && value > TimeSpan.Zero;
        }
    }
}
