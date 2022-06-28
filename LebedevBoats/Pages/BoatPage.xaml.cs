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

namespace Project1.Pages
{
    /// <summary>
    /// Interaction logic for BoatPage.xaml
    /// </summary>
    public partial class BoatPage : Page
    {
        private int DlgMode;
        public Base.Boat_ SelectedBoat;
        public ObservableCollection<Base.Boat_> Boats;
        //private string Model_Buf;
        //private string Type_Buf;
        //private bool Macht_Buf;
        //private string Colour_Buf;
        //private string Material_Buf;
        //private string StartPrice_Buf;
        //protected string VAT_Buf;
        //protected Array _items;
        //protected string lol;
        //protected string lol2;
        public BoatPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
           //BoatGrid.ItemsSource = SourceCore.MyBase.Boat_.ToList();
           //BoatComboColour.ItemsSource = SourceCore.MyBase.Boat_.ToList();
            BookDlgLoad(false,"");
            
        }

        //public void UpdateGrid(Base.Boat_ boats)
        //{
        //    if((boats == null)&&(BoatGrid.ItemsSource != null))
        //    {
        //        boats = (Base.Boat_)BoatGrid.SelectedItem;
        //    }
        //    ObservableCollection<Base.Boat_> Boats =
        //        new ObservableCollection<Base.Boat_>(SourceCore.MyBase.Boat_.ToList());
        //    BoatGrid.ItemsSource = Boats;
        //    BoatComboColour.ItemsSource = Boats;
        //   // BoatComboMaterial.ItemsSource = Boats;
        //    BoatGrid.SelectedItem = boats;
        //}
        public void UpdateGrid(Base.Boat_ Boat)
        {
            if((Boat == null)&&(BoatGrid.ItemsSource != null))
            {
                Boat = (Base.Boat_)BoatGrid.SelectedItem;
            }
            Boats = new ObservableCollection<Base.Boat_>(SourceCore.MyBase.Boat_);
            BoatGrid.ItemsSource = Boats;
            BoatGrid.SelectedItem = Boat;


            BoatComboColour.ItemsSource = SourceCore.MyBase.Boat_.ToList();
            BoatComboMaterial.ItemsSource = SourceCore.MyBase.Boat_.ToList();
            //for (int i = 0; i < BoatComboMaterial.Items.Count; i++)
            //{
            //    for (int y = 0; y<BoatComboMaterial.Items.Count; y++)
            //    {
            //        if (y != i && BoatComboMaterial.Items[i].ToString() == BoatComboMaterial.Items[i].ToString())
            //        {
            //            BoatComboMaterial.Items.RemoveAt(i);
            //            break;
            //        }
            //    }
            //}

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> Columns = new List<string>();
            for(int i =0; i<7; i++)
            {
                Columns.Add(BoatGrid.Columns[i].Header.ToString());
            }
            BoatFilterCombobox.ItemsSource = Columns;
            BoatFilterCombobox.SelectedIndex = 0;
            foreach(DataGridColumn column in BoatGrid.Columns)
            {
                column.CanUserSort = false;
            }
        }


        //public void BookDlgLoad(bool b)
        //{
        //    if (b == true)
        //    {
        //        BoatColumnChange.Width = new GridLength(300);
        //        BoatGrid.IsHitTestVisible = false;

        //    }
        //    else
        //    {
        //        BoatColumnChange.Width = new GridLength(0);
        //        BoatGrid.IsHitTestVisible = true;
        //    }
        //}
        public void BookDlgLoad(bool b, string DlgModeContent)
        {
            if (b)
            {
                BoatColumnChange.Width = new GridLength(400);
                BoatGrid.IsHitTestVisible = false;
                AddLabel.Content = DlgModeContent;
                AddButton.Content = DlgModeContent;
                BoatAdd.IsEnabled = false;
                BoatCopy.IsEnabled = false;
                BoatDelete.IsEnabled = false;
                BoatChange.IsEnabled = false;
            }
            else
            {
                BoatColumnChange.Width = new GridLength(0);
                BoatGrid.IsHitTestVisible = true;
                BoatAdd.IsEnabled = true;
                BoatCopy.IsEnabled = true;
                BoatDelete.IsEnabled = true;
                BoatChange.IsEnabled = true;
                DlgMode = -1;
            }
        }
       
