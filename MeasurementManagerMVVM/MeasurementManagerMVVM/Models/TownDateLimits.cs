using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeasurementManagerMVVM.Models
{
    /// <summary>
    /// Класс, характеризующий лимиты замеров для определенного региона в определённую дату
    /// </summary>
    class TownDateLimits: DependencyObject
    {
        public string Town { get; set; }
        public DateTime Date { get; set; }

        private List<IntervalLimit> _limits;
        public List<IntervalLimit> Limits 
        { 
            get
            {
                return _limits;
            }
            set
            {
                foreach(IntervalLimit limit in value)
                {
                    AddLimit(limit);
                }
            }
        }

        public static IEnumerable<TownDateLimits> GetTestLimits()
        {
            List<TownDateLimits> tdLimits = new List<TownDateLimits>();
            List<IntervalLimit> intervalLimits = new List<IntervalLimit>();

            intervalLimits.AddRange(new IntervalLimit[]
                {
                    new IntervalLimit(10, 12, 3),
                    new IntervalLimit(12, 14, 4),
                    new IntervalLimit(14, 15, 5),
                    new IntervalLimit(15, 16, 6)
                });

            tdLimits.Add( new TownDateLimits("Саратов", DateTime.Now, intervalLimits));

            return tdLimits;
        }

        public TownDateLimits(string town, DateTime date, IEnumerable<IntervalLimit> limits)
        {
            Town = town;
            Date = date;
            Limits = limits as List<IntervalLimit>;
        }

        public void AddLimit(IntervalLimit newLimit)
        {
            if(_limits == null)
            {
                _limits = new List<IntervalLimit>();
            }

            if (_limits.Any())
            {
                foreach(IntervalLimit limit in _limits)
                {
                    if( (newLimit.Interval.HourEnd > limit.Interval.HourBegin) && (newLimit.Interval.HourBegin < limit.Interval.HourEnd))
                    {
                        throw new ArgumentException("Временные интервалы не должны пересекаться.");
                    }
                }
            }

            _limits.Add(newLimit);
        }
    }
}
