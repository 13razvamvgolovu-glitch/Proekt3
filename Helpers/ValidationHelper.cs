using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proekt3.Helpers
{
    public static class ValidationHelper // класс проверки ввода данных
    {
        public static bool ValidateFullName(string name, out string error) // проверка ФИО
        {
            error = string.Empty; // изначально без ошибок 
            if (string.IsNullOrWhiteSpace(name)) // проверка на пустосту 
            {
                error = "ФИО обязательно для заполнения";
                return false;
            }
            if (name.Length < 5 || name.Length > 100) // проверка на кол-во символов
            {
                error = "ФИО должно быть от 5 до 100 символов";
                return false;
            }
            if (!Regex.IsMatch(name, @"^[а-яА-Яa-zA-Z\s\-]+$")) // проверка на символы по ТЗ
            {
                error = "ФИО может содержать только буквы, пробелы и дефис";
                return false;
            }
            return true;
        }

        public static bool ValidateBirthDate(DateTime birthDate, out string error) // проверка др
        {
            error = string.Empty; // изначально без ошибок
            if (birthDate > DateTime.Today) // проверка на др в будущем
            {
                error = "Дата рождения не может быть позже текущей даты";
                return false;
            }
            int age = DateTime.Today.Year - birthDate.Year; // вычисление возраста
            if (birthDate > DateTime.Today.AddYears(-age)) age--; // проверка возраста по годам
            if (age < 0 || age > 120)
            {
                error = "Возраст должен быть от 0 до 120 лет";
                return false;
            }
            return true;
        }

        public static bool ValidatePhone(string phone, out string error) // проверка номера телефона
        {
            error = string.Empty; // изначально ошибки нет
            if (string.IsNullOrWhiteSpace(phone)) // проверка на пустую строку
            {
                error = "Телефон обязателен для заполнения";
                return false;
            }
            string cleaned = Regex.Replace(phone, @"\D", ""); // удаление всего кроме цифр
            if (cleaned.Length == 11 && (cleaned.StartsWith("7") || cleaned.StartsWith("8"))) // проверка на корректность номера телефона
            {
                return true;
            }
            error = "Телефон должен быть в формате +7XXXXXXXXXX или 8XXXXXXXXXX (11 цифр)";
            return false;
        }

        public static bool ValidateSnils(string snils, out string error) // проверка снилса
        {
            error = string.Empty; // изначально ошибки нет
            if (string.IsNullOrWhiteSpace(snils)) // проверка на пустоту
            {
                error = "СНИЛС обязателен для заполнения";
                return false;
            }
            if (!Regex.IsMatch(snils, @"^\d{3}-\d{3}-\d{3} \d{2}$")) // проверка формата ввода снила
            {
                error = "СНИЛС должен быть в формате XXX-XXX-XXX XX";
                return false;
            }
            return true;
        }

        public static bool ValidateServiceName(string name, out string error) // проверка названия услуги
        {
            error = string.Empty; // изначально ошибок нет
            if (string.IsNullOrWhiteSpace(name)) // проверка на пустоту
            {
                error = "Название услуги обязательно";
                return false;
            }
            if (name.Length < 2 || name.Length > 100) // проверка на кол-во символов
            {
                error = "Название должно быть от 2 до 100 символов";
                return false;
            }
            return true;
        }

        public static bool ValidateCost(decimal cost, out string error) // проверка стоимости услуги
        {
            error = string.Empty; // изначально ошибки нет
            if (cost <= 0) // проверка мин цену
            {
                error = "Стоимость должна быть больше 0";
                return false;
            }
            if (cost > 1000000) // проверка н макс цену
            {
                error = "Стоимость не может превышать 1 000 000";
                return false;
            }
            return true;
        }

        public static bool ValidateDuration(int duration, out string error)  // проверка длительности услуги
        {
            error = string.Empty; // изначально ошибки нет
            if (duration <= 0) // проверка на минимальное кол-во минут
            {
                error = "Продолжительность должна быть больше 0";
                return false;
            }
            if (duration > 480) // проверка на макс кол-во минут
            {
                error = "Продолжительность не может превышать 480 минут (8 часов)";
                return false;
            }
            return true;
        }

        public static bool ValidateCabinet(string cabinet, out string error) // проверка кабинета услуги
        {
            error = string.Empty; // изначально ошибки нет
            if (string.IsNullOrWhiteSpace(cabinet)) // проверка на пустоту
            {
                error = "Номер кабинета обязателен";
                return false;
            }
            if (!Regex.IsMatch(cabinet, @"^\d+$")) // только цифры
            {
                error = "Номер кабинета должен содержать только цифры";
                return false;
            }
            return true;
        }
    }
}
