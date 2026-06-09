using Proekt3.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proekt3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Коллекции для хранения данных
        private ObservableCollection<Patient> patients;      // Список пациентов
        private ObservableCollection<DentalService> services; // Список услуг

        // Генераторы ID
        private int nextPatientId = 1;
        private int nextServiceId = 1;
        public MainWindow()
        {
            InitializeComponent();
            InitializeData(); // Заполнение тестовыми данными
        }
        private void InitializeData()
        {
            // Тестовые данные пациентов
            patients = new ObservableCollection<Patient>
            {
                new Patient
                {
                    Id = nextPatientId++,
                    FullName = "Иванов Иван Иванович",
                    BirthDate = new DateTime(1985, 5, 15),
                    Phone = "89123456789",
                    Snils = "123-456-789 01",
                    LastVisit = DateTime.Today.AddDays(-10),
                    Status = "В лечении"
                },
                new Patient
                {
                    Id = nextPatientId++,
                    FullName = "Петрова Анна Сергеевна",
                    BirthDate = new DateTime(1990, 8, 20),
                    Phone = "79201234567",
                    Snils = "987-654-321 99",
                    LastVisit = DateTime.Today.AddDays(-30),
                    Status = "Здоров"
                }
            };

            // Тестовые данные услуг
            services = new ObservableCollection<DentalService>
            {
                new DentalService { Id = nextServiceId++, Name = "Осмотр стоматолога", Cost = 500, Duration = 30, Cabinet = "101" },
                new DentalService { Id = nextServiceId++, Name = "Лечение кариеса", Cost = 2500, Duration = 60, Cabinet = "102" },
                new DentalService { Id = nextServiceId++, Name = "Удаление зуба", Cost = 1500, Duration = 45, Cabinet = "103" },
                new DentalService { Id = nextServiceId++, Name = "Профессиональная чистка", Cost = 3000, Duration = 90, Cabinet = "104" }
            };

            // Привязка данных к таблицам
            dgPatients.ItemsSource = patients;
            dgServices.ItemsSource = services;
        }

        // ==================== ОПЕРАЦИИ С ПАЦИЕНТАМИ ====================

        /// <summary>
        /// Добавление нового пациента
        /// </summary>
        private void BtnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new PatientEditDialog();
                if (dialog.ShowDialog() == true)
                {
                    var patient = dialog.GetPatient();
                    patient.Id = nextPatientId++;
                    patient.LastVisit = DateTime.Today;
                    patient.Status = "В лечении";
                    patients.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении пациента: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Редактирование выбранного пациента
        /// </summary>
        private void BtnEditPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = dgPatients.SelectedItem as Patient;
                if (selected == null)
                {
                    MessageBox.Show("Выберите пациента для редактирования", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var dialog = new PatientEditDialog(selected);
                if (dialog.ShowDialog() == true)
                {
                    dialog.UpdatePatient(selected);
                    dgPatients.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при редактировании: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Удаление выбранного пациента
        /// </summary>
        private void BtnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = dgPatients.SelectedItem as Patient;
                if (selected == null)
                {
                    MessageBox.Show("Выберите пациента для удаления", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show($"Удалить запись о пациенте \"{selected.FullName}\"?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    patients.Remove(selected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обновление таблицы пациентов
        /// </summary>
        private void BtnRefreshPatients_Click(object sender, RoutedEventArgs e)
        {
            dgPatients.Items.Refresh();
        }

        /// <summary>
        /// Обработчик двойного клика по строке пациента
        /// </summary>
        private void DgPatients_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BtnEditPatient_Click(sender, null);
        }

        // ==================== ОПЕРАЦИИ С УСЛУГАМИ ====================

        /// <summary>
        /// Добавление новой услуги
        /// </summary>
        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new ServiceEditDialog();
                if (dialog.ShowDialog() == true)
                {
                    var service = dialog.GetService();
                    service.Id = nextServiceId++;
                    services.Add(service);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении услуги: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Редактирование выбранной услуги
        /// </summary>
        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = dgServices.SelectedItem as DentalService;
                if (selected == null)
                {
                    MessageBox.Show("Выберите услугу для редактирования", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var dialog = new ServiceEditDialog(selected);
                if (dialog.ShowDialog() == true)
                {
                    dialog.UpdateService(selected);
                    dgServices.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при редактировании: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Удаление выбранной услуги
        /// </summary>
        private void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = dgServices.SelectedItem as DentalService;
                if (selected == null)
                {
                    MessageBox.Show("Выберите услугу для удаления", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show($"Удалить услугу \"{selected.Name}\"?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    services.Remove(selected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обновление таблицы услуг
        /// </summary>
        private void BtnRefreshServices_Click(object sender, RoutedEventArgs e)
        {
            dgServices.Items.Refresh();
        }

        /// <summary>
        /// Обработчик двойного клика по строке услуги
        /// </summary>
        private void DgServices_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BtnEditService_Click(sender, null);
        }
    }
}