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
    // Логика взаимодействия для ServiceEditDialog.xaml
    public partial class ServiceEditDialog : Window
    {
        private DentalService editingService;
        private bool isEditMode = false;
        public ServiceEditDialog()
        {
            InitializeComponent();
        }
        public ServiceEditDialog(DentalService service)
        {
            InitializeComponent();
            isEditMode = true;           // Включаем режим редактирования
            editingService = service;    // Запоминаем, какую услугу редактируем
            LoadServiceData();           // Загружаем данные в поля формы
        }

        private void LoadServiceData() // Загрузка данных услуги в поля формы
        {
            txtName.Text = editingService.Name;
            txtCost.Text = editingService.Cost.ToString();
            txtDuration.Text = editingService.Duration.ToString();
            txtCabinet.Text = editingService.Cabinet;
        }

        public DentalService GetService()// Получение новой услуги из данных формы (для режима добавления)
        {
            return new DentalService
            {
                Name = txtName.Text.Trim(),
                Cost = decimal.TryParse(txtCost.Text, out decimal cost) ? cost : 0,
                Duration = int.TryParse(txtDuration.Text, out int duration) ? duration : 0,
                Cabinet = txtCabinet.Text.Trim()
            };
        }
        public void UpdateService(DentalService service) // Обновление существующей услуги данными из формы (для режима редактирования)
        {
            service.Name = txtName.Text.Trim();
            service.Cost = decimal.TryParse(txtCost.Text, out decimal cost) ? cost : 0;
            service.Duration = int.TryParse(txtDuration.Text, out int duration) ? duration : 0;
            service.Cabinet = txtCabinet.Text.Trim();
        }
        private void ValidateForm(object sender, RoutedEventArgs e) // Валидация всех полей формы
        {
            string error = "";
            bool isValid = true;

            // Проверка названия услуги
            if (!ValidationHelper.ValidateServiceName(txtName.Text.Trim(), out string nameError))
            {
                error += nameError + "\n";
                isValid = false;
            }

            // Проверка стоимости
            if (decimal.TryParse(txtCost.Text, out decimal cost))
            {
                if (!ValidationHelper.ValidateCost(cost, out string costError))
                {
                    error += costError + "\n";
                    isValid = false;
                }
            }
            else
            {
                error += "Введите корректную стоимость\n";
                isValid = false;
            }

            // Проверка продолжительности
            if (int.TryParse(txtDuration.Text, out int duration))
            {
                if (!ValidationHelper.ValidateDuration(duration, out string durationError))
                {
                    error += durationError + "\n";
                    isValid = false;
                }
            }
            else
            {
                error += "Введите корректную продолжительность\n";
                isValid = false;
            }

            // Проверка кабинета
            if (!ValidationHelper.ValidateCabinet(txtCabinet.Text.Trim(), out string cabinetError))
            {
                error += cabinetError + "\n";
                isValid = false;
            }

            txtError.Text = error;
            btnSave.IsEnabled = isValid;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) // Обработчик кнопки "Сохранить"
        {
            // Если это режим редактирования, обновляем данные
            if (isEditMode && editingService != null)
            {
                UpdateService(editingService);
            }

            DialogResult = true;
            Close();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e) // Обработчик кнопки "Отмена"
        {
            DialogResult = false;
            Close();
        }

    }
    
}
