using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp4
{
    public enum ExpenseCategory
    {
        Еда,
        Транспорт,
        Развлечения,
        Другое
    }

    public class ExpenseRecord
    {
        public string Name { get; private set; }
        public decimal Amount { get; private set; }
        public ExpenseCategory Category { get; private set; }

        public ExpenseRecord(string name, decimal amount, ExpenseCategory category)
        {
            Name = name;
            Amount = amount;
            Category = category;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} - {1:0.##} руб. ({2})", Name, Amount, Category);
        }
    }

    public partial class Window1 : Window
    {
        private readonly List<ExpenseRecord> expenses = new List<ExpenseRecord>();

        public Window1()
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(ExpenseCategory));
            CategoryComboBox.SelectedIndex = 0;
            UpdateControls();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateControls();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            decimal amount;
            if (string.IsNullOrWhiteSpace(PurchaseNameTextBox.Text) || !TryGetAmount(out amount))
            {
                return;
            }

            ExpenseCategory category = (ExpenseCategory)CategoryComboBox.SelectedItem;
            ExpenseRecord record = new ExpenseRecord(PurchaseNameTextBox.Text.Trim(), amount, category);
            expenses.Add(record);
            ExpensesListBox.Items.Add(record);
            PurchaseNameTextBox.Clear();
            AmountTextBox.Clear();
            UpdateControls();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ExpenseRecord selected = ExpensesListBox.SelectedItem as ExpenseRecord;
            if (selected == null)
            {
                return;
            }

            expenses.Remove(selected);
            ExpensesListBox.Items.Remove(selected);
            UpdateControls();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (expenses.Count == 0)
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Очистить весь список расходов?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                expenses.Clear();
                ExpensesListBox.Items.Clear();
                UpdateControls();
            }
        }

        private void ExpensesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteButton.IsEnabled = ExpensesListBox.SelectedItem != null;
        }

        private bool TryGetAmount(out decimal amount)
        {
            return decimal.TryParse(
                AmountTextBox.Text,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out amount) && amount >= 0;
        }

        private void UpdateControls()
        {
            decimal amount;
            AddButton.IsEnabled = !string.IsNullOrWhiteSpace(PurchaseNameTextBox.Text) && TryGetAmount(out amount);
            TotalTextBlock.Text = string.Format(
                CultureInfo.CurrentCulture,
                "Общая сумма: {0:0.##} руб.",
                GetTotal());
            CountTextBlock.Text = "Количество записей: " + expenses.Count;
            DeleteButton.IsEnabled = ExpensesListBox.SelectedItem != null;
        }

        private decimal GetTotal()
        {
            decimal total = 0;
            foreach (ExpenseRecord expense in expenses)
            {
                total += expense.Amount;
            }

            return total;
        }

    }
}
