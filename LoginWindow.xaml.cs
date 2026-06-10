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
    public partial class LoginWindow : Window
    {
        public LoginWindow() // конструктор
        {
            InitializeComponent();
        }
        // Обработчик кнопки "Вход"
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Получаем введенные данные
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            // Проверка данных
            if (login == "zub" && password == "hehe")
            {
                // Успешный вход
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Ошибка авторизации
                txtError.Text = "Ошибка авторизации. Проверьте логин и пароль";
                txtLogin.Clear();
                txtPassword.Clear();
                txtLogin.Focus();
            }
        }
        // Обработчик кнопки Выход
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
