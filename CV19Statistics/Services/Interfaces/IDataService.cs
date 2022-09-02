using System.Collections.Generic;
using CV19Statistics.Models;

namespace CV19Statistics.Services.Interfaces
{
    internal interface IDataService
    {
        IEnumerable<Country> GetData();
    }
}