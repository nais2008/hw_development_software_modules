using System.Globalization;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WpfApp5
{
    public enum CoffeeType
    {
        Эспрессо,
        Американо,
        Капучино,
        Латте,
        Раф
    }

    public enum VolumeType
    {
        Маленький,
        Средний,
        Большой
    }

    public enum SyrupType
    {
        Карамель,
        Ваниль,
        Орех,
        БезСиропа
    }

    public class Coffee
    {
        public CoffeeType Beverage { get; set; }

        public VolumeType Volume { get; set; }

        public SyrupType Syrup { get; set; }

        public bool HasSyrup { get; set; }

        public bool HasCinnamon { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerPortion
        {
            get
            {
                decimal price = GetBeveragePrice() + GetVolumePrice();

                if (HasSyrup && Syrup != SyrupType.БезСиропа)
                {
                    price += 40m;
                }

                if (HasCinnamon)
                {
                    price += 20m;
                }

                return price;
            }
        }

        public decimal TotalPrice
        {
            get { return PricePerPortion * Quantity; }
        }

        public override string ToString()
        {
            string syrupText = HasSyrup && Syrup != SyrupType.БезСиропа
                ? ", сироп: " + Syrup.ToString()
                : string.Empty;
            string cinnamonText = HasCinnamon ? ", корица" : string.Empty;

            return string.Format(
                CultureInfo.CurrentCulture,
                "{0}, {1} мл{2}{3}, {4} порц. — {5:N2} руб.",
                Beverage.ToString(),
                GetVolumeMilliliters(),
                syrupText,
                cinnamonText,
                Quantity,
                TotalPrice);
        }

        private decimal GetBeveragePrice()
        {
            switch (Beverage)
            {
                case CoffeeType.Эспрессо:
                    return 150m;
                case CoffeeType.Американо:
                    return 180m;
                case CoffeeType.Капучино:
                    return 220m;
                case CoffeeType.Латте:
                    return 250m;
                case CoffeeType.Раф:
                    return 280m;
                default:
                    return 0m;
            }
        }

        private decimal GetVolumePrice()
        {
            switch (Volume)
            {
                case VolumeType.Средний:
                    return 50m;
                case VolumeType.Большой:
                    return 100m;
                default:
                    return 0m;
            }
        }

        private int GetVolumeMilliliters()
        {
            switch (Volume)
            {
                case VolumeType.Средний:
                    return 300;
                case VolumeType.Большой:
                    return 400;
                default:
                    return 200;
            }
        }
    }

    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Coffee> order = new ObservableCollection<Coffee>();

        public MainWindow()
        {
            InitializeComponent();
            BeverageComboBox.ItemsSource = System.Enum.GetValues(typeof(CoffeeType));
            SyrupComboBox.ItemsSource = System.Enum.GetValues(typeof(SyrupType));
            SyrupComboBox.SelectedItem = SyrupType.БезСиропа;
            OrderListBox.ItemsSource = order;
            RecalculateOrder();
        }

        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (BeverageComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите напиток.", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int quantity;
            if (!int.TryParse(QuantityTextBox.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Количество порций должно быть положительным целым числом.", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SyrupType syrup = SyrupComboBox.SelectedItem == null
                ? SyrupType.БезСиропа
                : (SyrupType)SyrupComboBox.SelectedItem;

            Coffee coffee = new Coffee
            {
                Beverage = (CoffeeType)BeverageComboBox.SelectedItem,
                Volume = GetSelectedVolume(),
                Syrup = syrup,
                HasSyrup = SyrupCheckBox.IsChecked == true,
                HasCinnamon = CinnamonCheckBox.IsChecked == true,
                Quantity = quantity
            };

            order.Add(coffee);
            OrderListBox.SelectedItem = coffee;
            RecalculateOrder();
        }

        private void RemoveSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            Coffee selectedCoffee = OrderListBox.SelectedItem as Coffee;
            if (selectedCoffee == null)
            {
                MessageBox.Show("Выберите позицию для удаления.", "Удаление позиции", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            order.Remove(selectedCoffee);
            RecalculateOrder();
        }

        private VolumeType GetSelectedVolume()
        {
            if (MediumVolumeRadioButton.IsChecked == true)
            {
                return VolumeType.Средний;
            }

            if (LargeVolumeRadioButton.IsChecked == true)
            {
                return VolumeType.Большой;
            }

            return VolumeType.Маленький;
        }

        private void RecalculateOrder()
        {
            int totalDrinks = order.Sum(coffee => coffee.Quantity);
            decimal totalCost = order.Sum(coffee => coffee.TotalPrice);

            if (totalDrinks > 3)
            {
                totalCost *= 0.9m;
            }

            TotalCostTextBlock.Text = totalCost.ToString("N2", CultureInfo.CurrentCulture) + " руб.";
            TotalDrinksTextBlock.Text = totalDrinks.ToString(CultureInfo.CurrentCulture);
        }
    }
}
