   M S B A M L   `   `   `  ����W  SDevExpress.ProductsDemo.Wpf, Version=18.1.4.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ProductsDemoProductsDemo  5   	   a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089*    $ProductsDemo.PhotoGalleryItemControl  �� 
TargetType �� Property �� Value �� 
TargetType �Name J� RenderTransformOrigin'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   Transitions%  System.Windows.VisualTransition   GeneratedDuration		   To
   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName   From9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value   States   System.Windows.VisualState   
Storyboard J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   	Direction   Opacity   
BlurRadius   ShadowDepth J� Opacity ��Name �� TextAlignment�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   $localclr-namespace:ProductsDemo  5   	   %R
  '         �� 5      "    6   .�5   
   ��  � �� 5      �� 5      "   6"   .R�5	      � $ grid1��6   $
��Auto��6*   $
��Auto��68   $ 0.5,0.5&�6E   z�5
      V� 5      .~�5   "   �� �� 5   "   �� 5   "   T� 5   "   5       5        5      CommonStates�� 5   "     5   &   $ 0D�67   $	 	MouseOver��6M   .
 5   *   �� .��5   .   W� $ 0:0:0.15D�6>   $
 0.15X�6R   $ (UIElement.Opacity)�6\   $
 grid��6�   W� 5   .   $ 0:0:0.15D�6>   $ 1X�6R   $ (UIElement.Opacity)�6Y   $ 	textBlock��6�   5   +   5   '     5   &   $ 	MouseOver��67   $ 0D�6H   $	 Normal��6^   .
 5   *   �� .��5   .   W� $ 0:0:0.3D�6>   $ 1X�6Q   $ (UIElement.Opacity)�6X   $
 grid��6�   W� 5   .   $ 0:0:0.3D�6>   $ 0X�6Q   $ (UIElement.Opacity)�6X   $ 	textBlock��6�   5   +   5    '     5!   &   $ 	MouseOver��67   $ 0D�6H   $	 Pressed��6^   .
 5"   *   �� .��5#   .   W� $ 0:0:0.3D�6>   $ 0X�6Q   $ (UIElement.Opacity)�6X   $
 grid��6�   W� 5$   .   $ 0:0:0.3D�6>   $ 0X�6Q   $ (UIElement.Opacity)�6X   $ 	textBlock��6�   U� 5%   .   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6L   $ grid1��6�   .\�5&   2     $ 0:0:0.3��6G   $	 1.5Q�6Y   5'   /   U� 5(   .   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6L   $ grid1��6�   .\�5)   2     $ 0:0:0.3��6G   $	 1.5Q�6Y   5*   /   5+   +   5,   '   . 5.   "     
Normal��6.     5/   "   	MouseOver��. 50   &   �� .��51   *   U� $ (UIElement.Opacity)�6H   $
 grid��6x   .\�52   .     $ 0��6C   $
 0.15Q�6O   53   +   U� 54   *   $ (UIElement.Opacity)�6H   $ 	textBlock��6x   .\�55   .     $ 0��6C   $ 1Q�6O   56   +   57   '   58   #     59   "   Pressed��6.     5:   "   Disabled��6.   5;      .+�5=      � $
 grid��6    5>        5?   "   $	 -90Q�63   $
 0.75Q�6C   $ 12Q�6R   $ 2Q�6b   .+�5A      �� 
���   �$
 0.35Q�68   �� 5B      
�������$��1q�68   �� 5C      #��z��6/   #��z��6r   $��4q�6�   ��5D   "   �� 5E   &   $
��None=�6K   ��6,   �� *ImageSource+6K   5G      5H      �� 5I      $ 	textBlock��6$   $ Center=�67   $��Center=�6N   $	��160��6k   $
��Wrap=�6z   $��Center=�6�   $��21.333(�6�   
�������$ 0Q�6�   $��0,0,0,5q�6�   ��6�   �� *Name+ 5J   	     5K   
   $	 -90Q�6   $ 0Q�6+   $ 10Q�6;   5M      5N      5O      5Q      5R      5S      