using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19Statistics.Models
{
    internal class Country : Place
    {
        public IEnumerable<Place> ProvinceCounts { get; set; }

        private Point? _location;
        public override Point Location
        {
            get
            {
                if (_location != null)
                    return (Point)_location;

                if (ProvinceCounts is null) return default;

                var average_x = ProvinceCounts.Average(x => x.Location.X);
                var average_y = ProvinceCounts.Average(y => y.Location.Y);

                return (Point)(_location = new(average_x, average_y));
            }
            set => _location = value;
        }

        private IEnumerable<ConfirmedCount> _counts;
        public override IEnumerable<ConfirmedCount> Counts
        {
            get
            {
                if (_counts != null) return _counts;

                var pointCount = ProvinceCounts.FirstOrDefault()?.Counts?.Count() ?? 0;
                if (pointCount == 0) return Enumerable.Empty<ConfirmedCount>();

                var provincePoints = ProvinceCounts.Select(p => p.Counts.ToArray()).ToArray();

                var points = new ConfirmedCount[pointCount];
                foreach (var province in provincePoints)
                    for (int i = 0; i < pointCount; i++)
                    {
                        if (points[i] is null)
                            points[i] = province[i];
                        else
                            points[i].Count += province[i].Count;
                    }

                return _counts = points;
            }
            set => _counts = value;
        }
    }
}