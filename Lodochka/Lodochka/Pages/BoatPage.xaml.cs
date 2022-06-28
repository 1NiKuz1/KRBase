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
    /// Interaction logic for BoatPage.xaml
    /// </summary>
    public partial class BoatPage : Page
    {
        public BoatPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Boat SelectedItem;
        public ObservableCollection<Base.Boat> Boats;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int i = 0; i < 8; i++)
            {
                Columns.Add(BoatGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in BoatGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
        public void UpdateGrid(Base.Boat Boat)
        {
            if ((Boat == null) && (BoatGrid.ItemsSource != null))
            {
                Boat = (Base.Boat)BoatGrid.SelectedItem;
            }
            Boats = new ObservableCollection<Base.Boat>(SourceCore.MyBase.Boat);
            BoatGrid.ItemsSource = Boats;
            BoatGrid.SelectedItem = Boat;
        }



        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                BoatGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                BoatAddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                BoatGrid.IsHitTestVisible = true;
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
            if (BoatGrid.SelectedItem != null)
            {
                DlgLoad(true, "Копировать");
                SelectedItem = (Base.Boat)BoatGrid.SelectedItem;
                RecordTextModel.Text = SelectedItem.Model;
                RecordTextBoatType.Text = SelectedItem.BoatType.ToString();
                RecordTextNumberOfRowers.Text = SelectedItem.NumberOfRowers.ToString();
                RecordTextMast.Text = SelectedItem.Mast.ToString();
                RecordTextColour.Text = SelectedItem.Colour;
                RecordTextWood.Text = SelectedItem.Wood;
                RecordTextBasePrice.Text = SelectedItem.BasePrice.ToString();
                RecordTextVAT.Text = SelectedItem.VAT.ToString();
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (BoatGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.Boat)BoatGrid.SelectedItem;
                RecordTextModel.Text = SelectedItem.Model;
                RecordTextBoatType.Text = SelectedItem.BoatType.ToString();
                RecordTextNumberOfRowers.Text = SelectedItem.NumberOfRowers.ToString();
                RecordTextMast.Text = SelectedItem.Mast.ToString();
                RecordTextColour.Text = SelectedItem.Colour;
                RecordTextWood.Text = SelectedItem.Wood;
                RecordTextBasePrice.Text = SelectedItem.BasePrice.ToString();
                RecordTextVAT.Text = SelectedItem.VAT.ToString();
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
                    Base.Boat Deleting = (Base.Boat)BoatGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (BoatGrid.SelectedIndex < BoatGrid.Items.Count - 1)
                    {
                        BoatGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (BoatGrid.SelectedIndex > 0)
                        {
                            BoatGrid.SelectedIndex--;
                        }
                    }
                    Base.Boat SelectingBoat = (Base.Boat)BoatGrid.SelectedItem;
                    SourceCore.MyBase.Boat.Remove(Deleting);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingBoat);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void BoatAddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextModel.Text))
                errors.AppendLine("Укажите название Model");

            if (string.IsNullOrEmpty(RecordTextBoatType.Text))
                errors.AppendLine("Укажите название BoatType");

            if (string.IsNullOrEmpty(RecordTextNumberOfRowers.Text))
                errors.AppendLine("Укажите название NumberOfRowers");

            if (string.IsNullOrEmpty(RecordTextMast.Text))
                errors.AppendLine("Укажите название Mast");

            if (string.IsNullOrEmpty(RecordTextColour.Text))
                errors.AppendLine("Укажите название Colour");

            if (string.IsNullOrEmpty(RecordTextWood.Text))
                errors.AppendLine("Укажите название Wood");

            if (string.IsNullOrEmpty(RecordTextBasePrice.Text))
                errors.AppendLine("Укажите название BasePrice");

            if (string.IsNullOrEmpty(RecordTextVAT.Text))
                errors.AppendLine("Укажите название BasePrice");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                var NewBoat = new Base.Boat();
                NewBoat.Model = RecordTextModel.Text.Trim();
                NewBoat.BoatType = RecordTextBoatType.Text.Trim();
                NewBoat.NumberOfRowers = int.Parse(RecordTextNumberOfRowers.Text);
                NewBoat.Mast = Convert.ToBoolean(RecordTextMast.Text);
                NewBoat.Colour = RecordTextColour.Text;
                NewBoat.Wood = RecordTextColour.Text;
                NewBoat.BasePrice = Convert.ToDecimal(RecordTextBasePrice.Text.Replace('.', ','));
                NewBoat.VAT = Convert.ToDecimal(RecordTextVAT.Text.Replace('.', ','));
                SourceCore.MyBase.Boat.Add(NewBoat);
                SelectedItem = NewBoat;
            }
            else
            {
                var EditBoat = new Base.Boat();
                EditBoat = SourceCore.MyBase.Boat.First(p => p.boat_ID == SelectedItem.boat_ID);
                EditBoat.Model = RecordTextModel.Text.Trim();
                EditBoat.BoatType = RecordTextBoatType.Text.Trim();
                EditBoat.NumberOfRowers = int.Parse(RecordTextNumberOfRowers.Text);
                EditBoat.Mast = Convert.ToBoolean(RecordTextMast.Text);
                EditBoat.Colour = RecordTextColour.Text;
                EditBoat.Wood = RecordTextColour.Text;
                EditBoat.BasePrice = Convert.ToDecimal(RecordTextBasePrice.Text.Replace('.', ','));
                EditBoat.VAT = Convert.ToDecimal(RecordTextVAT.Text.Replace('.', ','));
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

        private void BoatAddRollback_Click(object sender, RoutedEventArgs e)
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
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat.Where(q => q.Model.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat.Where(q => q.BoatType.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat.Where(q => q.NumberOfRowers.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat.Where(q => q.Mast.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat.Where(q => q.Colour.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 5:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat.Where(q => q.Wood.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 6:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat.Where(q => q.BasePrice.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 7:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat.Where(q => q.VAT.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }

   
}
