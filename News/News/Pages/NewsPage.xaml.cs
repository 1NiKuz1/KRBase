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

namespace News.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewsPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        public NewsPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.News SelectedItem;
        public ObservableCollection<Base.News> Newss;

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
                Columns.Add(NewsGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in NewsGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }

        public void UpdateGrid(Base.News News)
        {
            if ((News == null) && (NewsGrid.ItemsSource != null))
            {
                News = (Base.News)NewsGrid.SelectedItem;
            }
            Newss = new ObservableCollection<Base.News>(SourceCore.MyBase.News);
            NewsGrid.ItemsSource = Newss;
            NewsGrid.SelectedItem = News;
        }



        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                NewsGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                RecordAdd.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                NewsGrid.IsHitTestVisible = true;
                RecordAdd.IsEnabled = true;
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

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (NewsGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.News)NewsGrid.SelectedItem;
                RecordText_header.Text = SelectedItem.header;
                RecordText_desc.Text = SelectedItem.description;
                RecordText_text.Text = SelectedItem.full_text;
                RecordText_dMake.Text = SelectedItem.date_make.ToString();
                RecordText_dUpdate.Text = SelectedItem.date_update.ToString();
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
                    Base.News DeletingNews = (Base.News)NewsGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (NewsGrid.SelectedIndex < NewsGrid.Items.Count - 1)
                    {
                        NewsGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (NewsGrid.SelectedIndex > 0)
                        {
                            NewsGrid.SelectedIndex--;
                        }
                    }
                    Base.News SelectingNews = (Base.News)NewsGrid.SelectedItem;
                    SourceCore.MyBase.News.Remove(DeletingNews);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingNews);
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

            if (string.IsNullOrEmpty(RecordText_header.Text))
                errors.AppendLine("Укажите название header");

            if (string.IsNullOrEmpty(RecordText_desc.Text))
                errors.AppendLine("Укажите название description");

            if (string.IsNullOrEmpty(RecordText_text.Text))
                errors.AppendLine("Укажите название full_text");

            if (string.IsNullOrEmpty(RecordText_dMake.Text))
                errors.AppendLine("Укажите название date_make");

            if (string.IsNullOrEmpty(RecordText_dUpdate.Text))
                errors.AppendLine("Укажите название date_update");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                var NewNews = new Base.News();
                NewNews.header = RecordText_header.Text.Trim();
                NewNews.description = RecordText_desc.Text.Trim();
                NewNews.full_text = RecordText_text.Text.Trim();
                NewNews.date_make = Convert.ToDateTime(RecordText_dMake.Text);
                NewNews.date_update = Convert.ToDateTime(RecordText_dUpdate.Text);
                SourceCore.MyBase.News.Add(NewNews);
                SelectedItem = NewNews;
            }
            else
            {
                var EditNews = new Base.News();
                EditNews = SourceCore.MyBase.News.First(p => p.news_id == SelectedItem.news_id);
                EditNews.header = RecordText_header.Text.Trim();
                EditNews.description = RecordText_desc.Text.Trim();
                EditNews.full_text = RecordText_text.Text.Trim();
                EditNews.date_make = Convert.ToDateTime(RecordText_dMake.Text);
                EditNews.date_update = Convert.ToDateTime(RecordText_dUpdate.Text);
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
                    NewsGrid.ItemsSource = SourceCore.MyBase.News.Where(q => q.header.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    NewsGrid.ItemsSource = SourceCore.MyBase.News.Where(q => q.description.Contains(textbox.Text)).ToList();
                    break;

            }
        }
    }
}
