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
    /// Interaction logic for InvoicePage.xaml
    /// </summary>
    public partial class InvoicePage : Page
    {
        public InvoicePage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Invoice SelectedItem;
        public ObservableCollection<Base.Invoice> Invoices;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int i = 0; i < 4; i++)
            {
                Columns.Add(InvoiceGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in InvoiceGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
        public void UpdateGrid(Base.Invoice Invoice)
        {
            if ((Invoice == null) && (InvoiceGrid.ItemsSource != null))
            {
                Invoice = (Base.Invoice)InvoiceGrid.SelectedItem;
            }
            Invoices = new ObservableCollection<Base.Invoice>(SourceCore.MyBase.Invoice);
            InvoiceGrid.ItemsSource = Invoices;
            InvoiceGrid.SelectedItem = Invoice;
        }



        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                InvoiceGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                InvoiceAddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                InvoiceGrid.IsHitTestVisible = true;
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
            if (InvoiceGrid.SelectedItem != null)
            {
                DlgLoad(true, "Копировать");
                SelectedItem = (Base.Invoice)InvoiceGrid.SelectedItem;
                RecordTextSettled.Text = SelectedItem.Settled.ToString();
                RecordTextSum.Text = SelectedItem.Sum.ToString();
                RecordTextSum_inclVAT.Text = SelectedItem.Sum_inclVAT.ToString();
                RecordTextDate.Text = SelectedItem.Date.ToString();
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.Invoice)InvoiceGrid.SelectedItem;
                RecordTextSettled.Text = SelectedItem.Settled.ToString();
                RecordTextSum.Text = SelectedItem.Sum.ToString();
                RecordTextSum_inclVAT.Text = SelectedItem.Sum_inclVAT.ToString();
                RecordTextDate.Text = SelectedItem.Date.ToString();
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
                    Base.Invoice Deleting = (Base.Invoice)InvoiceGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (InvoiceGrid.SelectedIndex < InvoiceGrid.Items.Count - 1)
                    {
                        InvoiceGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (InvoiceGrid.SelectedIndex > 0)
                        {
                            InvoiceGrid.SelectedIndex--;
                        }
                    }
                    Base.Invoice SelectingInvoice = (Base.Invoice)InvoiceGrid.SelectedItem;
                    SourceCore.MyBase.Invoice.Remove(Deleting);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingInvoice);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void InvoiceAddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextSettled.Text))
                errors.AppendLine("Укажите название Settled");

            if (string.IsNullOrEmpty(RecordTextSum.Text))
                errors.AppendLine("Укажите название Sum");

            if (string.IsNullOrEmpty(RecordTextSum_inclVAT.Text))
                errors.AppendLine("Укажите название Sum_inclVAT");

            if (string.IsNullOrEmpty(RecordTextDate.Text))
                errors.AppendLine("Укажите название Date");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //RecordTextSettled.Text = SelectedItem.Settled.ToString();
            //RecordTextSum.Text = SelectedItem.Sum.ToString();
            //RecordTextSum_inclVAT.Text = SelectedItem.Sum_inclVAT.ToString();
            //RecordTextDate.Text = SelectedItem.Date.ToString();

            if (DlgMode == 0)
            {
                var NewInvoice = new Base.Invoice();
                NewInvoice.Date = Convert.ToDateTime(RecordTextDate.Text);
                NewInvoice.Settled = Convert.ToBoolean(RecordTextSettled.Text);
                NewInvoice.Sum = int.Parse(RecordTextSum.Text);
                NewInvoice.Sum_inclVAT = Convert.ToDecimal(RecordTextSum_inclVAT.Text.Replace('.', ','));
                SourceCore.MyBase.Invoice.Add(NewInvoice);
                SelectedItem = NewInvoice;
            }
            else
            {
                var EditInvoice = new Base.Invoice();
                EditInvoice = SourceCore.MyBase.Invoice.First(p => p.Invoice_ID == SelectedItem.Invoice_ID);
                EditInvoice.Date = Convert.ToDateTime(RecordTextDate.Text);
                EditInvoice.Settled = Convert.ToBoolean(RecordTextSettled.Text);
                EditInvoice.Sum = int.Parse(RecordTextSum.Text);
                EditInvoice.Sum_inclVAT = Convert.ToDecimal(RecordTextSum_inclVAT.Text.Replace('.', ','));
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

        private void InvoiceAddRollback_Click(object sender, RoutedEventArgs e)
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
                    InvoiceGrid.ItemsSource = SourceCore.MyBase.Invoice.Where(q => q.Date.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    InvoiceGrid.ItemsSource = SourceCore.MyBase.Invoice.Where(q => q.Settled.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    InvoiceGrid.ItemsSource = SourceCore.MyBase.Invoice.Where(q => q.Sum.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    InvoiceGrid.ItemsSource = SourceCore.MyBase.Invoice.Where(q => q.Sum_inclVAT.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }
}
