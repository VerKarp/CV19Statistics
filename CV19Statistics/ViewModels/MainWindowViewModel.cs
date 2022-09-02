using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV19Statistics.Models;
using CV19Statistics.Services;
using CV19Statistics.Services.Interfaces;
using CV19Statistics.ViewModels.Base;

namespace CV19Statistics.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Title : string - Window title

        /// <summary>Window title</summary>
        private string _title = "CV19 Statistics";

        /// <summary>Window title</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        #region Countries : List<Country> - Countries

        /// <summary>Countries</summary>
        private IEnumerable<Country> _countries;

        /// <summary>Countries</summary>
        public IEnumerable<Country> Countries
        {
            get => _countries;
            set => Set(ref _countries, value);
        }

        #endregion

        private IDataService _dataService = new DataService();

        public MainWindowViewModel()
        {
            Countries = _dataService.GetData();
        }
    }
}