using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementManagerMVVM.Models
{
    /// <summary>
    /// Класс, характеризующий лимит количества замеров для определенного интервала.
    /// </summary>
    public class IntervalLimit
    {
        private int _measurementsLimit;

        public HourInterval Interval { get; set; }

        public string HourBeginEnd { get { return $"{Interval.HourBegin}-{Interval.HourEnd}"; }}

        public int MeasurementsLimit 
        { 
            get { return _measurementsLimit; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Limit не может быть меньше нуля.");
                }

                _measurementsLimit = value;
            }
        }

        public IntervalLimit(int limit)
        {
            Interval = new HourInterval();
            MeasurementsLimit = limit;
        }

        public IntervalLimit(int hourBegin, int hourEnd, int limit)
        {
            Interval = new HourInterval(hourBegin, hourEnd);
            MeasurementsLimit = limit;
        }

        public string ViewString
        {
            get
            {
                return $"{Interval.HourBegin}-{Interval.HourEnd}, замеров доступно: {MeasurementsLimit}";
            }
        }
        
    }
}
