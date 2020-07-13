using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeasurementManagerMVVM.Models
{
    /// <summary>
    /// Класс, характеризующий лимиты замеров для определенного региона в определённую дату
    /// </summary>
    public class TownDateLimits: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

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

        public int LimitsCount { get { return _limits.Count; } }

        public static IEnumerable<TownDateLimits> GetTestLimits() //Статический метод получения примеров лимитов замеров по городам
        {
            return new List<TownDateLimits>
            {
                new TownDateLimits("Саратов", DateTime.Now, new List<IntervalLimit>
                {
                    new IntervalLimit(10, 12, 3),
                    new IntervalLimit(12, 14, 4),
                    new IntervalLimit(14, 15, 5),
                    new IntervalLimit(15, 16, 6)
                }),

                new TownDateLimits("Москва", DateTime.Now, new List<IntervalLimit>
                {
                    new IntervalLimit(8, 10, 1),
                    new IntervalLimit(10, 12, 5),
                    new IntervalLimit(12, 14, 6),
                    new IntervalLimit(14, 15, 4),
                    new IntervalLimit(15, 16, 7)
                })
            };

            
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
