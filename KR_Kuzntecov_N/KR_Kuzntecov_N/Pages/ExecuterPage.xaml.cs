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
    /// Interaction logic for ExecuterPage.xaml
    /// </summary>
    public partial class ExecuterPage : Page
    {
        public ExecuterPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
        }

        public Base.Executers SelectedItem;
        public ObservableCollection<Base.Executers> Executerss;


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int i = 0; i < 1; i++)
            {
                Columns.Add(ExecutersGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in ExecutersGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }

        public void UpdateGrid(Base.Executers Executers)
        {
            if ((Executers == null) && (ExecutersGrid.ItemsSource != null))
            {
                Executers = (Base.Executers)ExecutersGrid.SelectedItem;
            }
            Executerss = new ObservableCollection<Base.Executers>(SourceCore.MyBase.Executers);
            ExecutersGrid.ItemsSource = Executerss;
            ExecutersGrid.SelectedItem = Executers;
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    ExecutersGrid.ItemsSource = SourceCore.MyBase.GosProgram.Where(q => q.name.Contains(textbox.Text)).ToList();
                    break;
            }
        }
    }
}
