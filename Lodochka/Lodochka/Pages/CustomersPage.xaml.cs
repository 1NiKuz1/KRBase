using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lodochka.Pages
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        public CustomersPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Customers SelectedItem;
        public ObservableCollection<Base.Customers> Customerss;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int i = 0; i < 10; i++)
            {
                Columns.Add(CustomersGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in CustomersGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
        public void UpdateGrid(Base.Customers Customers)
        {
            if ((Customers == null) && (CustomersGrid.ItemsSource != null))
            {
                Customers = (Base.Customers)CustomersGrid.SelectedItem;
            }
            Customerss = new ObservableCollection<Base.Customers>(SourceCore.MyBase.Customers);
            CustomersGrid.ItemsSource = Customerss;
            CustomersGrid.SelectedItem = Customers;
        }



        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                CustomersGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                CustomersAddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                CustomersGrid.IsHitTestVisible = true;
                RecordAdd.IsEnabled = true;
                RecordCopy.IsEnabled = true;
                RecordEdit.IsEnabled = true;
                RecordDellete.IsEnabled = true;
                DlgMode = -1;
            }
        }
        private void RecordAdd_Click(object sender, RoutedEventArgs e)
        {
            DlgLoad(true, "Добавить");
            DataContext = null;
            DlgMode = 0;
        }

        private void RecordkCopy_Click(object sender, RoutedEventArgs e)
        {
            DlgLoad(true, "Добавить");
            DataContext = null;
            DlgMode = 0;
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.Customers)CustomersGrid.SelectedItem;
                RecordTextFistName.Text = SelectedItem.FistName;
                RecordTextFamilyName.Text = SelectedItem.FamilyName;
                RecordTextDateOfBirth.Text = SelectedItem.DateOfBirth.ToString();
                RecordTextOrganisationName.Text = SelectedItem.OrganisationName;
                RecordTextAddress.Text = SelectedItem.Address;
                RecordTextCity.Text = SelectedItem.City;
                RecordTextemail.Text = SelectedItem.email;
                RecordTextPhone.Text = SelectedItem.Phone;
                RecordTextIDNumber.Text = SelectedItem.IDNumber;
                RecordTextIDDocumentName.Text = SelectedItem.IDDocumentName;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {

                try
                {
                    // Ссылка на удаляемую книгу
                    Base.Customers Deleting = (Base.Customers)CustomersGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (CustomersGrid.SelectedIndex < CustomersGrid.Items.Count - 1)
                    {
                        CustomersGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (CustomersGrid.SelectedIndex > 0)
                        {
                            CustomersGrid.SelectedIndex--;
                        }
                    }
                    Base.Customers SelectingCustomers = (Base.Customers)CustomersGrid.SelectedItem;
                    SourceCore.MyBase.Customers.Remove(Deleting);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingCustomers);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void CustomersAddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextFistName.Text))
                errors.AppendLine("Укажите название FistName");

            if (string.IsNullOrEmpty(RecordTextFamilyName.Text))
                errors.AppendLine("Укажите название FamilyName");

            if (string.IsNullOrEmpty(RecordTextDateOfBirth.Text))
                errors.AppendLine("Укажите название DateOfBirth");

            if (string.IsNullOrEmpty(RecordTextOrganisationName.Text))
                errors.AppendLine("Укажите название OrganisationName");

            if (string.IsNullOrEmpty(RecordTextAddress.Text))
                errors.AppendLine("Укажите название Address");

            if (string.IsNullOrEmpty(RecordTextCity.Text))
                errors.AppendLine("Укажите название City");

            if (string.IsNullOrEmpty(RecordTextemail.Text))
                errors.AppendLine("Укажите название email");

            if (string.IsNullOrEmpty(RecordTextPhone.Text))
                errors.AppendLine("Укажите название Phone");

            if (string.IsNullOrEmpty(RecordTextIDNumber.Text))
                errors.AppendLine("Укажите название IDNumber");

            if (string.IsNullOrEmpty(RecordTextIDDocumentName.Text))
                errors.AppendLine("Укажите название IDDocumentName");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //RecordTextFistName.Text = SelectedItem.FistName;
            //RecordTextFamilyName.Text = SelectedItem.FamilyName;
            //RecordTextDateOfBirth.Text = SelectedItem.DateOfBirth.ToString();
            //RecordTextOrganisationName.Text = SelectedItem.OrganisationName;
            //RecordTextAddress.Text = SelectedItem.Address;
            //RecordTextCity.Text = SelectedItem.City;
            //RecordTextemail.Text = SelectedItem.email;
            //RecordTextPhone.Text = SelectedItem.Phone;
            //RecordTextIDNumber.Text = SelectedItem.IDNumber;
            //RecordTextIDDocumentName.Text = SelectedItem.IDDocumentName;

            if (DlgMode == 0)
            {
                var NewCustomers = new Base.Customers();
                NewCustomers.FistName = RecordTextFistName.Text.Trim();
                NewCustomers.FamilyName = RecordTextFamilyName.Text.Trim();
                NewCustomers.DateOfBirth = Convert.ToDateTime(RecordTextDateOfBirth.Text);
                NewCustomers.OrganisationName = RecordTextOrganisationName.Text.Trim();
                NewCustomers.Address = RecordTextAddress.Text.Trim();
                NewCustomers.City = RecordTextCity.Text.Trim();
                NewCustomers.email = RecordTextemail.Text.Trim();
                NewCustomers.Phone = RecordTextPhone.Text.Trim();
                NewCustomers.IDNumber = RecordTextIDNumber.Text.Trim();
                NewCustomers.IDDocumentName = RecordTextIDDocumentName.Text.Trim();
                SourceCore.MyBase.Customers.Add(NewCustomers);
                SelectedItem = NewCustomers;
            }
            else
            {
                var EditCustomers = new Base.Customers();
                EditCustomers = SourceCore.MyBase.Customers.First(p => p.Customer_ID == SelectedItem.Customer_ID);
                EditCustomers.FistName = RecordTextFistName.Text.Trim();
                EditCustomers.FamilyName = RecordTextFamilyName.Text.Trim();
                EditCustomers.DateOfBirth = Convert.ToDateTime(RecordTextDateOfBirth.Text);
                EditCustomers.OrganisationName = RecordTextOrganisationName.Text.Trim();
                EditCustomers.Address = RecordTextAddress.Text.Trim();
                EditCustomers.City = RecordTextCity.Text.Trim();
                EditCustomers.email = RecordTextemail.Text.Trim();
                EditCustomers.Phone = RecordTextPhone.Text.Trim();
                EditCustomers.IDNumber = RecordTextIDNumber.Text.Trim();
                EditCustomers.IDDocumentName = RecordTextIDDocumentName.Text.Trim();
            }

            try
            {
                SourceCore.MyBase.SaveChanges();
                UpdateGrid(SelectedItem);
                DlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            //DlgMode = -1;
        }

        private void CustomersAddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedItem);
            DlgLoad(false, "");
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.FistName.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.FamilyName.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.DateOfBirth.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.OrganisationName.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.Address.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 5:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.City.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 6:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.email.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 7:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.Phone.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 8:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.IDNumber.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 9:
                    CustomersGrid.ItemsSource = SourceCore.MyBase.Customers.Where(q => q.IDDocumentName.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }
}
