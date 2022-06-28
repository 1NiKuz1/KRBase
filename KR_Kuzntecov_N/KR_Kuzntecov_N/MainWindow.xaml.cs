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

namespace KR_Kuzntecov_N
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

        private void ProgramButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.ProgramPage());
        }

        private void SubroutinesButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.SubroutinesPage());
        }

        private void ExecuterButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.ExecuterPage());
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.DetailPage());
        }
    }
}
