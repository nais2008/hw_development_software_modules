using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp6
{
    public partial class Window1 : Window
    {
        public enum WorkoutType
        {
            Бег,
            Плавание,
            Велосипед,
            Силовая,
            Йога
        }

        public class Workout
        {
            public DateTime Date { get; set; }
            public WorkoutType Type { get; set; }
            public double Duration { get; set; }
            public double Distance { get; set; }
            public double Calories { get; set; }

            public override string ToString()
            {
                string distanceText = Distance > 0 ? ", дистанция: " + Distance.ToString("0.##") + " км" : "";
                return Date.ToString("dd.MM.yyyy") + " — " + Type + ", " + Duration.ToString("0.##") + " мин" + distanceText + ", " + Calories.ToString("0") + " ккал";
            }
        }

        private readonly List<Workout> workouts = new List<Workout>();
        private readonly List<Workout> visibleWorkouts = new List<Workout>();

        public Window1()
        {
            InitializeComponent();
            WorkoutDatePicker.SelectedDate = DateTime.Today;
            foreach (WorkoutType type in Enum.GetValues(typeof(WorkoutType)))
            {
                WorkoutTypeComboBox.Items.Add(type);
            }
            WorkoutTypeComboBox.SelectedIndex = 0;
            UpdateDistanceState();
            UpdateStatistics();
        }

        private void WorkoutDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkoutDatePicker.SelectedDate.HasValue && WorkoutDatePicker.SelectedDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Дата тренировки не может быть в будущем.");
                WorkoutDatePicker.SelectedDate = DateTime.Today;
            }
        }

        private void WorkoutTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDistanceState();
        }

        private void UpdateDistanceState()
        {
            if (DistanceTextBox == null || WorkoutTypeComboBox == null)
            {
                return;
            }

            bool needsDistance = WorkoutTypeComboBox.SelectedItem is WorkoutType &&
                ((WorkoutType)WorkoutTypeComboBox.SelectedItem == WorkoutType.Бег ||
                 (WorkoutType)WorkoutTypeComboBox.SelectedItem == WorkoutType.Плавание ||
                 (WorkoutType)WorkoutTypeComboBox.SelectedItem == WorkoutType.Велосипед);

            DistanceTextBox.IsEnabled = needsDistance;
            if (!needsDistance)
            {
                DistanceTextBox.Clear();
            }
        }

        private void AddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;
            double duration;
            double weight;
            double distance = 0;

            if (!WorkoutDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату тренировки.");
                return;
            }

            date = WorkoutDatePicker.SelectedDate.Value.Date;
            if (date > DateTime.Today)
            {
                MessageBox.Show("Дата тренировки не может быть в будущем.");
                return;
            }

            if (!(WorkoutTypeComboBox.SelectedItem is WorkoutType))
            {
                MessageBox.Show("Выберите тип тренировки.");
                return;
            }

            if (!TryParseNumber(DurationTextBox.Text, out duration) || duration < 5 || duration > 300)
            {
                MessageBox.Show("Длительность должна быть числом от 5 до 300 минут.");
                return;
            }

            if (!TryParseNumber(WeightTextBox.Text, out weight) || weight < 30 || weight > 250)
            {
                MessageBox.Show("Вес должен быть числом от 30 до 250 кг.");
                return;
            }

            WorkoutType type = (WorkoutType)WorkoutTypeComboBox.SelectedItem;
            bool needsDistance = type == WorkoutType.Бег || type == WorkoutType.Плавание || type == WorkoutType.Велосипед;
            if (needsDistance && (!TryParseNumber(DistanceTextBox.Text, out distance) || distance < 0.5 || distance > 100))
            {
                MessageBox.Show("Дистанция должна быть числом от 0,5 до 100 км.");
                return;
            }

            double calories = CalculateCalories(type, weight, duration, distance);
            workouts.Add(new Workout
            {
                Date = date,
                Type = type,
                Duration = duration,
                Distance = distance,
                Calories = calories
            });

            RefreshWorkoutList();
            UpdateStatistics();
            DurationTextBox.Clear();
            DistanceTextBox.Clear();
        }

        private double CalculateCalories(WorkoutType type, double weight, double duration, double distance)
        {
            double calories;
            if (type == WorkoutType.Бег)
            {
                calories = weight * 1.036 * distance;
            }
            else if (type == WorkoutType.Плавание)
            {
                calories = weight * 0.9 * duration / 10;
            }
            else if (type == WorkoutType.Велосипед)
            {
                calories = weight * 0.5 * distance;
            }
            else if (type == WorkoutType.Силовая)
            {
                calories = weight * 0.1 * duration;
            }
            else
            {
                calories = weight * 0.05 * duration;
            }

            return Math.Round(calories, 2);
        }

        private void DeleteWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            Workout selectedWorkout = WorkoutsListBox.SelectedItem as Workout;
            if (selectedWorkout == null)
            {
                MessageBox.Show("Выберите тренировку для удаления.");
                return;
            }

            workouts.Remove(selectedWorkout);
            RefreshWorkoutList();
            UpdateStatistics();
        }

        private void RefreshWorkoutList()
        {
            visibleWorkouts.Clear();
            DateTime weekStart = GetWeekStart(DateTime.Today);
            DateTime weekEnd = weekStart.AddDays(6).Date;

            foreach (Workout workout in workouts.OrderByDescending(item => item.Date))
            {
                if (CurrentWeekCheckBox.IsChecked == true && (workout.Date < weekStart || workout.Date > weekEnd))
                {
                    continue;
                }
                visibleWorkouts.Add(workout);
            }

            WorkoutsListBox.ItemsSource = null;
            WorkoutsListBox.ItemsSource = visibleWorkouts;
        }

        private void UpdateStatistics()
        {
            if (GoalSlider == null || GoalTextBlock == null)
            {
                return;
            }

            DateTime weekStart = GetWeekStart(DateTime.Today);
            DateTime weekEnd = weekStart.AddDays(6).Date;
            double weeklyCalories = workouts
                .Where(workout => workout.Date >= weekStart && workout.Date <= weekEnd)
                .Sum(workout => workout.Calories);
            double goal = GoalSlider.Value;
            double percent = goal > 0 ? weeklyCalories / goal * 100 : 0;

            GoalTextBlock.Text = "Недельная цель: " + goal.ToString("0") + " ккал";
            WeeklyCaloriesTextBlock.Text = "Сожжено за текущую неделю: " + weeklyCalories.ToString("0.##") + " ккал";
            GoalPercentTextBlock.Text = "Выполнение цели: " + percent.ToString("0.##") + "%";
            GoalMessageTextBlock.Text = percent >= 100 ? "Цель достигнута!" : "До цели осталось: " + Math.Max(0, goal - weeklyCalories).ToString("0.##") + " ккал";
        }

        private DateTime GetWeekStart(DateTime date)
        {
            int daysFromMonday = ((int)date.DayOfWeek + 6) % 7;
            return date.Date.AddDays(-daysFromMonday);
        }

        private void CurrentWeekCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            RefreshWorkoutList();
        }

        private void GoalSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateStatistics();
        }

        private bool TryParseNumber(string text, out double number)
        {
            string normalized = (text ?? "").Trim().Replace('.', ',');
            if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.CurrentCulture, out number))
            {
                return true;
            }

            return double.TryParse((text ?? "").Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out number);
        }
    }
}
