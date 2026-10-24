using PasswordHashing;
using PZ3_Bortsova.Models;
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
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btnRegistr_Click(object sender, RoutedEventArgs e)
        {
            string login = txtbLogin.Text.Trim();
            string password = pswbPassword.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Внимание");
                return;
            }

            try
            {
                var context = Helper.GetContext();

                if (context.Users.Any(u => u.Login == login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка");
                    return;
                }

                string hashedPassword = PasswordHasher.HashPassword(password);
                
                Users newUser = new Users
                {
                    Login = login,
                    Password = hashedPassword,
                };
                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно.Теперь вы можете войти");
                NavigationService.Navigate(new Autho());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации: " + ex.Message, "Ошибка");
            }
        }
    }
}
