using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementManagerMVVM.Models
{
    /// <summary>
    /// Класс, характеризующий лимиты замеров для определенного региона в определённую дату
    /// </summary>
    class TownDateLimits
    {
        public string Town { get; set; }
        public DateTime Date { get; set; }
        public List<IntervalLimit> Limits 
        { 
            get
            {
                return Limits;
            }
            set
            {
                foreach(IntervalLimit limit in value)
                {
                    AddLimit(limit);
                }
            }
        }

        public TownDateLimits(string town, DateTime date, IEnumerable<IntervalLimit> limits)
        {
            Town = town;
            Date = date;
            Limits = limits as List<IntervalLimit>;
        }

        public void AddLimit(IntervalLimit limit)
        {
            if(Limits.Any())
            {
                
                if(Limits.Last().Interval.HourEnd > limit.Interval.HourBegin)
                {
                    throw new ArgumentException("Временные интервалы не должны пересекаться.");
                }
            }

            Limits.Add(limit);
        }
    }
}
