   M S B A M L   `   `   `  ����C  ?Project1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Project1.PagesProject1.Pages  5      "    Project1.Pages.BoatPgeDetail   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Project1.Pages     5        N� Title$  BoatPgeDetail��5	      .��5      � -   5         �Name$ CopyGrid��5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   $	��200��6)   �� 5      $��3 �6   �� 5      $	��300 �6   $	��200��6+   .+�5   
   �� $%��Экземпляры лодок��6   $��0��6+   $��0��68   $��2��6I   $��26(�6]   $��	ExtraBold"�6k   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ BoatDataGrid��5      $��2��6)   $��0��66      AutoGenerateColumns .     CanUserAddRows .  
�������   
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Модель��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 10* 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   ��  �� Path$ Model�6T     5      $	 Тип лодки��6%   $	
 10* 6Z    68   �� $ BoatType�6Z     5      $	 
Мачта��6%   $	
 10* 6R    64   �� $
 Mast�6R     5      $	 Цвет��6%   $	
 10* 6S    63   �� $ Colour�6S     5      $	 Материал��6%   $	
 10* 6U    67   �� $
 Wood�6U     5      $!	 Стартовая цена��6%   $	
 10* 6`    6=   �� $ 	BasePrice�6`     5      $		 VAT��6%   $	
 10* 6O    62   �� $	 VAT�6O   5!      �� 5"   
   -   5         ��Name$ BoatCopyGridSplitter��5"      $��1��65   $��2��6E   $��3��6R   $��Stretch=�6\     5#   
   -   5        $ CopyDataGrid��5#      $��2��6t   $��2��6�    .   .  .  
���������6)   ��  �� ElementName$ BoatDataGrid��$ SelectedItem.Order_� 5$        5%      $	 Номер заказа��6%   $	
 10* 6]    6;   �� $ Order_ID�6]     5&      $	 Дата��6%   $	
 10* 6Q    63   �� $
 Date�6Q     5'      $!	 Адрес доставки��6%   $	
 10* 6f    6=   �� $ DeliveryAddress�6f     5(      $	 
Город��6%   $	
 10* 6R    64   �� $
 City�6R   5*      5,      5-      