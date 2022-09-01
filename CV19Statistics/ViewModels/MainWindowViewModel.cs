using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}