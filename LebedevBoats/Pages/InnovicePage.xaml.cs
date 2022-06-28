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

namespace Project1.Pages
{
    /// <summary>
    /// Interaction logic for InnovicePage.xaml
    /// </summary>
    public partial class InnovicePage : Page
    {
        public InnovicePage()
        {
            InitializeComponent();
            DataContext = this;
            InnoviceGrid.ItemsSource = SourceCore.MyBase.Invoice_.ToList();
        }
    }
}
