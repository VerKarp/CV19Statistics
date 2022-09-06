using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CV19Statistics.Infrastructure.Commands;
using CV19Statistics.Models;
using CV19Statistics.Services;
using CV19Statistics.Services.Interfaces;
using CV19Statistics.ViewModels.Base;

namespace CV19Statistics.ViewModels
{
    internal class Cv19ConfirmedCountsChartViewModel : ViewModel
    {
        private readonly IDataService _dataService;

        #region Countries : IEnumerable<Country> - Countries

        /// <summary>Countries</summary>
        private IEnumerable<Country> _countries;

        /// <summary>Countries</summary>
        public IEnumerable<Country> Countries
        {
            get => _countries;
            set => Set(ref _countries, value);
        }

        #endregion

        #region SelectedCountry : Country - Selected country

        /// <summary>Selected country</summary>
        private Country _selectedCountry;

        /// <summary>Selected country</summary>
        public Country SelectedCountry
        {
            get => _selectedCountry;
            set => Set(ref _selectedCountry, value);
        }

        #endregion

        #region StatisticDataType : Cv19StatisticsDataType - Statistic data type

        /// <summary>Statistic data type</summary>
        private Cv19StatisticsDataType _statisticsDataType = Cv19StatisticsDataType.Confirmed;

        /// <summary>Statistic data type</summary>
        public Cv19StatisticsDataType StatisticsDataType
        {
            get => _statisticsDataType;
            set => Set(ref _statisticsDataType, value);
        }

        #endregion

        #region Commands

        public ICommand RefreshDataCommand { get; }
        private void OnRefreshDataCommandExecuted(object p) => Countries = _dataService.GetData(_statisticsDataType);

        public ICommand SwitchStatisticDataTypeCommand { get; }
        private void OnSwitchStatisticDataTypeCommandExecuted(object p)
        {
            if (p is null) return;

            switch (p as string)
            {
                case "Confirmed":
                    StatisticsDataType = Cv19StatisticsDataType.Confirmed;
                    break;

                case "Deaths":
                    StatisticsDataType = Cv19StatisticsDataType.Deaths;
                    break;

                case "Recovered":
                    StatisticsDataType = Cv19StatisticsDataType.Recovered;
                    break;
            }

            OnRefreshDataCommandExecuted(this);
        }

        #endregion

        public Cv19ConfirmedCountsChartViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Countries = _dataService.GetData(_statisticsDataType);

            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);
            SwitchStatisticDataTypeCommand = new LambdaCommand(OnSwitchStatisticDataTypeCommandExecuted);

            #endregion
        }
    }
}