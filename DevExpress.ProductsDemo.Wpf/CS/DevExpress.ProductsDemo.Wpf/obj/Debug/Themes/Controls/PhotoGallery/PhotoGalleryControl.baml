   M S B A M L   `   `   `  ����W  SDevExpress.ProductsDemo.Wpf, Version=18.1.4.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ProductsDemoProductsDemo  5   	   a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   CloseButtonStyle  �� 
TargetType �� Property �� Value �� 
TargetType'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   Transitions%  System.Windows.VisualTransition   GeneratedDuration	   To   
Storyboard	 g� Duration	
 W� To �� TargetProperty �� 
TargetName   From   States   System.Windows.VisualState   
Storyboard9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value ��Name J� Opacity&    ProductsDemo.PhotoGalleryControl �Name R� KeyTime R� Value  System.Windows.Visibility � RowDefinitions ��Name � Color � Offset   Title J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   	Direction   ShadowDepth   
BlurRadius  ��Name!   ActualItems"   ItemTemplate# � DataContext$ �� RelativeSource$   ProductsDemo.PhotoGalleryPanel�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   $localclr-namespace:ProductsDemo  5   	   %�  &	        '  /    �� 5      "  ��6%   .�5   
   ��  � �� 5      �� 5      " ��6"   .R�5	      � ���	#00000000 5
        5      CommonStates�� 5   "     5   &   $ 0:0:0.15D�67   $ 	MouseOver��6T   . 5   *   �� .��5   .   W� $	 0:0:0.15D�6>   $
 1X�6R   $ (UIElement.Opacity)�6Y   $ contentPresenter��6�   5   +   5   '     5   &   $ 	MouseOver��67   $ 0D�6H   $ Normal��6^   . 5   *   �� .��5   .   W� $	 0:0:0.3D�6>   $

 0.75X�6Q   $ (UIElement.Opacity)�6[   $ contentPresenter��6�   5   +   5   '   . 5   "     
Normal��6.     5   "   	MouseOver��. 5   &   �� .��5   *   U� $ (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5   .     $ 0��6C   $ 1Q�6O   5   +   5   '   5   #     5    "   Pressed��. 5!   &   �� .��5"   *   U� $ (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5#   .     $ 0��6C   $
 0.35Q�6O   5$   +   5%   '   5&   #     5'   "   Disabled��. 5(   &   �� .��5)   *   U� $ (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5*   .     $ 0��6C   $
 0.15Q�6O   5+   +   5,   '   5-   #   5.      .+�50      �� $ contentPresenter��6+   #��z��6E   #��z��6�   $
 0.75Q�6�   51      52      54      55      �� 56      "   6   .�57   
   ��  � �� 58      �� 59      "  6"   .R�5:      � $ 
LayoutRoot��6   $ 0Q�6/   $y�	Collapsed=�6;    5;        5<      VisibleStates��. 5=   "     Visible��. 5>   &   �� .��5?   *   U� $ 
LayoutRoot��6H   $ (UIElement.Opacity)�5@      .\�5A   .     $ 00:00:01��6C   $ 1Q�6V   5B   +   T� 5C   *   $ 
LayoutRoot��6H   $ (UIElement.Visibility)�5D      ..�5E   .   i� $ 00:00:00��6E    5F   2    5G   6   Visible=�6J   5I   /   5J   +   5K   '   5L   #     5M   "   	Invisible��. 5N   &   �� .��5O   *   U� $ 
LayoutRoot��6H   $ (UIElement.Opacity)�5P      .\�5Q   .   �� $ 00:00:01��6C   $ 0Q�6V   5R   +   T� 5S   *   $ 
LayoutRoot��6H   $ (UIElement.Visibility)�5T      ..�5U   .   i� $ 00:00:01��6E    5V   2    5W   6   	Collapsed=�6L   5Y   /   5Z   +   5[   '   5\   #   5]       5_      �� 5`      $
��Auto �6,   �� 5a      $��* �6,   �� 5b      $
��Auto �6,   .+�5d      �� $
 Mask��6!   $��3��6/   ��5e      
� 5f   "   .��5g   &   � $ 	#BF000000��63   $ 1Q�6E   � 5h   &   $ 	#00000000��63   5i   #   5k      �� 5l      $
��Wrap=�6$   #��z 68   $��34.667(�6W   
�������$��Center=�6|   $��0,10,0,0q�6�    5m        5n      $	 -90Q�6$   $ 0Q�64   $ 10Q�6D   5p      �� 5q      $  PART_Elements��6~   $��Center=�6'   $��Center=�6D   $��1��6_   $��0,0,0,50q�6l   #��z! 5r   )   #��z" 6U   # 5q   �   �� $ �� *TemplatedParent+��5s      �� 5t   "   .R�5u   &     5v   #   5x      �� 5y      $
��Wrap=�6$   $'��!Click a sight to see it in detail��68   $��21.333(�6a   
�������$��Center=�6�   $��2��6�   $��0,0,0,20q�6�    5z        5{      $	 -90Q�6$   $ 0Q�64   $ 10Q�6D   5}      5~      5      5�      5�      5�      