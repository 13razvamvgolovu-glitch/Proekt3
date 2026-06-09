using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt3.Models
{
    public class DentalService
    {
        public int Id { get; set; }          // айди услуги
        public string Name { get; set; }     // Название услуги
        public decimal Cost { get; set; }    // Стоимость в рублях
        public int Duration { get; set; }    // Продолжительность в минутах
        public string Cabinet { get; set; }  // Номер кабинета
    }
}
