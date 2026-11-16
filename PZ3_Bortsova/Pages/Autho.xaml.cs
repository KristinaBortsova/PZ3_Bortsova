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
using PZ3_Bortsova.Services;

namespace PZ3_Bortsova.Pages
{
    public partial class Autho : Page
    {
        int click = 0;
        string currentCaptcha = "";

        public Autho()
        {
            InitializeComponent();
        }

        private void btnRegistr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
        private void GenerateCaptcha()
        {
            txtBlockCaptcha.Visibility = Visibility.Visible;
            txtboxCaptcha.Visibility = Visibility.Visible;
            txtboxCaptchaVvod.Visibility = Visibility.Visible;

            currentCaptcha = CaptchaGenerator.GenerateCaptchaText(6);

            txtBlockCaptcha.Text = "Слишком много невернх попыток. Введите капчу и попробуйте снова";
            txtboxCaptcha.IsEnabled = false;
            txtboxCaptcha.Text = currentCaptcha;
            txtboxCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
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
                if (click <= 2)
                {
                    if (user != null)
                    {
                        MessageBox.Show("Вы успешно вошли. Добро пожаловать", "Успех");
                        click = 0;
                        NavigationService.Navigate(new Client(user, "Клиент"));
                    }
                    else
                    {
                        MessageBox.Show($"Неверный логин или пароль. Попытка {click} из 2.", "Ошибка");

                        if (click == 2)
                        {
                            GenerateCaptcha();
                        }
                    }
                }
                else if (click > 2)
                {
                    if (user != null && txtboxCaptcha.Text.Trim() == currentCaptcha)
                    {
                        MessageBox.Show("Вы успешно вошли. Добро пожаловать", "Успех");
                        click = 0;
                        NavigationService.Navigate(new Client(user, "Клиент"));
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин, пароль или капча", "Ошибка");
                        GenerateCaptcha();
                        txtboxCaptchaVvod.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД: " + ex.Message, "Ошибка");
            }
        }
        private void ALoaded(object sender, RoutedEventArgs e)
        {
            click = 0;
            txtBlockCaptcha.Visibility = Visibility.Hidden;
            txtboxCaptcha.Visibility = Visibility.Hidden;
            txtboxCaptchaVvod.Visibility = Visibility.Hidden;
            txtBlockCaptcha.Text = "";
            txtboxCaptcha.Text = "";
            currentCaptcha = "";
            pswbPassword.Password = "";
        }
    }
}
