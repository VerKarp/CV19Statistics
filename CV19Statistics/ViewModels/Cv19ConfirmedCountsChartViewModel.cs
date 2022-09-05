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

        #region Commands

        public ICommand RefreshDataCommand { get; }
        private void OnRefreshDataCommandExecuted(object p) => Countries = _dataService.GetData();

        #endregion

        public Cv19ConfirmedCountsChartViewModel(IDataService dataService)
        {
            _dataService = dataService;

            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

            #endregion
        }
    }
}