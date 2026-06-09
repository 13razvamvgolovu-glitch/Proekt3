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
    /// <summary>
    /// Логика взаимодействия для PatientEditDialog.xaml
    /// </summary>
    public partial class PatientEditDialog : Window
    {
        private Patient editingPatient;  // Редактируемый пациент (для режима редактирования)
        private bool isEditMode = false; // true - редактирование, false - добавление
        public PatientEditDialog()
        {
            InitializeComponent();
            dpBirthDate.SelectedDate = DateTime.Today.AddYears(-30);
        }
        public PatientEditDialog(Patient patient)
        {
            InitializeComponent();
            isEditMode = true;
            editingPatient = patient;
            LoadPatientData(); // Загружаем данные в поля
        }

        /// <summary>
        /// Загрузка данных пациента в поля формы
        /// </summary>
        private void LoadPatientData()
        {
            txtFullName.Text = editingPatient.FullName;
            dpBirthDate.SelectedDate = editingPatient.BirthDate;
            txtPhone.Text = editingPatient.Phone;
            txtSnils.Text = editingPatient.Snils;
        }

        /// <summary>
        /// Получение нового пациента из данных формы
        /// </summary>
        public Patient GetPatient()
        {
            return new Patient
            {
                FullName = txtFullName.Text.Trim(),
                BirthDate = dpBirthDate.SelectedDate ?? DateTime.Today,
                Phone = txtPhone.Text.Trim(),
                Snils = txtSnils.Text.Trim()
            };
        }

        /// <summary>
        /// Обновление существующего пациента данными из формы
        /// </summary>
        public void UpdatePatient(Patient patient)
        {
            patient.FullName = txtFullName.Text.Trim();
            patient.BirthDate = dpBirthDate.SelectedDate ?? DateTime.Today;
            patient.Phone = txtPhone.Text.Trim();
            patient.Snils = txtSnils.Text.Trim();
        }

        /// <summary>
        /// Валидация всех полей формы
        /// </summary>
        private void ValidateForm(object sender, RoutedEventArgs e)
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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
    
}