        //private void BookAdd_Click(object sender, RoutedEventArgs e)
        //{
        //    BookDlgLoad(true);
        //    DlgMode = 0;
        //    BoatGrid.SelectedItem = null;
        //    AddLabel.Content = "Добавить";
        //    AddButton.Content = "Добавить лодку в БД";
        //    BoatModel.Text = "";
        //    Type.Text = "";
        //    Macht.Content = false;
        //    BoatComboColour.Text = "";
        //    BoatComboMaterial.Text = "";
        //    Price.Text = "";
        //    VAT.Text = "";
        //}
        private void BookAdd_Click(object sender, RoutedEventArgs e)
        {
            BookDlgLoad(true, "Добавить лодку");
            DataContext = null;
            DlgMode = 0;
        }

        private void AddFinal_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(BoatModel.Text)) errors.AppendLine("Укажите название модели!");
            if (string.IsNullOrEmpty(Type.Text)) errors.AppendLine("Укажите название типа!");
            if (string.IsNullOrEmpty(Price.Text)) errors.AppendLine("Укажите цену модели!");
            if (string.IsNullOrEmpty(VAT.Text)) errors.AppendLine("Укажите VAT модели!");
            if ((Base.Boat_)BoatComboColour.SelectedItem == null) errors.AppendLine("Укажите цвет!");
            if ((Base.Boat_)BoatComboMaterial.SelectedItem == null) errors.AppendLine("Укажите материал!");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                var NewBoat = new Base.Boat_();
                NewBoat.Model = BoatModel.Text;
                NewBoat.BoatType = Type.Text;
                if ((bool)Macht.IsChecked)
                {
                    NewBoat.Mast = true;
                }
                else
                {
                    NewBoat.Mast = false;
                }
                NewBoat.Colour = BoatComboColour.Text;
                NewBoat.Wood = BoatComboMaterial.Text;
                NewBoat.BasePrice = Convert.ToDecimal(Price.Text.Replace('.', ','));
                NewBoat.VAT= Convert.ToDecimal(VAT.Text.Replace('.', ','));
                SourceCore.MyBase.Boat_.Add(NewBoat);
                SelectedBoat = NewBoat;
            }
            else
            {
                var EditBoat = new Base.Boat_();
                EditBoat.Model = BoatModel.Text;
                EditBoat.BoatType = Type.Text;
                if ((bool)Macht.IsChecked)
                {
                    EditBoat.Mast = true;
                }
                else
                {
                    EditBoat.Mast = false;
                }
                EditBoat.Colour = BoatComboColour.Text;
                EditBoat.Wood = BoatComboMaterial.Text;
                EditBoat.BasePrice = Convert.ToDecimal(Price.Text.Replace('.', ','));
                EditBoat.VAT = Convert.ToDecimal(VAT.Text.Replace('.', ','));
            }

            try
            {
                SourceCore.MyBase.SaveChanges();
                BookDlgLoad(false, "");
                UpdateGrid(SelectedBoat);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //private void AddFinal_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder errors = new StringBuilder();
        //    if (string.IsNullOrEmpty(BoatModel.Text)) errors.AppendLine("Укажите название модели!");
        //    if (string.IsNullOrEmpty(Type.Text)) errors.AppendLine("Укажите название типа!");
        //    if (string.IsNullOrEmpty(Price.Text)) errors.AppendLine("Укажите цену модели!");
        //    if (string.IsNullOrEmpty(VAT.Text)) errors.AppendLine("Укажите VAT модели!");
        //    if ((Base.Boat_)BoatComboColour.SelectedItem == null) errors.AppendLine("Укажите цвет!");
        //    if ((Base.Boat_)BoatComboMaterial.SelectedItem == null) errors.AppendLine("Укажите материал!");

        //    var NewBoat = new Base.Boat_();

        //        if (errors.Length > 0)
        //        {
        //            MessageBox.Show(errors.ToString());
        //            return;
        //        }
        //        else
        //        {
        //            NewBoat.Model = BoatModel.Text;
        //            NewBoat.BoatType = Type.Text;
        //            if ((bool)Macht.IsChecked)
        //            {
        //                NewBoat.Mast = true;
        //            }
        //            else
        //            {
        //                NewBoat.Mast = false;
        //            }
        //            NewBoat.Colour = BoatComboColour.Text;
        //            NewBoat.Wood = BoatComboMaterial.Text;
        //            NewBoat.BasePrice = Convert.ToDecimal(Price.Text.Replace('.', ','));
        //            NewBoat.VAT = Convert.ToDecimal(VAT.Text.Replace('.', ','));

        //        }
        //    if (DlgMode == 0)
        //    {
        //        SourceCore.MyBase.Boat_.Add(NewBoat);
        //    }

        //    try
        //    {
        //        SourceCore.MyBase.SaveChanges();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }

        //    UpdateGrid(NewBoat);
        //    BookDlgLoad(false,"");
        //}

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            BookDlgLoad(false,"");
            UpdateGrid(SelectedBoat);
        }

        //private void Delete_Click(object sender, RoutedEventArgs e)
        //{
        //   // if(MessageBox.Show("Удалить запись?","Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
        //   // {
        //   //     SourceCore.MyBase.Boat_.Remove((Base.Boat_)BoatGrid.SelectedItem);
        //   //     SourceCore.MyBase.SaveChanges();
        //   // }

        //    if(MessageBox.Show("Удалить запсь?","Внимание",MessageBoxButton.OKCancel, MessageBoxImage.Warning)==MessageBoxResult.OK)
        //    {
        //        try
        //        {
        //            Base.Boat_ DeletingBoat = (Base.Boat_)BoatGrid.SelectedItem;
        //            if(BoatGrid.SelectedIndex < BoatGrid.Items.Count - 1)
        //            {
        //                BoatGrid.SelectedIndex++;
        //            }
        //            else
        //            {
        //                if (BoatGrid.SelectedIndex > 0)
        //                {
        //                    BoatGrid.SelectedIndex--;
        //                }
        //            }
        //            Base.Boat_ SelectingBoat = (Base.Boat_)BoatGrid.SelectedItem;
        //            SourceCore.MyBase.Boat_.Remove(DeletingBoat);
        //            SourceCore.MyBase.SaveChanges();
        //            UpdateGrid(SelectingBoat);
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных",
        //                "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
        //        }
        //    }
        //}

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (BoatFilterCombobox.SelectedIndex)
            {
                case 0:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat_.Where(q =>
                    q.Model.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat_.Where(q =>
                    q.BoatType.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    string mast ="";
                    if (textbox.Text =="+")
                    {
                        mast = "True";
                    }
                    else
                    {
                        if(textbox.Text == "-")
                        {
                            mast = "False";
                        }
                    }
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat_.Where(q =>
                    q.Mast.ToString().Contains(mast)).ToList();
                    break;
                case 3:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat_.Where(q =>
                  q.Colour.Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat_.Where(q =>
                  q.Wood.Contains(textbox.Text)).ToList();
                    break;
                case 5:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat_.Where(q =>
                  q.BasePrice.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 6:
                    BoatGrid.ItemsSource = SourceCore.MyBase.Boat_.Where(q =>
                  q.VAT.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запсь?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    Base.Boat_ DeletingBoat = (Base.Boat_)BoatGrid.SelectedItem;
                if (BoatGrid.SelectedIndex < BoatGrid.Items.Count - 1)
                {
                    BoatGrid.SelectedIndex++;
                }
                else
                {
                    if (BoatGrid.SelectedIndex > 0)
                    {
                        BoatGrid.SelectedIndex--;
                    }
                }
                Base.Boat_ SelectingBoat = (Base.Boat_)BoatGrid.SelectedItem;
                SourceCore.MyBase.Boat_.Remove(DeletingBoat);
                SourceCore.MyBase.SaveChanges();
                UpdateGrid(SelectingBoat);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }

                //SourceCore.MyBase.Boat_.Remove((Base.Boat_)BoatGrid.SelectedItem);
                //SourceCore.MyBase.Entry((Base.Boat_)BoatGrid.SelectedItem).State = System.Data.Entity.EntityState.Deleted;
                //SourceCore.MyBase.SaveChanges();
                //UpdateGrid(null);
            }
        }

        //private void BoatCopy_Click(object sender, RoutedEventArgs e)
        //{
        //    if (BoatGrid.SelectedItem != null)
        //    {
        //        BookDlgLoad(true);
        //        DlgMode = 0;
        //        AddLabel.Content = "Копировать";
        //        AddButton.Content = "Копировать лодку в БД";
        //        Model_Buf = BoatModel.Text;
        //        Type_Buf = Type.Text;
        //        if ((bool)Macht.IsChecked)
        //        {
        //            Macht_Buf = true;
        //        }
        //        else
        //        {
        //            Macht_Buf = false;
        //        }
        //        Colour_Buf = BoatComboColour.Text;
        //        Material_Buf = BoatComboMaterial.Text;
        //        StartPrice_Buf = Price.Text;
        //        VAT_Buf = VAT.Text;
        //        BoatGrid.SelectedItem = null;
        //        BoatModel.Text = Model_Buf;
        //        Type.Text = Type_Buf;
        //        if (Macht_Buf==true)
        //        {
        //            Macht.IsChecked = true;
        //        }
        //        else
        //        {
        //            Macht.IsChecked = false;
        //        }
        //        BoatComboColour.Text = Colour_Buf;
        //        BoatComboMaterial.Text = Material_Buf;
        //        Price.Text = StartPrice_Buf;
        //        VAT.Text = VAT_Buf;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
        //    }
        //}
        private void BoatCopy_Click(object sender, RoutedEventArgs e)
        {
            if(BoatGrid.SelectedItem != null)
            {
                BookDlgLoad(true, "Копировать лодку");
                SelectedBoat = (Base.Boat_)BoatGrid.SelectedItem;
                BoatModel.Text = SelectedBoat.Model;
                Type.Text = SelectedBoat.BoatType;
                if (SelectedBoat.Mast)
                {
                    Macht.IsChecked = true;
                }
                else
                {
                    Macht.IsChecked = false;
                }
                BoatComboColour.Text = SelectedBoat.Colour;
                BoatComboMaterial.Text = SelectedBoat.Wood;
                //            NewBoat.BasePrice = Convert.ToDecimal(Price.Text.Replace('.', ','));
                Price.Text = Convert.ToString(SelectedBoat.BasePrice);
                VAT.Text = Convert.ToString(SelectedBoat.VAT);
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }


        //private void BoatChange_Click(object sender, RoutedEventArgs e)
        //{
        //    if (BoatGrid.SelectedItem != null)
        //    {
        //        BookDlgLoad(true,"Изменить лодку");
        //        AddLabel.Content = "Изменить";
        //        AddButton.Content = "Изменить лодку";
        //        DlgMode = 1;
        //    }
        //    else
        //    {

        //        MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);

        //    }
        //}

        private void BoatChange_Click(object sender, RoutedEventArgs e)
        {
            if(BoatGrid.SelectedItem != null)
            {
                BookDlgLoad(true, "Изменить лодку");
                SelectedBoat = (Base.Boat_)BoatGrid.SelectedItem;
                BoatModel.Text = SelectedBoat.Model;
                Type.Text = SelectedBoat.BoatType;
                if (SelectedBoat.Mast)
                {
                    Macht.IsChecked = true;
                }
                else
                {
                    Macht.IsChecked = false;
                }
                BoatComboColour.Text = SelectedBoat.Colour;
                BoatComboMaterial.Text = SelectedBoat.Colour;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

    }
}
