   M S B A M L   `   `   `  ����W  SDevExpress.ProductsDemo.Wpf, Version=18.1.4.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ProductsDemo.ModulesProductsDemo.Modules  5      a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35f bDevExpress.Xpf.Grid.v18.1.Core, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ac _DevExpress.Xpf.Ribbon.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aj fDevExpress.Xpf.LayoutControl.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]	 YDevExpress.Mvvm.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al
 hDevExpress.Xpf.Core.v18.1.Extensions, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0894    .ProductsDemo.Modules.ViewToVisibilityConverter   ViewToVisibilityConverter.   (ProductsDemo.Modules.EmptyPhotoConverter  EmptyPhotoConverter0   *ProductsDemo.Modules.ViewToMarginConverter  ViewToMarginConverter/   )ProductsDemo.Modules.PhoneFormatConverter  phoneFormatConverter6   0ProductsDemo.Modules.AddressDetailPanelConverter  AddressDetailPanelConverter.  (DevExpress.Mvvm.UI.FormatStringConverter  birthDateConverter    FormatString  ContactDetailsTemplate J� Opacity �� Path �� ElementName �� 	Converter3   -ProductsDemo.Modules.ObjectToOpacityConverter   Opacity   Invert � RowDefinitions&   DevExpress.Xpf.Editors.ImageEdit%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly	   
ShowBorder
   	EditValue �� Orientation  cardTemplate � ColumnDefinitions   Stretch   ShowMenu   Source  ContactsGridViewStyle �� 
TargetType&	   DevExpress.Xpf.Grid.GridViewBase&
   DevExpress.Xpf.Grid.DataViewBase �� Property �� Value#  DevExpress.Xpf.Grid.TableView 	 	TableView   	AutoWidth 	  ShowGroupedColumns   ShowIndicator   AllowPerPixelScrolling2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   Triggers'  !DevExpress.Mvvm.UI.EventToCommand�  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName+  %DevExpress.Mvvm.UI.EventToCommandBase   Command"  DevExpress.Xpf.Grid.CardView 
 CardView   CardTemplate   
CardLayout   	FixedSize   SeparatorTemplate   CardHeaderBinding �� RelativeSource  �� IsAsync! �� FallbackValue/  )DevExpress.Xpf.Core.EnumToObjectConverter  ViewTypeToViewConverter"   ValuesO Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089�  �System.Collections.Generic.Dictionary`2[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]],  &DevExpress.Xpf.Core.EnumObjectProvider  Key#   Value�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     ,local"clr-namespace:ProductsDemo.Modules  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      >dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      >dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  5      Fdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 	  5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core  
 5	      :x,http://schemas.microsoft.com/winfx/2006/xaml     5
      %�  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 �     7  7  7  7  &	 X    7  7  &	 p    &		 0    7  &	
 �    7  7  &	 �    7 	 7 
    5      6&     5      6      5      6"     5      6!     5      6(     5      $  {0:dd MMM yyyy}��5   5   5      �� 5      .R�5   
   � .+�5      �� $��No contact selected��6   $��Segoe UI)�63   $��16(�6I   $��Center=�6W   $��Center=�6t    5      �� 5      $ View.FocusedRowData.Row�6   $
 grid��6=    5        5      $	 0.5Q�6=    . 6K   5      5      � 5        5!      �� 5"      $ View.FocusedRowData.Row�6   $
 grid��6=    5#        5$      $ 1Q�6=    .  6I   5&       5(      �� 5)      $
��Auto �6$   �� 5*      $
��Auto �6$   �� 5+      $
��Auto �6$   �� 5,      $
��Auto �6$   �� 5-      $
��Auto �6$   �� 5.      $
��Auto �6$   .+�50        $��0,0,0,23q�6�    . 	 .  
 6    �� *View.FocusedRowData.Row.Photo+$
 grid��8   6�   �� 51      $��1q�6   
���   �$
 0.25Q�6A   $��0,0,0,23q�6P   �� 52      $��1��6   $��0,0,0,23q�6r   $��20(�6�   $��SemiBold"�6�   ��6)   �� *'%View.FocusedRowData.Row.Name.FullName+$
 grid��6�   �� 53      $��2��6   $ 
Horizontal=�6*   .+�54      �� $
 0.65Q�6    $��	BIRTHDATE��6/   $��0,0,4,2q�6@   �� 55      $
��Wrap=�6�   $��0,0,0,2q�6�   ��6    �� *#!View.FocusedRowData.Row.BirthDate+$
 grid��8  6�   56      �� 57      $��3��6   $ 
Horizontal=�6*   .+�58      �� $
 0.65Q�6    $��ADDRESS��6/   $��0,0,4,2q�6>   �� 59      $
��Wrap=�6�   $��0,0,0,2q�6�   ��6    �� *!View.FocusedRowData.Row.Address+$
 grid��8  6�   5:      �� 5;      $��4��6   $ 
Horizontal=�6*   .+�5<      �� $
 0.65Q�6    $��EMAIL��6/   $��0,0,4,2q�6<   �� 5=      $
��Wrap=�6a   $��0,0,0,2q�6u   ��6    �� *View.FocusedRowData.Row.Email+$
 grid��6u   5>      �� 5?      $��5��6   $ 
Horizontal=�6*   .+�5@      �� $
 0.65Q�6    $��PHONE��6/   $��0,0,4,0q�6<   �� 5A      $
��Wrap=�6�   ��6    �� *View.FocusedRowData.Row.Phone+$
 grid��8  6�   5B      5C      5D      5E      �� 5F      .R�5G   
   �  5H      �� 5I      $	��100 �6#   �� 5J      $	��195 �6#   .+�5L      � $	��Top=�6   .+�5M        $ Uniform=�6     .  	 .   62   �� $ DataContext.Photo�8   6�   �� 5N      $��1q�6   
���   �$
 0.25Q�6A   5O      � 5P      $��1��6   $��12,0,0,0q�6#    5Q      �� 5R      $
��Auto �6$   �� 5S      $
��Auto �6$   �� 5T      $
��Auto �6$   .+�5V      �� $ Vertical=�6   $��0,0,0,12q�64   .+�5W      �� $��ADDRESS��6    $
 0.65Q�6/   �� 5X      $
��Wrap=�6J   ��6    �� $ DataContext.Address�6J   5Y      �� 5Z      $��1��6   $ Vertical=�6*   $��0,0,0,12q�6A   .+�5[      �� $��EMAIL��6    $
 0.65Q�6-   �� 5\      $
��Wrap=�6H   ��6    �� $ DataContext.Email�6H   5]      �� 5^      $��2��6   $ Vertical=�6*   .+�5_      �� $��PHONE��6    $
 0.65Q�6-   �� 5`      $
��Wrap=�6z   ��6    �� $ DataContext.Phone�8  6z   5a      5b      5c      5d      �� 5e      " 	 6*   .�5f   
   ��  �@
 AllowEditing .  6)   �� 5g   
    �@
 ShowSearchPanelMode$ Always=�60   �� 5h   
    �@	 ShowGroupPanel .  6+   5i        5j      8��   .  .  .   .  5k   
     5l      $ RowDoubleClick��6$    6?   �� *ShowEditContactWindowCommand+5n        5o      8��  8  $
 Rows=�6  $	 205Q�6,   6�   Z�  6�   �� *Data.Name.FullName+ �� *Self+  . ! Z�  5p   
     5q      $ MouseDoubleClick��6$    6A   �� *ShowEditContactWindowCommand+5s        5t      " 5u   
      5v      	TableView 8#   66     5w      CardView 8#  65   5x      5y      5z      