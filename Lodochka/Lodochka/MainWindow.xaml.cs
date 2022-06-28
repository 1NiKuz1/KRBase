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

namespace Lodochka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AccessoryButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.AccessoryPage());
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.OrderPage());
        }

        private void ContractButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.ContractPage());
        }

        private void CustomersButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.CustomersPage());
        }

        private void InvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.InvoicePage());
        }

        private void PartnerButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.PartnerPage());
        }

        private void SalesPersonButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.SalesPersonPage());
        }

        private void BoatButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.BoatPage());
        }

        private void BoatDetail_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.BoatPageDetail());
        }
    }
}
