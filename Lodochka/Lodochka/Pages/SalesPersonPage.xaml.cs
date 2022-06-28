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

namespace Lodochka.Pages
{
    /// <summary>
    /// Interaction logic for SalesPersonPage.xaml
    /// </summary>
    public partial class SalesPersonPage : Page
    {
        public SalesPersonPage()
        {
            InitializeComponent();
            DataContext = this;
            SalesPersonGrid.ItemsSource = SourceCore.MyBase.Sales_Person.ToList();
        }

        private void SalesPersonAddCommit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SalesPersonAddRollback_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecordAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecordkCopy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecordDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
