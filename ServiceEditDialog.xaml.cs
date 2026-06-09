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
    /// Логика взаимодействия для ServiceEditDialog.xaml
    /// </summary>
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

        /// <summary>
        /// Загрузка данных услуги в поля формы
        /// </summary>
        private void LoadServiceData()
        {
            txtName.Text = editingService.Name;
            txtCost.Text = editingService.Cost.ToString();
            txtDuration.Text = editingService.Duration.ToString();
            txtCabinet.Text = editingService.Cabinet;
        }

        /// <summary>
        /// Получение новой услуги из данных формы (для режима добавления)
        /// </summary>
        public DentalService GetService()
        {
            return new DentalService
            {
                Name = txtName.Text.Trim(),
                Cost = decimal.TryParse(txtCost.Text, out decimal cost) ? cost : 0,
                Duration = int.TryParse(txtDuration.Text, out int duration) ? duration : 0,
                Cabinet = txtCabinet.Text.Trim()
            };
        }

        /// <summary>
        /// Обновление существующей услуги данными из формы (для режима редактирования)
        /// </summary>
        public void UpdateService(DentalService service)
        {
            service.Name = txtName.Text.Trim();
            service.Cost = decimal.TryParse(txtCost.Text, out decimal cost) ? cost : 0;
            service.Duration = int.TryParse(txtDuration.Text, out int duration) ? duration : 0;
            service.Cabinet = txtCabinet.Text.Trim();
        }

        /// <summary>
        /// Валидация всех полей формы
        /// </summary>
        private void ValidateForm(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Обработчик кнопки "Сохранить"
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Если это режим редактирования, обновляем данные
            if (isEditMode && editingService != null)
            {
                UpdateService(editingService);
            }

            DialogResult = true;
            Close();
        }

        /// <summary>
        /// Обработчик кнопки "Отмена"
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
    
}
