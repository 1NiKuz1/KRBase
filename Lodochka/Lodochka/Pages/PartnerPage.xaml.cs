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
    /// Interaction logic for PartnerPage.xaml
    /// </summary>
    public partial class PartnerPage : Page
    {
        public PartnerPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Partner SelectedItem;
        public ObservableCollection<Base.Partner> Partners;

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
                Columns.Add(PartnerGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in PartnerGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
        public void UpdateGrid(Base.Partner Partner)
        {
            if ((Partner == null) && (PartnerGrid.ItemsSource != null))
            {
                Partner = (Base.Partner)PartnerGrid.SelectedItem;
            }
            Partners = new ObservableCollection<Base.Partner>(SourceCore.MyBase.Partner);
            PartnerGrid.ItemsSource = Partners;
            PartnerGrid.SelectedItem = Partner;
        }



        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                PartnerGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                PartnerAddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                PartnerGrid.IsHitTestVisible = true;
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
            if (PartnerGrid.SelectedItem != null)
            {
                DlgLoad(true, "Копировать");
                SelectedItem = (Base.Partner)PartnerGrid.SelectedItem;
                RecordTextName.Text = SelectedItem.Name.ToString();
                RecordTextAddress.Text = SelectedItem.Address.ToString();
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
            if (PartnerGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.Partner)PartnerGrid.SelectedItem;
                RecordTextName.Text = SelectedItem.Name.ToString();
                RecordTextAddress.Text = SelectedItem.Address.ToString();
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
                    Base.Partner Deleting = (Base.Partner)PartnerGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (PartnerGrid.SelectedIndex < PartnerGrid.Items.Count - 1)
                    {
                        PartnerGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (PartnerGrid.SelectedIndex > 0)
                        {
                            PartnerGrid.SelectedIndex--;
                        }
                    }
                    Base.Partner SelectingPartner = (Base.Partner)PartnerGrid.SelectedItem;
                    SourceCore.MyBase.Partner.Remove(Deleting);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingPartner);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void PartnerAddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextName.Text))
                errors.AppendLine("Укажите название Name");

            if (string.IsNullOrEmpty(RecordTextAddress.Text))
                errors.AppendLine("Укажите название Address");

            if (string.IsNullOrEmpty(RecordTextCity.Text))
                errors.AppendLine("Укажите название City");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            if (DlgMode == 0)
            {
                var NewPartner = new Base.Partner();
                NewPartner.Name = RecordTextName.Text.Trim();
                NewPartner.Address = RecordTextAddress.Text.Trim();
                NewPartner.City = RecordTextCity.Text.Trim();
                SourceCore.MyBase.Partner.Add(NewPartner);
                SelectedItem = NewPartner;
            }
            else
            {
                var EditPartner = new Base.Partner();
                EditPartner = SourceCore.MyBase.Partner.First(p => p.Partner_ID == SelectedItem.Partner_ID);
                EditPartner.Name = RecordTextName.Text.Trim();
                EditPartner.Address = RecordTextAddress.Text.Trim();
                EditPartner.City = RecordTextCity.Text.Trim();
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

        private void PartnerAddRollback_Click(object sender, RoutedEventArgs e)
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
                    PartnerGrid.ItemsSource = SourceCore.MyBase.Partner.Where(q => q.Name.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PartnerGrid.ItemsSource = SourceCore.MyBase.Partner.Where(q => q.Address.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    PartnerGrid.ItemsSource = SourceCore.MyBase.Partner.Where(q => q.City.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }
}
