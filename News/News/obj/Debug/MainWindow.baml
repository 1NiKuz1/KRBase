   M S B A M L   `   `   `  ����?  ;News, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NewsNews  5   	       News.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:News     5   	     � Title$  
MainWindow��5   	   $	��600��6   $
��1000��6)   $	��270��66   $	��610��6F    � WindowStartupLocation$ CenterScreen=�6U   .��5	      � ���#fafafa � ColumnDefinitions 5
   
   �� 5      $	��140 �6   $	��140��6+   $	��250��6:   �� 5      $
��Auto �6   �� 5      $��* �6   $	��300��6)    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��35 �6   .+�5   
   �� $��0��6   $��0��6%   $��2��62   ���#eee.+�5      �� -   5         ��Name$ 	RoleLabel��5      $ ��Администратор��6'   $
��Bold"�5      $��10(�6   $��Center=�6+   �� 5      -   5         ��Name$ 
NewsButton��5      $
��News��6)   $��25��68   $��5,5,5,0q�6D   6U   �� 5      -   5        $ KategoriesButton��5      $��
Kategories��6/   $��25��6D   $��5,5,5,0q�6P   �� 5      -   5        $ CommentsButton��5      $��Comments��6-   $��25��6@   $��5,5,5,0q�6L   5      �� 5   
   $��1��6   $��0��6'   $��2��64   $��3��5      $��Stretch=�6   ]� 5   
   $��2��6   $��1��6$   ���#eee.+�5       ��  �� Orientation$ 
Horizontal=�6   $
��Left=�62   .+�5!      �� -   5        $ PreviosPageButton��5!      $��
Назад��64   $��75��6D   $��5,5,0,5q�6O   �� 5"      -   5        $ NextPageButton��5"      $��Вперед��61   $��75��6B   $��5,5,0,5q�6M   5#      �� 5%      $ 
Horizontal=�6   $��Right=�62   .+�5&      �� -   5        $ ClosePageButton��5&      $%��Закрыть страницу��62   $	��120��6M   $��5q�6Y   5'      5(       � 5*   
   -   5          �Name$ 	RootFrame��5*      $��2��6#   $��0��63     � NavigationUIVisibility$ Hidden=�6@   5,      5-      