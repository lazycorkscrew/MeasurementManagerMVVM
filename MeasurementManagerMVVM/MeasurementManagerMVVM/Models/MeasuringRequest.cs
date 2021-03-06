﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MeasurementManagerMVVM.Models
{
    public class MeasuringRequest : INotifyPropertyChanged
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

        private IntervalLimit _appoinedInterval;
        

        private DateTime? _appointment;
        public DateTime? Appointed 
        {
            get
            {
                return _appointment;
            }
            set
            {
                _appointment = value;
                OnPropertyChanged("Appointed");
            }
        } //Дата назначения даты и временного интервала замера

        public void Appoint(DateTime? appointed, IntervalLimit intervalLimit) //Назначение даты и времени заказа
        {
            Appointed = appointed;
            _appoinedInterval = intervalLimit;
            intervalLimit.MeasurementsLimit--;
        }

        public void CancelAppoint() //Отмена назначения
        {
            Appointed = null;
            _appoinedInterval.MeasurementsLimit++;
            _appoinedInterval = null;
        }

        public static MeasuringRequest[] GetMeasurings() //Статический метод получения примеров данных
        {
            try
            {
                MeasuringRequest[] result = new MeasuringRequest[]
                {
                    new MeasuringRequest { Number = "000001", Lname = "Брошкина", Fname = "Наталья", Patronymic = "Викторовна", ClientAddress = new Address("Москва;ул;Пушкина;4;7"), Phone = "88005553535", Requested = DateTime.Parse("17.06.2020") },
                    new MeasuringRequest { Number = "000002", Lname = "Колотилов", Fname = "Пётр", Patronymic = "Иванович", ClientAddress = new Address("Саратов;ул;Лебедева-Кумача;70;23"), Phone = "88006008000", Requested = DateTime.Parse("17.06.2020") }
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
