using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KR2
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private Base.KR2Entities DataBase;
        public RegistrationWindow(Base.KR2Entities Database)
        {
            InitializeComponent();
            this.DataBase = Database;
        }

        private String CheckPassword(String pas)
        {
            //string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,24}$";
            string pattern = @"^*$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(pas))
            {
                return pas;
            }
            return "";
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            // Переброска необходимой информации во временные буферы
            String Password = PasswordPasswordBox.Password;
            Visibility Visibility = PasswordPasswordBox.Visibility;
            double Width = PasswordPasswordBox.ActualWidth;
            // Изменение подписи на кнопке
            PasswordButton.Content = Visibility == Visibility.Visible ? "Скрыть" : "Показать";
            // Переброска информации из TextBox'а в PasswordBox
            PasswordPasswordBox.Password = PasswordTextBox.Text;
            PasswordPasswordBox.Visibility = PasswordTextBox.Visibility;
            PasswordPasswordBox.Width = PasswordTextBox.Width;
            // Возврат информации из временных буферов в TextBox
            PasswordTextBox.Text = Password;
            PasswordTextBox.Visibility = Visibility;
            PasswordTextBox.Width = Width;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание и инициализация нового пользователя системы
            String password = PasswordPasswordBox.Password != "" ? CheckPassword(PasswordPasswordBox.Password) : CheckPassword(PasswordTextBox.Text);
            if (password != "")
            {
                Base.Users User = new Base.Users();
                User.Login = LoginTextBox.Text;
                User.Password = PasswordPasswordBox.Password != "" ? PasswordPasswordBox.Password : PasswordTextBox.Text;
                // Добавление его в базу данных
                DataBase.Users.Add(User);
                // Сохранение изменений
                DataBase.SaveChanges();
            }
            else
            {
                MessageBox.Show("Неверный формат пароля, попробуйте еще раз", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
