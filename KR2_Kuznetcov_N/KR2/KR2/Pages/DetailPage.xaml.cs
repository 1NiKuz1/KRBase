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

namespace KR2.Pages
{
    /// <summary>
    /// Interaction logic for DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Page
    {
        public ObservableCollection<Base.Debtors> Debtors;
        public DetailPage()
        {
            InitializeComponent();
            Debtors = new ObservableCollection<Base.Debtors>(SourceCore.MyBase.Debtors);
            DeptorsGrid.ItemsSource = Debtors;
        }
    }
}
