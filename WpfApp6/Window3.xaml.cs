using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp6
{
    public partial class Window3 : Window
    {
        public enum ExpenseCategory
        {
            Транспорт,
            Проживание,
            Питание,
            Развлечения,
            Прочее
        }

        public class Expense
        {
            public string Name { get; set; }
            public ExpenseCategory Category { get; set; }
            public double Cost { get; set; }
            public DateTime Date { get; set; }

            public override string ToString()
            {
                return Date.ToString("dd.MM.yyyy") + " — " + Name + " (" + Category + "): " + Cost.ToString("0.##") + " руб.";
            }
        }

        private readonly List<Expense> expenses = new List<Expense>();
        private readonly List<Expense> visibleExpenses = new List<Expense>();
        private readonly Dictionary<ExpenseCategory, int> categoryPriorities = new Dictionary<ExpenseCategory, int>();
        private bool changingPriority;

        public Window3()
        {
            InitializeComponent();
            foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
            {
                ExpenseCategoryComboBox.Items.Add(category);
                categoryPriorities[category] = 3;
            }
            ExpenseCategoryComboBox.SelectedIndex = 0;
            ExpenseDatePicker.SelectedDate = DateTime.Today;
            UpdatePriorityControls();
            RefreshExpenses();
            UpdateStatistics();
        }

        private void ExpenseDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExpenseDatePicker.SelectedDate.HasValue && ExpenseDatePicker.SelectedDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Дата расхода не может быть в прошлом.");
                ExpenseDatePicker.SelectedDate = DateTime.Today;
            }
        }

        private void AddExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ExpenseNameTextBox.Text.Trim();
            double cost;

            if (name == "")
            {
                MessageBox.Show("Введите название расхода.");
                return;
            }

            if (!(ExpenseCategoryComboBox.SelectedItem is ExpenseCategory))
            {
                MessageBox.Show("Выберите категорию расхода.");
                return;
            }

            if (!TryParseNumber(ExpenseCostTextBox.Text, out cost) || cost <= 0)
            {
                MessageBox.Show("Стоимость должна быть положительным числом.");
                return;
            }

            if (!ExpenseDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату расхода.");
                return;
            }

            DateTime date = ExpenseDatePicker.SelectedDate.Value.Date;
            if (date < DateTime.Today)
            {
                MessageBox.Show("Дата расхода не может быть в прошлом.");
                return;
            }

            expenses.Add(new Expense
            {
                Name = name,
                Category = (ExpenseCategory)ExpenseCategoryComboBox.SelectedItem,
                Cost = cost,
                Date = date
            });

            ExpenseNameTextBox.Clear();
            ExpenseCostTextBox.Clear();
            RefreshExpenses();
            UpdateStatistics();
        }

        private void DeleteExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            Expense selectedExpense = ExpensesListBox.SelectedItem as Expense;
            if (selectedExpense == null)
            {
                MessageBox.Show("Выберите расход для удаления.");
                return;
            }

            expenses.Remove(selectedExpense);
            RefreshExpenses();
            UpdateStatistics();
        }

        private void RefreshExpenses()
        {
            visibleExpenses.Clear();
            foreach (Expense expense in expenses.OrderByDescending(item => item.Date))
            {
                if (ExpensiveOnlyCheckBox.IsChecked == true && expense.Cost <= 1000)
                {
                    continue;
                }
                visibleExpenses.Add(expense);
            }

            ExpensesListBox.ItemsSource = null;
            ExpensesListBox.ItemsSource = visibleExpenses;
        }

        private void ExpenseFilter_Changed(object sender, RoutedEventArgs e)
        {
            if (IsInitialized)
            {
                RefreshExpenses();
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            double budget;
            if (!TryParseNumber(BudgetTextBox.Text, out budget) || budget <= 0)
            {
                MessageBox.Show("Бюджет должен быть положительным числом.");
                return;
            }
            UpdateStatistics();
        }

        private void BudgetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsInitialized)
            {
                UpdateStatistics();
            }
        }

        private void ExpenseCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
            {
                UpdatePriorityControls();
                UpdateStatistics();
            }
        }

        private void PrioritySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (changingPriority || !IsInitialized || !(ExpenseCategoryComboBox.SelectedItem is ExpenseCategory))
            {
                return;
            }

            ExpenseCategory category = (ExpenseCategory)ExpenseCategoryComboBox.SelectedItem;
            categoryPriorities[category] = (int)Math.Round(PrioritySlider.Value);
            UpdatePriorityText();
            UpdateStatistics();
        }

        private void UpdatePriorityControls()
        {
            if (!(ExpenseCategoryComboBox.SelectedItem is ExpenseCategory))
            {
                return;
            }

            ExpenseCategory category = (ExpenseCategory)ExpenseCategoryComboBox.SelectedItem;
            changingPriority = true;
            PrioritySlider.Value = categoryPriorities[category];
            changingPriority = false;
            UpdatePriorityText();
        }

        private void UpdatePriorityText()
        {
            if (PriorityTextBlock == null || !(ExpenseCategoryComboBox.SelectedItem is ExpenseCategory))
            {
                return;
            }

            ExpenseCategory category = (ExpenseCategory)ExpenseCategoryComboBox.SelectedItem;
            PriorityTextBlock.Text = "Приоритет категории " + category + ": " + categoryPriorities[category];
        }

        private void UpdateStatistics()
        {
            if (TotalExpensesTextBlock == null)
            {
                return;
            }

            double total = expenses.Sum(expense => expense.Cost);
            double budget;
            bool hasBudget = TryParseNumber(BudgetTextBox.Text, out budget) && budget > 0;
            double balance = hasBudget ? budget - total : 0;
            double percent = hasBudget ? total / budget * 100 : 0;

            TotalExpensesTextBlock.Text = "Общая сумма расходов: " + total.ToString("0.##") + " руб.";
            BudgetBalanceTextBlock.Text = hasBudget ? "Остаток бюджета: " + balance.ToString("0.##") + " руб." : "Остаток бюджета: введите бюджет";
            BudgetPercentTextBlock.Text = hasBudget ? "Использовано бюджета: " + percent.ToString("0.##") + "%" : "Использовано бюджета: —";

            if (!hasBudget)
            {
                BudgetStatusTextBlock.Text = "Введите положительный бюджет для расчёта статуса.";
                BudgetStatusTextBlock.Foreground = Brushes.Gray;
            }
            else if (balance < 0)
            {
                BudgetStatusTextBlock.Text = "Бюджет превышен на " + Math.Abs(balance).ToString("0.##") + " руб.";
                BudgetStatusTextBlock.Foreground = Brushes.Red;
            }
            else if (percent > 80)
            {
                BudgetStatusTextBlock.Text = "Внимание: использовано больше 80% бюджета.";
                BudgetStatusTextBlock.Foreground = Brushes.Yellow;
            }
            else
            {
                BudgetStatusTextBlock.Text = "Бюджет под контролем.";
                BudgetStatusTextBlock.Foreground = Brushes.Green;
            }

            UpdateCategoryStatistics(total);
            UpdateTripStatistics(total);
            UpdateRecommendation(total, hasBudget, balance, percent);
        }

        private void UpdateCategoryStatistics(double total)
        {
            Dictionary<ExpenseCategory, double> categoryTotals = new Dictionary<ExpenseCategory, double>();
            foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
            {
                categoryTotals[category] = expenses
                    .Where(expense => expense.Category == category)
                    .Sum(expense => expense.Cost);
            }

            ExpenseCategory mostExpensiveCategory = ExpenseCategory.Прочее;
            double maximum = 0;
            foreach (KeyValuePair<ExpenseCategory, double> item in categoryTotals)
            {
                if (item.Value > maximum)
                {
                    maximum = item.Value;
                    mostExpensiveCategory = item.Key;
                }
            }

            MostExpensiveCategoryTextBlock.Text = maximum > 0
                ? "Самая затратная категория: " + mostExpensiveCategory + " (" + maximum.ToString("0.##") + " руб.)"
                : "Самая затратная категория: нет данных";

            List<string> distribution = new List<string>();
            foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
            {
                double amount = categoryTotals[category];
                double percent = total > 0 ? amount / total * 100 : 0;
                distribution.Add(category + ": " + amount.ToString("0.##") + " руб. (" + percent.ToString("0.##") + "%)");
            }
            CategoryDistributionTextBlock.Text = "Распределение расходов: " + string.Join("; ", distribution);
        }

        private void UpdateTripStatistics(double total)
        {
            if (expenses.Count == 0)
            {
                TripDurationTextBlock.Text = "Продолжительность поездки: нет данных";
                AverageDailyExpenseTextBlock.Text = "Средний расход в день: нет данных";
                return;
            }

            DateTime firstDate = expenses.Min(expense => expense.Date);
            DateTime lastDate = expenses.Max(expense => expense.Date);
            int duration = (lastDate - firstDate).Days + 1;
            double average = total / duration;
            TripDurationTextBlock.Text = "Продолжительность поездки: " + duration + " дн. (" + firstDate.ToString("dd.MM.yyyy") + " — " + lastDate.ToString("dd.MM.yyyy") + ")";
            AverageDailyExpenseTextBlock.Text = "Средний расход в день: " + average.ToString("0.##") + " руб." + (average > 5000 ? " — предупреждение: расход выше 5000 руб." : "");
        }

        private void UpdateRecommendation(double total, bool hasBudget, double balance, double percent)
        {
            if (!hasBudget)
            {
                SavingRecommendationTextBlock.Text = "Рекомендация по экономии появится после ввода бюджета.";
                return;
            }

            if (balance < 0)
            {
                List<string> recommendations = new List<string>();
                foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
                {
                    double categoryTotal = expenses
                        .Where(expense => expense.Category == category)
                        .Sum(expense => expense.Cost);
                    int priority = categoryPriorities[category];
                    if (categoryTotal > 0 && priority <= 2)
                    {
                        double possibleSaving = priority == 1 ? categoryTotal * 0.3 : categoryTotal * 0.15;
                        recommendations.Add("Попробуйте сократить расходы в категории \"" + category + "\" (приоритет " + priority + ", потенциальная экономия " + possibleSaving.ToString("0.##") + " руб.)");
                    }
                }

                SavingRecommendationTextBlock.Text = recommendations.Count > 0
                    ? string.Join("\n", recommendations)
                    : "Бюджет превышен. Назначьте приоритет 1–2 категориям, которые можно оптимизировать.";
            }
            else if (percent > 80)
            {
                SavingRecommendationTextBlock.Text = "Создайте финансовый резерв: использовано больше 80% бюджета.";
            }
            else
            {
                SavingRecommendationTextBlock.Text = "Расходы находятся в безопасной зоне. Продолжайте контролировать бюджет.";
            }
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
