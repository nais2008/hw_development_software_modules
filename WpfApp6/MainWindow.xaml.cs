using System.Windows;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFitnessTracker_Click(object sender, RoutedEventArgs e)
        {
            new Window1().Show();
        }

        private void OpenRecipeGenerator_Click(object sender, RoutedEventArgs e)
        {
            new Window2().Show();
        }

        private void OpenTripPlanner_Click(object sender, RoutedEventArgs e)
        {
            new Window3().Show();
        }
    }
}
