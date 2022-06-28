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
    /// Interaction logic for AccessoryPage.xaml
    /// </summary>
    public partial class AccessoryPage : Page
    {
        public AccessoryPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);           
            AccessoryDlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Accessory SelectedItem;
        public ObservableCollection<Base.Accessory> Accessorys;


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int i = 0; i < 7; i++)
            {
                Columns.Add(AccessoryGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in AccessoryGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }

        public void UpdateGrid(Base.Accessory accessory)
        {
            if ((accessory == null) && (AccessoryGrid.ItemsSource != null))
            {
                accessory = (Base.Accessory)AccessoryGrid.SelectedItem;
            }
            Accessorys = new ObservableCollection<Base.Accessory>(SourceCore.MyBase.Accessory);
            AccessoryGrid.ItemsSource = Accessorys;
            AccessoryGrid.SelectedItem = accessory;
            RecordComboBoxPartner.ItemsSource = SourceCore.MyBase.Partner.ToList();
        }



        public void AccessoryDlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                AccessoryGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                AccessoryAddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                AccessoryGrid.IsHitTestVisible = true;
                RecordAdd.IsEnabled = true;
                RecordCopy.IsEnabled = true;
                RecordEdit.IsEnabled = true;
                RecordDellete.IsEnabled = true;
                DlgMode = -1;
            }
        }

        private void RecordAdd_Click(object sender, RoutedEventArgs e)
        {
            AccessoryDlgLoad(true, "Добавить");
            DataContext = null;
            DlgMode = 0;
        }

        private void RecordkCopy_Click(object sender, RoutedEventArgs e)
        {
            if (AccessoryGrid.SelectedItem != null)
            {
                AccessoryDlgLoad(true, "Копировать");
                SelectedItem = (Base.Accessory)AccessoryGrid.SelectedItem;
                RecordTextAccName.Text = SelectedItem.AccName;
                RecordTextDescriptionOfAccessory.Text = SelectedItem.DescriptionOfAccessory;
                RecordTextPrice.Text = SelectedItem.Price.ToString();
                RecordTextVAT.Text = SelectedItem.VAT.ToString();
                RecordTextInventory.Text = SelectedItem.Inventory.ToString();
                RecordTextOrderLevel.Text = SelectedItem.OrderLevel.ToString();
                RecordTextOrderBatch.Text = SelectedItem.OrderBatch.ToString();
                RecordComboBoxPartner.Text = SelectedItem.Partner.Name;
                DlgMode = 0;  
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }

        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (AccessoryGrid.SelectedItem != null)
            {
                AccessoryDlgLoad(true, "Изменить");
                SelectedItem = (Base.Accessory)AccessoryGrid.SelectedItem;
                RecordTextAccName.Text = SelectedItem.AccName;
                RecordTextDescriptionOfAccessory.Text = SelectedItem.DescriptionOfAccessory;
                RecordTextPrice.Text = SelectedItem.Price.ToString();
                RecordTextVAT.Text = SelectedItem.VAT.ToString();
                RecordTextInventory.Text = SelectedItem.Inventory.ToString();
                RecordTextOrderLevel.Text = SelectedItem.OrderLevel.ToString();
                RecordTextOrderBatch.Text = SelectedItem.OrderBatch.ToString();
                RecordComboBoxPartner.Text = SelectedItem.Partner.Name;
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
                    Base.Accessory DeletingAccessory = (Base.Accessory)AccessoryGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (AccessoryGrid.SelectedIndex < AccessoryGrid.Items.Count - 1)
                    {
                        AccessoryGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (AccessoryGrid.SelectedIndex > 0)
                        {
                            AccessoryGrid.SelectedIndex--;
                        }
                    }
                    Base.Accessory SelectingAccessory = (Base.Accessory)AccessoryGrid.SelectedItem;
                    SourceCore.MyBase.Accessory.Remove(DeletingAccessory);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingAccessory);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void AccessoryAddCommit_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (RecordComboBoxPartner.SelectedItem == null)
                errors.AppendLine("Укажите партнера");

            if (string.IsNullOrEmpty(RecordTextAccName.Text))
                errors.AppendLine("Укажите название AccName");

            if (string.IsNullOrEmpty(RecordTextDescriptionOfAccessory.Text))
                errors.AppendLine("Укажите название DescriptionOfAccessory");

            if (string.IsNullOrEmpty(RecordTextPrice.Text))
                errors.AppendLine("Укажите название Price");

            if (string.IsNullOrEmpty(RecordTextVAT.Text))
                errors.AppendLine("Укажите название VAT");

            if (string.IsNullOrEmpty(RecordTextInventory.Text))
                errors.AppendLine("Укажите название Inventory");

            if (string.IsNullOrEmpty(RecordTextOrderLevel.Text))
                errors.AppendLine("Укажите название OrderLevel");

            if (string.IsNullOrEmpty(RecordTextOrderBatch.Text))
                errors.AppendLine("Укажите название OrderBatch");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }         

            if (DlgMode == 0)
            {
                var NewAccessory = new Base.Accessory();
                NewAccessory.AccName = RecordTextAccName.Text.Trim();
                NewAccessory.DescriptionOfAccessory = RecordTextDescriptionOfAccessory.Text.Trim();
                NewAccessory.Price = Convert.ToDecimal(RecordTextPrice.Text.Replace('.', ','));
                NewAccessory.VAT = Convert.ToDecimal(RecordTextVAT.Text.Replace('.', ','));
                NewAccessory.Inventory = int.Parse(RecordTextInventory.Text);
                NewAccessory.OrderLevel = int.Parse(RecordTextOrderLevel.Text);
                NewAccessory.OrderBatch = int.Parse(RecordTextOrderBatch.Text);
                NewAccessory.Partner = (Base.Partner)RecordComboBoxPartner.SelectedItem;
                SourceCore.MyBase.Accessory.Add(NewAccessory);
                SelectedItem = NewAccessory;
            } else
            {
                var EditAccessory = new Base.Accessory();
                EditAccessory = SourceCore.MyBase.Accessory.First(p => p.Accessory_ID == SelectedItem.Accessory_ID);
                EditAccessory.AccName = RecordTextAccName.Text.Trim();
                EditAccessory.DescriptionOfAccessory = RecordTextDescriptionOfAccessory.Text.Trim();
                EditAccessory.Price = Convert.ToDecimal(RecordTextPrice.Text.Replace('.', ','));
                EditAccessory.VAT = Convert.ToDecimal(RecordTextVAT.Text.Replace('.', ','));
                EditAccessory.Inventory = int.Parse(RecordTextInventory.Text);
                EditAccessory.OrderLevel = int.Parse(RecordTextOrderLevel.Text);
                EditAccessory.OrderBatch = int.Parse(RecordTextOrderBatch.Text);
                EditAccessory.Partner = (Base.Partner)RecordComboBoxPartner.SelectedItem;
            }

            try
            {
                SourceCore.MyBase.SaveChanges();
                UpdateGrid(SelectedItem);
                AccessoryDlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }        
            //DlgMode = -1;
        }

        private void AccessoryAddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedItem);
            AccessoryDlgLoad(false, "");
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    AccessoryGrid.ItemsSource = SourceCore.MyBase.Accessory.Where(q => q.AccName.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    AccessoryGrid.ItemsSource = SourceCore.MyBase.Accessory.Where(q => q.DescriptionOfAccessory.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    AccessoryGrid.ItemsSource = SourceCore.MyBase.Accessory.Where(q => q.Price.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    AccessoryGrid.ItemsSource = SourceCore.MyBase.Accessory.Where(q => q.VAT.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    AccessoryGrid.ItemsSource = SourceCore.MyBase.Accessory.Where(q => q.Inventory.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 5:
                    AccessoryGrid.ItemsSource = SourceCore.MyBase.Accessory.Where(q => q.OrderLevel.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 6:
                    AccessoryGrid.ItemsSource = SourceCore.MyBase.Accessory.Where(q => q.OrderBatch.ToString().Contains(textbox.Text)).ToList();
                    break;
                //case 7:
                //    AccessoryGrid.ItemsSource = SourceCore.MyBase.Accessory.Where(q => q.Partner.ToString().Contains(textbox.Text)).ToList();
                //    break;
            }

        }
    }
}
