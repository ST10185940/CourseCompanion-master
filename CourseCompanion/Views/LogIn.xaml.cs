using CourseCompanion.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Controls;

namespace CourseCompanion.Views
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        public LogIn()
        {
            InitializeComponent();
            LogInViewModel viewModel = new LogInViewModel();
            DataContext = viewModel;

            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "LoginSuccess" && viewModel.LoginSuccess)
                {
                    Dispatcher.Invoke(() =>
                    {                
                        MainFrame.NavigationService.Navigate( new HomeView());
                    });
                }
            };

        }
    }
}
