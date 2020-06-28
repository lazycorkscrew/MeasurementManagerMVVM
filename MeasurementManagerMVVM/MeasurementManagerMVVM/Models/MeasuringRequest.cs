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
    class MeasuringRequest : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public string Number { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Patronymic { get; set; }
        public Address ClientAddress { get; set; }

        public string AddressString
        {
            get
            {
                return ClientAddress.ToString();
            }
        }
        public string Phone { get; set; }
        public DateTime Requested { get; set; } //Дата создания заявки

        public Appointment MeasuringAppointment { get; set; }
        public DateTime Completed { get; set; } //Дата назначения даты и временного интервала замера

        public static MeasuringRequest[] GetMeasurings()
        {
            try
            {
                MeasuringRequest[] result = new MeasuringRequest[]
                {
                    new MeasuringRequest { Number = "000001", Lname = "Брошкина", Fname = "Наталья", Patronymic = "Викторовна", ClientAddress = new Address("Москва;ул;Пушкина;4;7"), Phone = "88005553535", Requested = DateTime.Parse("17.06.2020"), Completed = DateTime.Now },
                    new MeasuringRequest { Number = "000002", Lname = "Колотилов", Fname = "Пётр", Patronymic = "Иванович", ClientAddress = new Address("Саратов;ул;Лебедева-Кумача;70;23"), Phone = "88006008000", Requested = DateTime.Parse("17.06.2020"), Completed = DateTime.Now }
                };

                return result;
            }
            //try-catch Нужен для отладки, смотреть, какое исключение мешает данным выгрузиться. Позже удалить
            catch(Exception ex)
            {
                return null;
            }
            
        }

    }
}
