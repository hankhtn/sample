   M S B A M L   `   `   `  ����c  _DevExpress.DevAV.v18.1.Data, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aXCclr-namespace:DevExpress.DevAV;assembly=DevExpress.DevAV.v18.1.DataDevExpress.DevAV  5       DevExpress.ProductsDemo.Wpf.   (ProductsDemo.Controls.EditContactContent   5      a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      l hDevExpress.Xpf.Core.v18.1.Extensions, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      c _DevExpress.Xpf.Ribbon.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  5	      j	 fDevExpress.Xpf.LayoutControl.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 	 5
      ]
 YDevExpress.Mvvm.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 
  5      QdevavCclr-namespace:DevExpress.DevAV;assembly=DevExpress.DevAV.v18.1.Data 
  5      .��5      � $��10q�6     � RowDefinitions  5   
   �� 5      $��2* �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   0 	 *DevExpress.Xpf.LayoutControl.LayoutControl  . 	 (DevExpress.Xpf.LayoutControl.LayoutGroup   Orientation$ 
Horizontal=�6   !   StretchContentHorizontally . 4 	 .DevExpress.Xpf.LayoutControl.LayoutControlBase   Padding$ 0q�6X   $��0q�6d   .+�5           Header$ LayoutGroup��6      View$ Group=�64   .+�5      &   DevExpress.Xpf.Editors.ImageEdit  $	��300��6       AllowHorizontalSizing .    Source 6\   �� *EditableContact.Photo+ �� Mode$ TwoWay=�  5      $ Group=�6#   $ Vertical=�60   .+�5      - 	 'DevExpress.Xpf.LayoutControl.LayoutItem  	   Label$	 First Name:��6&   
   Content.
 5      %  DevExpress.Xpf.Editors.TextEdit  %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 6'   �� * EditableContact.Name.FirstName+ �� UpdateSourceTrigger$ PropertyChanged=�5        5      $	 Middle Name:��6&   .
 5         6'   �� *!EditableContact.Name.MiddleName+$ PropertyChanged=�5        5      $	 
Last Name:��6&   .
 5          6'   �� *EditableContact.Name.LastName+$ PropertyChanged=�5!        5"      $ Address��6'   $ Vertical=�68   $ GroupBox=�6O   $��0,10,0,0q�6_   .+�5#        $	 Address Line:��6*   .
 5$         6+   �� *%#EditableContact.Address.AddressLine+$ PropertyChanged=�5%        5&      $ 
Horizontal=�6+   .+�5'        $	 City:��6.   .
 5(   "   )  #DevExpress.Xpf.Editors.ComboBoxEdit  +	  %DevExpress.Xpf.Editors.LookUpEditBase 	  ItemsSource 63   �� *Cities+ 6R   �� *EditableContact.Address.City+$ PropertyChanged=�5)        5*      5+        5,      $ 
Horizontal=�6+   .+�5-        $	 State:��6.   .
 5.   "      63   �� *States+ 6R   �� *EditableContact.Address.State+$ PropertyChanged=�5/        50      $	 	Zip Code:��6.   .
 51   "      6/   �� *EditableContact.Address.Zip+$ PropertyChanged=�52      53      54        55      $
 Info��6'   $ GroupBox=�65   $ Vertical=�6E   $��Stretch=�6\   .+�56        $	 Email:��6*   .
 57         6+   �� *EditableContact.Email+$ PropertyChanged=�58        59      $ 
Horizontal=�6+   .+�5:        $	 Phone:��6.   .
 5;   "        MaskType$ RegEx=�6�      Mask$# (\(\d\d\d\)) \d{1,3}-\d\d\d\d��6�      MaskUseAsDisplayFormat .  6/   �� *EditableContact.Phone+$ PropertyChanged=�6�   5<        5=      5>        5?      $ 
Horizontal=�6+   .+�5@        $	 Birth Date:��6.   .
 5A   "   %
  DevExpress.Xpf.Editors.DateEdit
   6/   �� *EditableContact.BirthDate+$ PropertyChanged=�5B        5C      5D        5E      $ 
Horizontal=�6+   .+�5F        $	 Prefix:��6.   .
 5G   "     '  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable$ FalseX�5H   3   & 	  ApplyItemTemplateToSelectedItem .  5G   3   �� *EditableContact.Prefix+$ PropertyChanged=� 5I   &   ,  &DevExpress.Xpf.Editors.EnumItemsSource  5J   *      EnumType#   DevExpress.DevAV.PersonPrefix"  6>   5L   #   5M        5N      5O      5P      5Q      5R      5S      � 5T   
   $��1��6   $��0,0,0,4q�6     5U      �� 5V      $
��Auto �6    �� 5W      $��* �6    .+�5Y      �� $��Notes:��6   $��0,0,0,4q�6&     5Z      $��1��6   )  #DevExpress.Xpf.Editors.TextEditBase   AcceptsReturn . $	��Top=�6�   "   VerticalScrollBarVisibility$ Visible=�6�    6(   �� *EditableContact.Notes+$ PropertyChanged=�6�   5[      �� 5\   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��0,4,4,8q�6;   $��Right=�6L   .+�5]      �� $��OK��6   $	��100��6I   ��6"   �� *SaveContactCommand+6I   �� 5^      $��Cancel��6   $��6,0,0,0q�6T   $	��100��6e   ��6&   �� *CloseContactWindowCommand+6e   5_      5`      5a      