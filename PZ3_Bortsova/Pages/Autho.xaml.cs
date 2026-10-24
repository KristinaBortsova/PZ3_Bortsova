using PasswordHashing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PZ3_Bortsova.Pages
{
    public partial class Autho : Page
    {
        public Autho()
        {
            InitializeComponent();
        }

        private void btnRegistr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtbLogin.Text.Trim();
            string password = pswbPassword.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Внимание");
                return;
            }
            try
            {
                string hashedPassword = PasswordHasher.HashPassword(password);
                var context = Helper.GetContext();
                var user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == hashedPassword);
                if (user != null)
                {
                    MessageBox.Show("Вы успешно вошли. Добро пожаловать", "Успех");
                    NavigationService.Navigate(new Client());
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД: " + ex.Message, "Ошибка");
            }
        }
    }
}
