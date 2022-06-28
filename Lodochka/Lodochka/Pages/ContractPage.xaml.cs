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
    /// Interaction logic for ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        public ContractPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Contract SelectedItem;
        public ObservableCollection<Base.Contract> Contracts;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int i = 0; i < 5; i++)
            {
                Columns.Add(ContractGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in ContractGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
        public void UpdateGrid(Base.Contract Contract)
        {
            if ((Contract == null) && (ContractGrid.ItemsSource != null))
            {
                Contract = (Base.Contract)ContractGrid.SelectedItem;
            }
            Contracts = new ObservableCollection<Base.Contract>(SourceCore.MyBase.Contract);
            ContractGrid.ItemsSource = Contracts;
            ContractGrid.SelectedItem = Contract;
        }



        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                ContractGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                ContractAddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                ContractGrid.IsHitTestVisible = true;
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
            if (ContractGrid.SelectedItem != null)
            {
                DlgLoad(true, "Копировать");
                SelectedItem = (Base.Contract)ContractGrid.SelectedItem;
                RecordTextDate.Text = SelectedItem.Date.ToString();
                RecordTextDepositPayed.Text = SelectedItem.DepositPayed.ToString();
                RecordTextContractTotalPrice.Text = SelectedItem.ContractTotalPrice.ToString();
                RecordTextContracTotalPrice_inclVAT.Text = SelectedItem.ContracTotalPrice_inclVAT.ToString();
                RecordTextProductionProcess.Text = SelectedItem.ProductionProcess;
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ContractGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.Contract)ContractGrid.SelectedItem;
                RecordTextDate.Text = SelectedItem.Date.ToString();
                RecordTextDepositPayed.Text = SelectedItem.DepositPayed.ToString();
                RecordTextContractTotalPrice.Text = SelectedItem.ContractTotalPrice.ToString();
                RecordTextContracTotalPrice_inclVAT.Text = SelectedItem.ContracTotalPrice_inclVAT.ToString();
                RecordTextProductionProcess.Text = SelectedItem.ProductionProcess;
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
                    Base.Contract Deleting = (Base.Contract)ContractGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (ContractGrid.SelectedIndex < ContractGrid.Items.Count - 1)
                    {
                        ContractGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (ContractGrid.SelectedIndex > 0)
                        {
                            ContractGrid.SelectedIndex--;
                        }
                    }
                    Base.Contract SelectingContract = (Base.Contract)ContractGrid.SelectedItem;
                    SourceCore.MyBase.Contract.Remove(Deleting);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingContract);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void ContractAddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextDate.Text))
                errors.AppendLine("Укажите название Date");

            if (string.IsNullOrEmpty(RecordTextDepositPayed.Text))
                errors.AppendLine("Укажите название DepositPayed");

            if (string.IsNullOrEmpty(RecordTextContractTotalPrice.Text))
                errors.AppendLine("Укажите название ContractTotalPrice");

            if (string.IsNullOrEmpty(RecordTextContractTotalPrice.Text))
                errors.AppendLine("Укажите название ContracTotalPrice_inclVAT");

            if (string.IsNullOrEmpty(RecordTextProductionProcess.Text))
                errors.AppendLine("Укажите название ProductionProcess");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                var NewContract = new Base.Contract();
                NewContract.Date = Convert.ToDateTime(RecordTextDate.Text);
                NewContract.DepositPayed = Convert.ToDecimal(RecordTextDepositPayed.Text.Replace('.', ','));
                NewContract.ContractTotalPrice = int.Parse(RecordTextContractTotalPrice.Text);
                NewContract.ContracTotalPrice_inclVAT = Convert.ToDecimal(RecordTextContractTotalPrice.Text.Replace('.', ','));
                NewContract.ProductionProcess = RecordTextProductionProcess.Text.Trim();
                SourceCore.MyBase.Contract.Add(NewContract);
                SelectedItem = NewContract;
            }
            else
            {
                var EditContract = new Base.Contract();
                EditContract = SourceCore.MyBase.Contract.First(p => p.Contract_ID == SelectedItem.Contract_ID);
                EditContract.Date = Convert.ToDateTime(RecordTextDate.Text);
                EditContract.DepositPayed = Convert.ToDecimal(RecordTextDepositPayed.Text.Replace('.', ','));
                EditContract.ContractTotalPrice = int.Parse(RecordTextContractTotalPrice.Text);
                EditContract.ContracTotalPrice_inclVAT = Convert.ToDecimal(RecordTextContractTotalPrice.Text.Replace('.', ','));
                EditContract.ProductionProcess = RecordTextProductionProcess.Text.Trim();
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

        private void ContractAddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedItem);
            DlgLoad(false, "");
        }

        private string getDateString(string textbox)
        {
            string[] dates = textbox.Split('.');
            if (dates.Length == 0)
                return "";

            StringBuilder date = new StringBuilder(10);
            if (dates.Length >= 3)
            {
                int.TryParse(dates[2], out int year);
                if (year != 0) date.Append($"{dates[2]}-");
            }

            if (dates.Length >= 2)
            {
                int.TryParse(dates[1], out int month);
                if (month != 0) date.Append($"{dates[1]}-");
            }

            int.TryParse(dates[0], out int day);
            if (day != 0) date.Append(dates[0]);

            return date.ToString();
        }


        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    string str = getDateString(textbox.Text);
                    ContractGrid.ItemsSource = SourceCore.MyBase.Contract.Where(q => q.Date.ToString().Contains(str)).ToList();
                    break;
                case 1:
                    ContractGrid.ItemsSource = SourceCore.MyBase.Contract.Where(q => q.DepositPayed.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    ContractGrid.ItemsSource = SourceCore.MyBase.Contract.Where(q => q.ContractTotalPrice.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    ContractGrid.ItemsSource = SourceCore.MyBase.Contract.Where(q => q.ContracTotalPrice_inclVAT.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    ContractGrid.ItemsSource = SourceCore.MyBase.Contract.Where(q => q.ProductionProcess.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }
}
