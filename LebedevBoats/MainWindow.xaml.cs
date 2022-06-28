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

namespace Project1
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

        private void Boats_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.BoatPage());
        }

        private void Accessory_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.AccessoriesPage());
        }

        private void Client_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.ClientPage());
        }

        private void Salespople_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.SalesPeoplePage());
        }

        private void Orders_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.OrdersPage());
        }

        private void Innovice_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.InnovicePage());
        }

        private void Partners_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.PartnersPage());
        }

        private void Contracts_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.ContractsPage());
        }

        public void BookDlgLoad(bool b)
        {
            if (b == true)
            {
                
            }
        }

        private void Detail_Activation(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.BoatPgeDetail());
        }
    }
}
