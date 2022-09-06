using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CV19Statistics.Models;
using CV19Statistics.Services.Interfaces;

namespace CV19Statistics.Services
{
    internal class DataService : IDataService
    {
        private static Cv19StatisticsDataType dataType;
        private static string _dataSourceAddress;
        private const string _dataSourceConfirmedAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        private const string _dataSourceDeathsAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_deaths_global.csv";
        private const string _dataSourceRecoveredAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_recovered_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            switch (dataType)
            {
                case Cv19StatisticsDataType.Confirmed:
                    _dataSourceAddress = _dataSourceConfirmedAddress;
                    break;

                case Cv19StatisticsDataType.Deaths:
                    _dataSourceAddress = _dataSourceDeathsAddress;
                    break;

                case Cv19StatisticsDataType.Recovered:
                    _dataSourceAddress = _dataSourceRecoveredAddress;
                    break;
            }

            HttpClient client = new();
            HttpResponseMessage respone = await client.GetAsync(_dataSourceAddress,
                HttpCompletionOption.ResponseHeadersRead);

            return await respone.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using Stream dataString = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;
            using StreamReader dataReader = new(dataString);

            while (!dataReader.EndOfStream)
            {
                string line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.Contains('"'))
                    line = line.Insert(line.IndexOf(',', line.IndexOf('"')) + 1, " -").Remove(line.IndexOf(',', line.IndexOf('"')), 1);
                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Province, string Country, (double lat, double lon) Place, int[] Counts)> GetCountriesData()
        {
            IEnumerable<string[]> lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (string[] row in lines)
            {
                string province = row[0].Trim();
                string name = row[1].Trim(' ', '"');
                double latitude = row[2] == string.Empty ? 0 : double.Parse(row[2], CultureInfo.InvariantCulture);
                double longitude = row[3] == string.Empty ? 0 : double.Parse(row[3], CultureInfo.InvariantCulture);
                int[] counts = row.Skip(5).Select(int.Parse).ToArray();

                yield return (province, name, (latitude, longitude), counts);
            }
        }

        public IEnumerable<Country> GetData(Cv19StatisticsDataType statisticsDataType)
        {
            dataType = statisticsDataType;
            DateTime[] dates = GetDates();

            var data = GetCountriesData().GroupBy(d => d.Country);

            foreach (var county_info in data)
            {
                Country country = new()
                {
                    Name = county_info.Key,
                    ProvinceCounts = county_info.Select(c => new Place()
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.lat, c.Place.lon),
                        Counts = dates.Zip(c.Counts, (date, count) => new ConfirmedCount { Date = date, Count = count })
                    })
                };
                yield return country;
            }
        }
    }
}