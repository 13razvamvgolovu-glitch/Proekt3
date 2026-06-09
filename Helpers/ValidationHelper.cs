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
        public static bool ValidateFullName(string name, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                error = "ФИО обязательно для заполнения";
                return false;
            }
            if (name.Length < 5 || name.Length > 100)
            {
                error = "ФИО должно быть от 5 до 100 символов";
                return false;
            }
            if (!Regex.IsMatch(name, @"^[а-яА-Яa-zA-Z\s\-]+$"))
            {
                error = "ФИО может содержать только буквы, пробелы и дефис";
                return false;
            }
            return true;
        }

        public static bool ValidateBirthDate(DateTime birthDate, out string error)
        {
            error = string.Empty;
            if (birthDate > DateTime.Today)
            {
                error = "Дата рождения не может быть позже текущей даты";
                return false;
            }
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age)) age--;
            if (age < 0 || age > 120)
            {
                error = "Возраст должен быть от 0 до 120 лет";
                return false;
            }
            return true;
        }

        public static bool ValidatePhone(string phone, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(phone))
            {
                error = "Телефон обязателен для заполнения";
                return false;
            }
            string cleaned = Regex.Replace(phone, @"\D", "");
            if (cleaned.Length == 11 && (cleaned.StartsWith("7") || cleaned.StartsWith("8")))
            {
                return true;
            }
            error = "Телефон должен быть в формате +7XXXXXXXXXX или 8XXXXXXXXXX (11 цифр)";
            return false;
        }

        public static bool ValidateSnils(string snils, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(snils))
            {
                error = "СНИЛС обязателен для заполнения";
                return false;
            }
            if (!Regex.IsMatch(snils, @"^\d{3}-\d{3}-\d{3} \d{2}$"))
            {
                error = "СНИЛС должен быть в формате XXX-XXX-XXX XX";
                return false;
            }
            return true;
        }

        public static bool ValidateServiceName(string name, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                error = "Название услуги обязательно";
                return false;
            }
            if (name.Length < 2 || name.Length > 100)
            {
                error = "Название должно быть от 2 до 100 символов";
                return false;
            }
            return true;
        }

        public static bool ValidateCost(decimal cost, out string error)
        {
            error = string.Empty;
            if (cost <= 0)
            {
                error = "Стоимость должна быть больше 0";
                return false;
            }
            if (cost > 1000000)
            {
                error = "Стоимость не может превышать 1 000 000";
                return false;
            }
            return true;
        }

        public static bool ValidateDuration(int duration, out string error)
        {
            error = string.Empty;
            if (duration <= 0)
            {
                error = "Продолжительность должна быть больше 0";
                return false;
            }
            if (duration > 480)
            {
                error = "Продолжительность не может превышать 480 минут (8 часов)";
                return false;
            }
            return true;
        }

        public static bool ValidateCabinet(string cabinet, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(cabinet))
            {
                error = "Номер кабинета обязателен";
                return false;
            }
            if (!Regex.IsMatch(cabinet, @"^\d+$"))
            {
                error = "Номер кабинета должен содержать только цифры";
                return false;
            }
            return true;
        }
    }
}
