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

namespace KR2.Pages
{
    /// <summary>
    /// Interaction logic for DeptorsPage.xaml
    /// </summary>
    public partial class DeptorsPage : Page
    {
        public DeptorsPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Debtors SelectedItem;
        public ObservableCollection<Base.Debtors> Debtorss;


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int i = 0; i < 2; i++)
            {
                Columns.Add(DebtorsGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in DebtorsGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }

        public void UpdateGrid(Base.Debtors Debtors)
        {
            if ((Debtors == null) && (DebtorsGrid.ItemsSource != null))
            {
                Debtors = (Base.Debtors)DebtorsGrid.SelectedItem;
            }
            Debtorss = new ObservableCollection<Base.Debtors>(SourceCore.MyBase.Debtors);
            DebtorsGrid.ItemsSource = Debtorss;
            DebtorsGrid.SelectedItem = Debtors;
        }



        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                DebtorsGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                DebtorsGrid.IsHitTestVisible = true;
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
            if (DebtorsGrid.SelectedItem != null)
            {
                DlgLoad(true, "Копировать");
                SelectedItem = (Base.Debtors)DebtorsGrid.SelectedItem;
                RecordTextName_debtor.Text = SelectedItem.name_debtor;
                RecordTextINN.Text = SelectedItem.INN;
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DebtorsGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.Debtors)DebtorsGrid.SelectedItem;
                RecordTextName_debtor.Text = SelectedItem.name_debtor;
                RecordTextINN.Text = SelectedItem.INN;
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
                    Base.Debtors DeletingDebtors = (Base.Debtors)DebtorsGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (DebtorsGrid.SelectedIndex < DebtorsGrid.Items.Count - 1)
                    {
                        DebtorsGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (DebtorsGrid.SelectedIndex > 0)
                        {
                            DebtorsGrid.SelectedIndex--;
                        }
                    }
                    Base.Debtors SelectingDebtors = (Base.Debtors)DebtorsGrid.SelectedItem;
                    SourceCore.MyBase.Debtors.Remove(DeletingDebtors);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingDebtors);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            if (string.IsNullOrEmpty(RecordTextINN.Text))
                errors.AppendLine("Укажите название INN");

            if (string.IsNullOrEmpty(RecordTextName_debtor.Text))
                errors.AppendLine("Укажите название name_debtor");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                var NewDebtors = new Base.Debtors();
                NewDebtors.name_debtor = RecordTextName_debtor.Text.Trim();
                NewDebtors.INN = RecordTextINN.Text.Trim();
                SourceCore.MyBase.Debtors.Add(NewDebtors);
                SelectedItem = NewDebtors;
            }
            else
            {
                var EditDebtors = new Base.Debtors();
                EditDebtors = SourceCore.MyBase.Debtors.First(p => p.id_debtor == SelectedItem.id_debtor);
                EditDebtors.name_debtor = RecordTextName_debtor.Text.Trim();
                EditDebtors.INN = RecordTextINN.Text.Trim();
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
        }

        private void AddRollback_Click(object sender, RoutedEventArgs e)
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
                    DebtorsGrid.ItemsSource = SourceCore.MyBase.Debtors.Where(q => q.name_debtor.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    DebtorsGrid.ItemsSource = SourceCore.MyBase.Debtors.Where(q => q.INN.Contains(textbox.Text)).ToList();
                    break;

            }
        }
    }
}
