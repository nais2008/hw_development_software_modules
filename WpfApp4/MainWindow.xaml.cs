using System.Windows;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenExpenses_Click(object sender, RoutedEventArgs e)
        {
            new Window1().ShowDialog();
        }

        private void OpenTimer_Click(object sender, RoutedEventArgs e)
        {
            new Window2().ShowDialog();
        }

        private void OpenQuiz_Click(object sender, RoutedEventArgs e)
        {
            new Window3().ShowDialog();
        }
    }
}
