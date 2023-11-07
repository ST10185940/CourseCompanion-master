using CourseCompanion.ViewModels;
using System.Windows;

namespace CourseCompanion.Views
{
    /// <summary>
    /// Interaction logic for AddModule.xaml
    /// </summary>
    public partial class AddModule : Window
    {
        public AddModule()
        {
            InitializeComponent();
            AddModuleViewModel viewModel = new AddModuleViewModel();
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
