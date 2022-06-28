using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
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

namespace TestApp.Pages
{   /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        
        private int DlgMode;
        public Base.Books SelectedBook;
        public ObservableCollection<Base.Books> Books;
     
               
        public BookPage()
        {
            InitializeComponent();
            UpdateGrid(null);
            BookDlgLoad(false,"");
            DataContext = this;
        }

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
                Columns.Add(BookDataGrid.Columns[i].Header.ToString());
            }
            BookFilterComboBox.ItemsSource = Columns;
            BookFilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in BookDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
 
        public void UpdateGrid(Base.Books Book)
        {
            if ((Book == null) && (BookDataGrid.ItemsSource != null))
            {
                Book = (Base.Books)BookDataGrid.SelectedItem;
            }

            Books = new ObservableCollection<Base.Books>(SourceCore.MyBase.Books);
            BookDataGrid.ItemsSource = Books;
            BookDataGrid.SelectedItem = Book;
            BookComboBoxPublishers.ItemsSource = SourceCore.MyBase.Publishers.ToList();
        }

        //Удаление книги - не требует вызова дополнительной панели
        private void BookDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    // Ссылка на удаляемую книгу
                    Base.Books DeletingBook = (Base.Books)BookDataGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (BookDataGrid.SelectedIndex < BookDataGrid.Items.Count - 1)
                    {
                        BookDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (BookDataGrid.SelectedIndex > 0)
                        {
                            BookDataGrid.SelectedIndex--;
                        }
                    }
                    Base.Books SelectingBook = (Base.Books)BookDataGrid.SelectedItem;
                    SourceCore.MyBase.Books.Remove(DeletingBook);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingBook);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        //Операции с книгой, требущие вызова дополнительной панели
        //Добавление новой книги
        private void BookAdd_Click(object sender, RoutedEventArgs e)
        {
            BookDlgLoad(true, "Добавить книгу");
            DataContext = null;
            DlgMode = 0;
        }

        //Добавление книги на основе выбранной
        private void BookCopy_Click(object sender, RoutedEventArgs e)
        {
            if (BookDataGrid.SelectedItem != null)
            {
                BookDlgLoad(true, "Копировать книгу");
                SelectedBook = (Base.Books)BookDataGrid.SelectedItem;
                BookTextName.Text = SelectedBook.Name;
                BookTextAuthors.Text = SelectedBook.Authors;
                BookComboBoxPublishers.Text = SelectedBook.Publishers.PublisherName;
                BookTextPublishYear.Text = SelectedBook.PublishYear.ToString();
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ниодной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        //Изменение книги
        private void BookEdit_Click(object sender, RoutedEventArgs e)
        {
            if (BookDataGrid.SelectedItem != null)
            {
                BookDlgLoad(true, "Изменить книгу");
                SelectedBook = (Base.Books)BookDataGrid.SelectedItem;
                BookTextName.Text = SelectedBook.Name;
                BookTextAuthors.Text = SelectedBook.Authors;
                BookComboBoxPublishers.Text = SelectedBook.Publishers.PublisherName;
                BookTextPublishYear.Text = SelectedBook.PublishYear.ToString();
            }
            else
            {
                MessageBox.Show("Не выбрано ниодной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        //Подтверждение выполненных действий - добавления, изменения (операций с дополнительной панелью)

        private void BookAddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            if (string.IsNullOrEmpty(BookTextName.Text))
                errors.AppendLine("Укажите название книги");
            if (string.IsNullOrEmpty(BookTextAuthors.Text))
                errors.AppendLine("Укажите название книги");
            if (((Base.Publishers)BookComboBoxPublishers.SelectedItem == null) || (BookComboBoxPublishers.Text == " ..."))
                errors.AppendLine("Укажите издательство");
            if (string.IsNullOrEmpty(BookTextPublishYear.Text))
                errors.AppendLine("Укажите название книги");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            if (DlgMode == 0)
            {
                var NewBook = new Base.Books();
                NewBook.Name = BookTextName.Text;
                NewBook.Authors = BookTextAuthors.Text;
                NewBook.Publishers = (Base.Publishers)BookComboBoxPublishers.SelectedItem;
                NewBook.PublishYear = (short?)int.Parse(BookTextPublishYear.Text);
                SourceCore.MyBase.Books.Add(NewBook);
                SelectedBook = NewBook;
            } else
            {
                var EditBook = new Base.Books();
                EditBook = SourceCore.MyBase.Books.First(p => p.IdBook == SelectedBook.IdBook);
                EditBook.Name = BookTextName.Text;
                EditBook.Authors = BookTextAuthors.Text;
                EditBook.Publishers = (Base.Publishers)BookComboBoxPublishers.SelectedItem;
                EditBook.PublishYear = (short?)int.Parse(BookTextPublishYear.Text);   
            }

            try
            {
                SourceCore.MyBase.SaveChanges();
                BookDlgLoad(false, "");
                UpdateGrid(SelectedBook);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //отказ от выбранной операции
        private void BookAddRollback_Click(object sender, RoutedEventArgs e)
        {
            BookDlgLoad(false,"");
            UpdateGrid(SelectedBook);
        }

        //настройка рабочей области
        public void BookDlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                BookColumnChange.Width = new GridLength(400);
                BookDataGrid.IsHitTestVisible = false;
                BookLabel.Content = DlgModeContent;
                BookAddCommit.Content = DlgModeContent;
                BookAdd.IsEnabled = false;
                BookCopy.IsEnabled = false;
                BookEdit.IsEnabled = false;
                BookDellete.IsEnabled = false;
            }
            else
            {
                BookColumnChange.Width = new GridLength(0);
                BookDataGrid.IsHitTestVisible = true;
                BookAdd.IsEnabled = true;
                BookCopy.IsEnabled = true;
                BookEdit.IsEnabled = true;
                BookDellete.IsEnabled = true;
                DlgMode = -1;
            }
        }

        private void BookFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (BookFilterComboBox.SelectedIndex)
            {
                case 0:
                    BookDataGrid.ItemsSource = SourceCore.MyBase.Books.Where(q => q.Name.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    BookDataGrid.ItemsSource = SourceCore.MyBase.Books.Where(q => q.Authors.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    BookDataGrid.ItemsSource = SourceCore.MyBase.Books.Where(q => q.Publishers.PublisherName.Contains(textbox.Text)).ToList();
                    break;
            }

        }
    }
}
