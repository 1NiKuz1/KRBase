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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Order SelectedItem;
        public ObservableCollection<Base.Order> Orders;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int i = 0; i < 3; i++)
            {
                Columns.Add(OrderGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in OrderGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
        public void UpdateGrid(Base.Order Order)
        {
            if ((Order == null) && (OrderGrid.ItemsSource != null))
            {
                Order = (Base.Order)OrderGrid.SelectedItem;
            }
            Orders = new ObservableCollection<Base.Order>(SourceCore.MyBase.Order);
            OrderGrid.ItemsSource = Orders;
            OrderGrid.SelectedItem = Order;
        }



        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                OrderGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                OrderAddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                OrderGrid.IsHitTestVisible = true;
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
            if (OrderGrid.SelectedItem != null)
            {
                DlgLoad(true, "Копировать");
                SelectedItem = (Base.Order)OrderGrid.SelectedItem;
                RecordTextDate.Text = SelectedItem.Date.ToString();
                RecordTextDeliveryAddress.Text = SelectedItem.DeliveryAddress.ToString();
                RecordTextCity.Text = SelectedItem.City.ToString();
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (OrderGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.Order)OrderGrid.SelectedItem;
                RecordTextDate.Text = SelectedItem.Date.ToString();
                RecordTextDeliveryAddress.Text = SelectedItem.DeliveryAddress.ToString();
                RecordTextCity.Text = SelectedItem.City.ToString();
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
                    Base.Order Deleting = (Base.Order)OrderGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (OrderGrid.SelectedIndex < OrderGrid.Items.Count - 1)
                    {
                        OrderGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (OrderGrid.SelectedIndex > 0)
                        {
                            OrderGrid.SelectedIndex--;
                        }
                    }
                    Base.Order SelectingOrder = (Base.Order)OrderGrid.SelectedItem;
                    SourceCore.MyBase.Order.Remove(Deleting);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingOrder);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void OrderAddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextDate.Text))
                errors.AppendLine("Укажите название Date");

            if (string.IsNullOrEmpty(RecordTextDeliveryAddress.Text))
                errors.AppendLine("Укажите название DeliveryAddress");

            if (string.IsNullOrEmpty(RecordTextCity.Text))
                errors.AppendLine("Укажите название City");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //RecordTextDate.Text = SelectedItem.Date.ToString();
            //RecordTextDeliveryAddress.Text = SelectedItem.DeliveryAddress.ToString();
            //RecordTextCity.Text = SelectedItem.City.ToString();

            if (DlgMode == 0)
            {
                var NewOrder = new Base.Order();
                NewOrder.Date = Convert.ToDateTime(RecordTextDate.Text);
                NewOrder.DeliveryAddress = RecordTextDeliveryAddress.Text.Trim();
                NewOrder.City = RecordTextCity.Text.Trim();
                SourceCore.MyBase.Order.Add(NewOrder);
                SelectedItem = NewOrder;
            }
            else
            {
                var EditOrder = new Base.Order();
                EditOrder = SourceCore.MyBase.Order.First(p => p.Order_ID == SelectedItem.Order_ID);
                EditOrder.Date = Convert.ToDateTime(RecordTextDate.Text);
                EditOrder.DeliveryAddress = RecordTextDeliveryAddress.Text.Trim();
                EditOrder.City = RecordTextCity.Text.Trim();
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

        private void OrderAddRollback_Click(object sender, RoutedEventArgs e)
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
                    OrderGrid.ItemsSource = SourceCore.MyBase.Order.Where(q => q.Date.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    OrderGrid.ItemsSource = SourceCore.MyBase.Order.Where(q => q.DeliveryAddress.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    OrderGrid.ItemsSource = SourceCore.MyBase.Order.Where(q => q.City.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }
}
