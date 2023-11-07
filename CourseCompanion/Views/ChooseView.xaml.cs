using System.Windows;
using System.Windows.Controls;

namespace CourseCompanion.Views
{
    /// <summary>
    /// Interaction logic for ChooseView.xaml
    /// </summary>
    public partial class ChooseView : Page
    {
        public ChooseView()
        {
            InitializeComponent();
        }

        public void Register(object sender, RoutedEventArgs e)
        {
            RegisterView register = new RegisterView();
            MainFrame.NavigationService.Navigate(register);
        }

        public void LogIn(object sender, RoutedEventArgs e)
        {
            LogIn login = new LogIn();
            MainFrame.NavigationService.Navigate(login);
        }
    }
}
