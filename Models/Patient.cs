using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proekt3.Models
{
    public class Patient // класс пациенты
    {
        public int Id { get; set; }                      // айди пациента
        public string FullName { get; set; }             // ФИО пациента
        public DateTime BirthDate { get; set; }          // Дата рождения
        public string Phone { get; set; }                // Номер телефона
        public string Snils { get; set; }                // СНИЛС
        public DateTime LastVisit { get; set; }          // Дата последнего визита
        public string Status { get; set; }               // Статус здоровья
    }
}
