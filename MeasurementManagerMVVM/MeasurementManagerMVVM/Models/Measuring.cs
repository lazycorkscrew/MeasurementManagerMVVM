﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeasurementManagerMVVM.Models
{
    class Measuring : DependencyObject
    {
        public string Number { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Patronymic { get; set; }
        public Address address { get; set; }
        public string AddressString
        {
            get
            {
                return address.ToString();
            }
        }
        public string Phone { get; set; }
        public DateTime Requested { get; set; }
        public DateTime Completed { get; set; }

        public static Measuring[] GetMeasurings()
        {
            try
            {
                Measuring[] result = new Measuring[]
                {
                    new Measuring { Number = "000001", Lname = "Брошкина", Fname = "Наталья", Patronymic = "Викторовна", address = new Address("Москва;ул;Пушкина;4;7"), Phone = "88005553535", Requested = DateTime.Parse("17.06.2020"), Completed = DateTime.Now },
                    new Measuring { Number = "000002", Lname = "Колотилов", Fname = "Пётр", Patronymic = "Иванович", address = new Address("Саратов;ул;Лебедева-Кумача;70;23"), Phone = "88006008000", Requested = DateTime.Parse("17.06.2020"), Completed = DateTime.Now }
                };

                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

    }
}
