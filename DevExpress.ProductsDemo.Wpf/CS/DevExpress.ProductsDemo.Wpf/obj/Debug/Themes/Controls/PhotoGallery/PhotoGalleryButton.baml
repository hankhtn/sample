   M S B A M L   `   `   `  ����W  SDevExpress.ProductsDemo.Wpf, Version=18.1.4.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ProductsDemoProductsDemo  5   	   a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089%    ProductsDemo.PhotoGalleryButton  �� 
TargetType �� Property �� Value �� 
TargetType �Name'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName	 �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame
 O� KeyTime O� Value R� KeyTime R� Value  System.Windows.Visibility   Transitions%  System.Windows.VisualTransition   GeneratedDuration	   To   
Storyboard g� Duration	 W� To   From J� Opacity    Content    ContentTemplate�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   $localclr-namespace:ProductsDemo  5   	   %�  '         �� 5      "    6   .�5   
   ��  � �� 5      �� 5      "   6"   .R�5	      � $ elementsContainer��6    5
        5      VisibleStates��. 5   "     Visible��. 5   &   �� .��5   *   U� $ elementsContainer��6H   $	 (UIElement.Opacity)�6r   .\�5   .     $
 
00:00:00.5��6C   $ 1Q�6X   5   +   T� 5   *   $ elementsContainer��6H   $	 (UIElement.Visibility)�6r   ..�5   .   i� $ 00:00:00��6E    5   2    5   6   Visible=�6J   5   /   5   +   5   '   5   #     5   "   	Invisible��. 5   &   �� .��5   *   U� $ elementsContainer��6H   $	 (UIElement.Opacity)�6r   .\�5   .     $
 
00:00:00.5��6C   $ 0Q�6X   5   +   T� 5   *   $ elementsContainer��6H   $	 (UIElement.Visibility)�6r   ..�5    .   i� $ 
00:00:00.5��6E    5!   2    5"   6   	Collapsed=�6L   5$   /   5%   +   5&   '   5'   #   5(        5)      CommonStates�� 5*   "     5+   &   $ 0:0:0.15D�67   $ 	MouseOver��6T   . 5,   *   �� .��5-   .   W� $ 0:0:0.15D�6>   $ 1X�6R   $	 (UIElement.Opacity)�6Y   $ contentPresenter��6�   5.   +   5/   '     50   &   $ 	MouseOver��67   $ 0D�6H   $ Normal��6^   . 51   *   �� .��52   .   W� $ 0:0:0.3D�6>   $
 0.75X�6Q   $	 (UIElement.Opacity)�6[   $ contentPresenter��6�   53   +   54   '   . 56   "     
Normal��6.     57   "   	MouseOver��. 58   &   �� .��59   *   U� $	 (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5:   .     $
 0��6C   $ 1Q�6O   5;   +   5<   '   5=   #     5>   "   Pressed��. 5?   &   �� .��5@   *   U� $	 (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5A   .     $
 0��6C   $
 0.35Q�6O   5B   +   5C   '   5D   #     5E   "   Disabled��. 5F   &   �� .��5G   *   U� $	 (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5H   .     $
 0��6C   $
 0.15Q�6O   5I   +   5J   '   5K   #   5L      .+�5N      � $ contentPresenter��6   $
 0.75Q�69   .+�5O      �� #��z 6/   #��z 6S   $��Center=�6�   $��Center=�6�   5P      5Q      5R      5T      5U      5V      