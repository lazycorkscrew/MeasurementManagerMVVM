using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementManagerMVVM.Models
{
    public class Address
    {
        /// <summary>
        /// Адрес вносится текстовой строкой в формате "Город;ТипУлицы;Улица;Дом;Квартира"
        /// </summary>
        /// <param name="address"></param>
        public Address(string address)
        {
            string[] parts = address.Split(';');
            if (parts.Length == 5)
            {
                Town = parts[0];
                StreetType = parts[1];
                Street = parts[2];
                House = parts[3];
                Kv = parts[4];
            }
            else throw new InvalidDataException("Адрес должнен вноситься текстовой строкой в формате \"Город;ТипУлицы;Улица;Дом;Квартира\"");
        }
        public string Town { get; set; }
        public string Street { get; set; }
        public string StreetType { get; set; }
        public string House { get; set; }
        public string Kv { get; set; }

        public override string ToString()
        {
            return $"{Town}, {StreetType} {Street}, д {House}, кв {Kv}";
        }
    }
}
