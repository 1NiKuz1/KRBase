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

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private Base.LibraryExEntities Database;
        public RegistrationWindow(Base.LibraryExEntities Database)
        {
            InitializeComponent();
            this.Database = Database;
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
            Base.Users User = new Base.Users();
            User.Login = LoginTextBox.Text;
            User.Password = PasswordPasswordBox.Password != "" ? PasswordPasswordBox.Password : PasswordTextBox.Text;
            User.IdRole = 0;
            User.FriendlyName = "";
            // Добавление его в базу данных
            Database.Users.Add(User);
            // Сохранение изменений
            Database.SaveChanges();
            Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
