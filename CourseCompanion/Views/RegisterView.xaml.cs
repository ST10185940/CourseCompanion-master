using CourseCompanion.ViewModels;
using System.Windows.Controls;

namespace CourseCompanion.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Page
    {
        public RegisterView()
        {
            InitializeComponent();
            RegisterViewModel view = new RegisterViewModel();
            DataContext = view;

            view.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "RegistrationSuccess" && view.RegistrationSuccess)
                {
                    Dispatcher.Invoke(() =>
                    {
                        MainFrame.NavigationService.Navigate(new HomeView());
                    });
                }
            };
        }
    }
}
