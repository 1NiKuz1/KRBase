using System;
using System.Collections.Generic;
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
{
    /// <summary>
    /// Логика взаимодействия для BookDlgPage.xaml
    /// </summary>
    public partial class BookDlgPage : Page
    {
        private Base.Books _currentbook = new Base.Books();
        private Base.LibraryExEntities Database;
        public Base.Books Book { get; set; }
        private Page Page;
        public BookDlgPage(Page Page, Base.LibraryExEntities Database, Base.Books Book)
        {
            InitializeComponent();
            
            this.Page = Page;
            this.Book = Book;
            this.DataContext = this;

            BookComboBoxPublishers.ItemsSource = SourceCore.MyBase.Publishers.ToList();
        }

        private void BookAddRollback_Click(object sender, RoutedEventArgs e)
        {
            (Page as Pages.BookPageWithDlg).BookDlgLoad(false);   
        }

        private void BookAddCommit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
