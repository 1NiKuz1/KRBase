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

namespace Lodochka.Pages
{
    /// <summary>
    /// Interaction logic for BoatPageDetail.xaml
    /// </summary>
    public partial class BoatPageDetail : Page
    {
        public ObservableCollection<Base.Boat> Boats;
        public BoatPageDetail()
        {
            InitializeComponent();
            Boats = new ObservableCollection<Base.Boat>(SourceCore.MyBase.Boat);
            BoatDataGrid.ItemsSource = Boats;
        }
    }
}
