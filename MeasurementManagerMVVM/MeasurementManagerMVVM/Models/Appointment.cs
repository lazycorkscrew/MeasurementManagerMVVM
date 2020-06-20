using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementManagerMVVM.Models
{
    public class Appointment
    {
        public DateTime Date { get; set; }
        public HourInterval interval { get; set; }
    }
}
