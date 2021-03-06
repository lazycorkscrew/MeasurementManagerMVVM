﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementManagerMVVM.Models
{
    /// <summary>
    /// Класс, характеризующий почасовой временной интервал от 0 до 24 часов (24 час считается, как 23:59, т.е. без переноса на следующий день)
    /// </summary>
    public class HourInterval
    {
        private int _hourBegin;
        private int _hourEnd;

        public int HourBegin
        {
            get { return _hourBegin; }
            set { _hourBegin = ValidatedHour(value, false, "HourBegin"); }
        }
        public int HourEnd
        {
            get
            { return _hourEnd; }
            set { _hourEnd = ValidatedHour(value, true, "HourEnd"); }
        }

        public HourInterval()
        {
            HourBegin = 0;
            HourEnd = 24;
        }

        public HourInterval(int hourBegin, int hourEnd)
        {
            if (hourBegin >= hourEnd)
            {
                throw new ArgumentException("hourBegin не может быть больше, чем hourEnd.");
            }

            HourEnd = hourEnd;
            HourBegin = hourBegin;
        }

        private int ValidatedHour(int inValue, bool notZero, string fieldName)
        {
            if (notZero && inValue == 0)
            {
                throw new ArgumentException($"Поле {fieldName} не может быть равным 0.");
            }

            if (inValue < 0 || inValue > 24)
            {
                throw new ArgumentException($"Поле {fieldName} должно быть в диапазоне от 0 до 24.");
            }

            return inValue;
        }
    }
}
