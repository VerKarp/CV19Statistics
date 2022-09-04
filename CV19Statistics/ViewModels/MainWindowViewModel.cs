using CV19Statistics.Services;
using CV19Statistics.Services.Interfaces;
using CV19Statistics.ViewModels.Base;

namespace CV19Statistics.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public Cv19ConfirmedCountsChartViewModel Cv19ConfirmedCountsChartViewModel { get; set; }
        
        public MainWindowViewModel()
        {
            IDataService dataService = new DataService();
            Cv19ConfirmedCountsChartViewModel = new Cv19ConfirmedCountsChartViewModel(dataService);
        }
    }
}