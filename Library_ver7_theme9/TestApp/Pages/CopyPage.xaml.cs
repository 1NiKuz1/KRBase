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

namespace TestApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для CopyPage.xaml
    /// </summary>
    public partial class CopyPage : Page
    {
        public ObservableCollection<Base.Books> Books;
        public CopyPage()
        {
            InitializeComponent();
            Books = new ObservableCollection<Base.Books>(SourceCore.MyBase.Books);
            BookDataGrid.ItemsSource = Books;
        }
    }
}
