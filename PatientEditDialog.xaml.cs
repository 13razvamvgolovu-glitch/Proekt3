using Proekt3.Helpers;
using Proekt3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proekt3
{
    // Логика взаимодействия для PatientEditDialog.xaml
    public partial class PatientEditDialog : Window
    {
        private Patient editingPatient;  // Редактируемый пациент (для режима редактирования)
        private bool isEditMode = false; // true - редактирование, false - добавление
        public PatientEditDialog() 
        {
            InitializeComponent(); // конструктор
            dpBirthDate.SelectedDate = DateTime.Today.AddYears(-30); // изначальная дата рождения
        }
        public PatientEditDialog(Patient patient)
        {
            InitializeComponent();
            isEditMode = true;
            editingPatient = patient;
            LoadPatientData(); // Загружаем данные в поля
        }
        private void LoadPatientData() // Загрузка данных пациента в поля формы
        {
            txtFullName.Text = editingPatient.FullName;
            dpBirthDate.SelectedDate = editingPatient.BirthDate;
            txtPhone.Text = editingPatient.Phone;
            txtSnils.Text = editingPatient.Snils;
        }
        public Patient GetPatient() // Получение нового пациента из данных формы
        {
            return new Patient
            {
                FullName = txtFullName.Text.Trim(),
                BirthDate = dpBirthDate.SelectedDate ?? DateTime.Today,
                Phone = txtPhone.Text.Trim(),
                Snils = txtSnils.Text.Trim()
            };
        }
        public void UpdatePatient(Patient patient) // Обновление существующего пациента данными из формы
        {
            patient.FullName = txtFullName.Text.Trim();
            patient.BirthDate = dpBirthDate.SelectedDate ?? DateTime.Today;
            patient.Phone = txtPhone.Text.Trim();
            patient.Snils = txtSnils.Text.Trim();
        }
        private void ValidateForm(object sender, RoutedEventArgs e) // Валидация всех полей формы
        {
            string error = "";
            bool isValid = true;

            // Проверка ФИО
            if (!ValidationHelper.ValidateFullName(txtFullName.Text.Trim(), out string nameError))
            {
                error += nameError + "\n";
                isValid = false;
            }

            // Проверка даты рождения
            if (dpBirthDate.SelectedDate.HasValue)
            {
                if (!ValidationHelper.ValidateBirthDate(dpBirthDate.SelectedDate.Value, out string dateError))
                {
                    error += dateError + "\n";
                    isValid = false;
                }
            }
            else
            {
                error += "Дата рождения обязательна\n";
                isValid = false;
            }

            if (!ValidationHelper.ValidatePhone(txtPhone.Text.Trim(), out string phoneError))
            {
                error += phoneError + "\n";
                isValid = false;
            } 
            if (!ValidationHelper.ValidateSnils(txtSnils.Text.Trim(), out string snilsError))
            {
                error += snilsError + "\n";
                isValid = false;
            }

            txtError.Text = error;
            btnSave.IsEnabled = isValid;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) // обработка кнопки сохранить
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) // обработка кнопки отмена
        {
            DialogResult = false;
            Close();
        }
    }
    
}
