using CV19Statistics.Views.Windows;
using System.Windows;
using CV19Statistics.ViewModels;

namespace CV19Statistics
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };

            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}