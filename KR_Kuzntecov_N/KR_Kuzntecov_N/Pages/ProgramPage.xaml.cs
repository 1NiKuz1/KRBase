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

namespace KR_Kuzntecov_N.Pages
{
    /// <summary>
    /// Interaction logic for ProgramPage.xaml
    /// </summary>
    public partial class ProgramPage : Page
    {
        public ProgramPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            GosProgramDlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.GosProgram SelectedItem;
        public ObservableCollection<Base.GosProgram> GosPrograms;


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
                Columns.Add(GosProgramGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in GosProgramGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }

        public void UpdateGrid(Base.GosProgram GosProgram)
        {
            if ((GosProgram == null) && (GosProgramGrid.ItemsSource != null))
            {
                GosProgram = (Base.GosProgram)GosProgramGrid.SelectedItem;
            }
            GosPrograms = new ObservableCollection<Base.GosProgram>(SourceCore.MyBase.GosProgram);
            GosProgramGrid.ItemsSource = GosPrograms;
            GosProgramGrid.SelectedItem = GosProgram;
            RecordComboBoxExecuter.ItemsSource = SourceCore.MyBase.Executers.ToList();
        }



        public void GosProgramDlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                GosProgramGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                GosProgramAddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                GosProgramGrid.IsHitTestVisible = true;
                RecordAdd.IsEnabled = true;
                RecordCopy.IsEnabled = true;
                RecordEdit.IsEnabled = true;
                RecordDellete.IsEnabled = true;
                DlgMode = -1;
            }
        }

        private void RecordAdd_Click(object sender, RoutedEventArgs e)
        {
            GosProgramDlgLoad(true, "Добавить");
            DataContext = null;
            DlgMode = 0;
        }

        private void RecordkCopy_Click(object sender, RoutedEventArgs e)
        {
            if (GosProgramGrid.SelectedItem != null)
            {
                GosProgramDlgLoad(true, "Копировать");
                SelectedItem = (Base.GosProgram)GosProgramGrid.SelectedItem;
                RecordTextName.Text = SelectedItem.name;
                RecordTextDateStart.Text = SelectedItem.dateStart.ToString();
                RecordTextDateEnd.Text = SelectedItem.dateEnd.ToString();
                RecordComboBoxExecuter.Text = SelectedItem.Executers.name;
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (GosProgramGrid.SelectedItem != null)
            {
                GosProgramDlgLoad(true, "Изменить");
                SelectedItem = (Base.GosProgram)GosProgramGrid.SelectedItem;
                RecordTextName.Text = SelectedItem.name;
                RecordTextDateStart.Text = SelectedItem.dateStart.ToString();
                RecordTextDateEnd.Text = SelectedItem.dateEnd.ToString();
                RecordComboBoxExecuter.Text = SelectedItem.Executers.name;
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
                    Base.GosProgram DeletingGosProgram = (Base.GosProgram)GosProgramGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (GosProgramGrid.SelectedIndex < GosProgramGrid.Items.Count - 1)
                    {
                        GosProgramGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (GosProgramGrid.SelectedIndex > 0)
                        {
                            GosProgramGrid.SelectedIndex--;
                        }
                    }
                    Base.GosProgram SelectingGosProgram = (Base.GosProgram)GosProgramGrid.SelectedItem;
                    SourceCore.MyBase.GosProgram.Remove(DeletingGosProgram);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingGosProgram);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }      

        private void GosProgramAddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (RecordComboBoxExecuter.SelectedItem == null)
                errors.AppendLine("Укажите исполнителя");

            if (string.IsNullOrEmpty(RecordTextName.Text))
                errors.AppendLine("Укажите название программы");

            if (string.IsNullOrEmpty(RecordTextDateStart.Text))
                errors.AppendLine("Укажите дату начала");

            if (string.IsNullOrEmpty(RecordTextDateEnd.Text))
                errors.AppendLine("Укажите дату конца");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                var NewGosProgram = new Base.GosProgram();
                NewGosProgram.name = RecordTextName.Text.Trim();
                NewGosProgram.dateStart = int.Parse(RecordTextDateStart.Text);
                NewGosProgram.dateEnd = int.Parse(RecordTextDateEnd.Text);
                //NewGosProgram.Executers.name = (Base.Executers)RecordComboBoxExecuter.SelectedItem;
                SourceCore.MyBase.GosProgram.Add(NewGosProgram);
                SelectedItem = NewGosProgram;
            }
            else
            {
                var EditGosProgram = new Base.GosProgram();
                EditGosProgram = SourceCore.MyBase.GosProgram.First(p => p.idGosProg == SelectedItem.idGosProg);
                EditGosProgram.name = RecordTextName.Text.Trim();
                EditGosProgram.dateStart = int.Parse(RecordTextDateStart.Text);
                EditGosProgram.dateEnd = int.Parse(RecordTextDateEnd.Text);
                //EditGosProgram.Executers.name = (Base.Executers)RecordComboBoxExecuter.SelectedItem;
            }

            try
            {
                SourceCore.MyBase.SaveChanges();
                UpdateGrid(SelectedItem);
                GosProgramDlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GosProgramAddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedItem);
            GosProgramDlgLoad(false, "");
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    GosProgramGrid.ItemsSource = SourceCore.MyBase.GosProgram.Where(q => q.name.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    GosProgramGrid.ItemsSource = SourceCore.MyBase.GosProgram.Where(q => q.dateStart.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    GosProgramGrid.ItemsSource = SourceCore.MyBase.GosProgram.Where(q => q.dateEnd.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }
}
