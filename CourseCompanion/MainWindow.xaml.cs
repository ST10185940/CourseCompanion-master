using CourseCompanion.Views;
using System.Windows;

namespace CourseCompanion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Start(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ChooseView());
            MainFrame.Visibility = Visibility.Visible;
        }
    }

}
