   M S B A M L   `   `   `  ����W  SDevExpress.ProductsDemo.Wpf, Version=18.1.4.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ProductsDemoProductsDemo  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089(    "ProductsDemo.PhotoGalleryResources   ImageResources  VerticalIncrementButton  �� 
TargetType �� Property �� Value �� 
TargetType �Name J� SnapsToDevicePixels'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 g� Duration	
 �� To �� TargetProperty �� 
TargetName	 W� To F�Name �� Stretch �� Color  VerticalDecrementButton  HorizontalDecrementButton  HorizontalIncrementButton  ScrollBarPageButton  VerticalThumb R� KeyTime R� Value  System.Windows.Visibility ��Name �� CornerRadius  HorizontalThumb  DescriptionScrollBar � RowDefinitions �� LineUpCommand ]�Name � 	Resources ]� DecreaseRepeatButton �� PageUpCommand ]� IncreaseRepeatButton �� PageDownCommand ]� Thumb �� LineDownCommand �� Triggers  P� Property! P� Value" � ColumnDefinitions# �� LineLeftCommand$ �� PageLeftCommand% �� PageRightCommand& �� LineRightCommand 	 DescriptionScrollViewer' ��Name( ��Name) �� CanContentScroll* �� CanHorizontallyScroll+ �� CanVerticallyScroll, J� IsHitTestVisible- ��Name4  .System.Windows.Automation.AutomationProperties.   AutomationId/ � Cursor0 �� ScrollableHeight1 � Maximum2 � Minimum*3 �� #ComputedVerticalScrollBarVisibility4 �� ViewportHeight5 �� ViewportSize6 � Value7 �� Mode8 �� RelativeSource9 �� ScrollableWidth: �� Orientation,; �� %ComputedHorizontalScrollBarVisibility< �� ViewportWidth 
 NavButtonStyle=   Transitions%  System.Windows.VisualTransition>   GeneratedDuration	?   To@   
StoryboardA   From9  3System.Windows.Media.Animation.EasingDoubleKeyFrameB O� KeyTimeC O� ValueD ��NameE J� Opacity#   ProductsDemo.PlaceInfoControlF ��NameG �� Path	   ProductsDemo.NavDirectionH �� Source�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   $localclr-namespace:ProductsDemo  5   	   .sys&clr-namespace:System;assembly=mscorlib  5   	   %�6  &	        &	      &	 X    &	 �    &	 �    &	     &	     &	 �    &	 �    7  7  7  7  7  7  7  7  7  7  &		 �!    7  7  &	
 :&    '  i+    7 
 7   7 
 7   7 	    5      6"   �� 5      "  ��6,   .�5   
   ��  �@�OverridesDefaultStyle . 62   �� 5	   
    � |� .  6&   �� 5
   
    � �� .  6&   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      � $
 Root��6   ���	#00000000 .  5        5      CommonStates��. 5   "     
Normal��6.     5   "   	MouseOver��. 5   &   �� .��5   *   �� $	 0D�69   $
 	#FFFFFFFFX�6F   $* $(Shape.Fill).(SolidColorBrush.Color)�6U   $
 path��6�   5   '   5   #     5   "   Pressed��. 5   &   �� .��5   *   �� $	 0D�69   $
 	#50FFFFFFX�6F   $* $(Shape.Fill).(SolidColorBrush.Color)�6U   $
 path��6�   5   '   5   #     5   "   Disabled��. 5   &   �� .��5   *   W� $	 0D�6:   $
 0.25X�6G   $ (UIElement.Opacity)�6Q   $
 path��6�   5   '   5    #   5!      .+�5#      F� $
 path��6   +������u0��2G �u0�П���Ѹ��u0$��5��6   $ Uniform=�6�   $��7��6�   ��5$      �� 5%   "   ButtonColor��$ 	#AFFFFFFF��6G   5'      5(      5)      5+      5,      �� 5-      "  ��6,   .�5.   
   ��  �@�OverridesDefaultStyle . 62   �� 5/   
    � |� .  6&   �� 50   
    � �� .  6&   �� 51   
    � �� 52      �� 53      " ��6"   .R�54      � $
 Root��6   ���	#00000000 55        56      CommonStates��. 57   "     
Normal��6.     58   "   	MouseOver��. 59   &   �� .��5:   *   �� $	 0D�69   $
 	#FFFFFFFFX�6F   $* $(Shape.Fill).(SolidColorBrush.Color)�6U   $
 path��6�   5;   '   5<   #     5=   "   Pressed��. 5>   &   �� .��5?   *   �� $	 0D�69   $
 	#50FFFFFFX�6F   $* $(Shape.Fill).(SolidColorBrush.Color)�6U   $
 path��6�   5@   '   5A   #     5B   "   Disabled��. 5C   &   �� .��5D   *   W� $	 0D�6:   $
 0.25X�6G   $ (UIElement.Opacity)�6Q   $
 path��6�   5E   '   5F   #   5G      .+�5I      F� $
 path��6   +�����2G �X
Ѹ��X
�П�б�	��2G �X
$��5��6~   $ Uniform=�6�   $��7��6�   ��5J      �� 5K   "   ButtonColor��$ 	#AFFFFFFF��6G   5M      5N      5O      5Q      5R      �� 5S      "  ��6.   .�5T   
   ��  �@�OverridesDefaultStyle . 62   �� 5U   
    � |� .  6&   �� 5V   
    � �� .  6&   �� 5W   
    � �� 5X      �� 5Y      " ��6"   .R�5Z      � $
 Root��6   ���	#00000000 5[        5\      CommonStates��. 5]   "     
Normal��6.     5^   "   	MouseOver��. 5_   &   �� .��5`   *   �� $	 0D�69   $
 	#FF564B32X�6F   $* $(Shape.Fill).(SolidColorBrush.Color)�6U   $
 path��6�   5a   '   5b   #     5c   "   Pressed��. 5d   &   �� .��5e   *   �� $	 0D�69   $
 	#FFCABB9AX�6F   $* $(Shape.Fill).(SolidColorBrush.Color)�6U   $
 path��6�   5f   '   5g   #     5h   "   Disabled��. 5i   &   �� .��5j   *   W� $	 0D�6:   $
 0.25X�6G   $ (UIElement.Opacity)�6Q   $
 path��6�   5k   '   5l   #   5m      .+�5o      F� $
 path��6   +�����X
�2G ��X
���б�	П���X
�2G $��6��6~   $ Uniform=�6�   $��10��6�   ��5p      �� 5q   "   ButtonColor��$ 	#FFB39F74��6G   5s      5t      5u      5w      5x      �� 5y      "  ��6.   .�5z   
   ��  �@�OverridesDefaultStyle . 62   �� 5{   
    � |� .  6&   �� 5|   
    � �� .  6&   �� 5}   
    � �� 5~      �� 5      " ��6"   .R�5�      � $
 Root��6   ���	#00000000 5�        5�      CommonStates��. 5�   "     
Normal��6.     5�   "   	MouseOver��. 5�   &   �� .��5�   *   �� $	 0D�69   $
 	#FF564B32X�6F   $* $(Shape.Fill).(SolidColorBrush.Color)�6U   $
 path��6�   5�   '   5�   #     5�   "   Pressed��. 5�   &   �� .��5�   *   �� $	 0D�69   $
 	#FFCABB9AX�6F   $* $(Shape.Fill).(SolidColorBrush.Color)�6U   $
 path��6�   5�   '   5�   #     5�   "   Disabled��. 5�   &   �� .��5�   *   W� $	 0D�6:   $
 0.25X�6G   $ (UIElement.Opacity)�6Q   $
 path��6�   5�   '   5�   #   5�      .+�5�      F� $
 path��6   +�����u0����u0�2G ���П���u0��$��6��6   $ Uniform=�6�   $��10��6�   ��5�      �� 5�   "   ButtonColor��$ 	#FFB39F74��6G   5�      5�      5�      5�      5�      �� 5�      "  ��6(   .�5�   
   ��  �@�OverridesDefaultStyle . 62   �� 5�   
    � ��
 ���� 6'   �� 5�   
    � |� .  6&   �� 5�   
    � �� .  6&   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      � $
 Root��6   
������  5�        5�      CommonStates��. 5�   "     
Normal��6.     5�   "   	MouseOver��6.     5�   "   Pressed��6.     5�   "   Disabled��6.   5�      5�      5�      5�      5�      �� 5�      "  n�6"   .�5�   
   ��  �@�OverridesDefaultStyle . 62   �� 5�   
    � �� .  6&   �� 5�   
    � �� 5�      �� 5�      " n�6"   .R�5�      �  5�        5�      CommonStates��. 5�   "     
Normal��6.     5�   "   	MouseOver��. 5�   &   �� .��5�   *   �� $	 0D�69   $
 	#FFFFFFFFX�6F   $1 +(Border.Background).(SolidColorBrush.Color)�6U   $ border��6�   5�   '   5�   #     5�   "   Pressed��. 5�   &   �� .��5�   *   �� $	 0D�69   $
 	#50FFFFFFX�6F   $1 +(Border.Background).(SolidColorBrush.Color)�6U   $ border��6�   5�   '   5�   #     5�   "   Disabled��. 5�   &   �� .��5�   *   T� $ (UIElement.Visibility)�6H   $ border��6{   ..�5�   .   i� $ 0��6E    5�   2    5�   6   	Collapsed=�6L   5�   /   5�   +   5�   '   5�   #   5�      .+�5�      �� $ border��6!   $ 3��61   ���	#AFFFFFFF$��7��6Y   5�      5�      5�      5�      �� 5�      "  n�6$   .�5�   
   ��  �@�OverridesDefaultStyle . 62   �� 5�   
    � �� .  6&   �� 5�   
    � �� 5�      �� 5�      " n�6"   .R�5�      �  5�        5�      CommonStates��. 5�   "     
Normal��6.     5�   "   	MouseOver��. 5�   &   �� .��5�   *   �� $	 0D�69   $
 	#FF564B32X�6F   $1 +(Border.Background).(SolidColorBrush.Color)�6U   $ border��6�   5�   '   5�   #     5�   "   Pressed��. 5�   &   �� .��5�   *   �� $	 0D�69   $
 	#FFCABB9AX�6F   $1 +(Border.Background).(SolidColorBrush.Color)�6U   $ border��6�   5�   '   5�   #     5�   "   Disabled��. 5�   &   �� .��5�   *   T� $ (UIElement.Visibility)�6H   $ border��6{   ..�5�   .   i� $ 0��6E    5�   2    5�   6   	Collapsed=�6L   5�   /   5�   +   5�   '   5�   #   5�      .+�5�      �� $ border��6!   $ 4��61   ���	#FFB39F74$��8��6Y   $��0,1,0,0q�6g   5�      5�      5     5     �� 5     "  ��6)   .�5  
   ��  � ��
 ���� 6'   �� 5  
    �@��IsPressAndHoldEnabled .  69   �� 5  
    �@��IsFlicksEnabled .  63   �� 5  
    � ��# � ��6'   �� 5  
    � ��$ 17��6"   �� 5	  
    � ��$ 17��6%   �� 5
  
    � �� 5     �� 5     " ��6"   .R�5     � $ Bg��6   #��z��6'    .  5     �� 5     #��� q�6,   �� 5     $��0.00001* �6,   �� 5     #��� q�6,   .+�5     �� #��Z 6'   8��  6T   ]� 5     $ 
PART_Track��6    ��. $��1��6O    5     ��X�5  "   (��       6-   *31SystemParameters.VerticalScrollBarButtonHeightKey+)36Q�6v   5      5     �� 5  "   #��Z 6/   8�� 6\    5     �� 5  "   #��Z 6/   8�� 6^    5     n� 5  "   8�� 6(   5!     �� 5"     #��Z 6'   $��2��6V   8�� 6c   5#     5$     5&      5'  
   P� 5(       �@��Orientation$! 
Horizontal=�6-   . �5)     ��  � ��$
 Auto��6*   �� 5*      � ��$ 0��6-   �� 5+      � ��$ 17��6+   �� 5,      � ��$ 17��6.   �� 5-      � �� 5.     �� 5/     " ��6*   .R�50     � $ Bg��6#   #��z��6/    . " 51  "   �� 52  &   #��� ��67   �� 53  &   $��0.00001* �67   �� 54  &   #��� ��67   .+�56  "   �� #��Z# 6/   8�� 6^   ]� 57  "   $ 
PART_Track��6(   $��1��6<    58  &   ��X�59  *   (��       65   *42SystemParameters.HorizontalScrollBarButtonWidthKey+)36Q�6   5:  '    5;  &   �� 5<  *   #��Z$ 67   8�� 6f    5>  &   �� 5?  *   #��Z% 67   8�� 6g    5A  &   n� 5B  *   8�� 60   5D  #   �� 5E  "   $��2��6/   #��Z& 6?   8��	 6o   5F     5G     5I     5J     5L     �� 5M     "  ��6,   .�5N  
   ��  � �� 5O     �� 5P     " ��6"   .R�5Q     � $
 Grid��6   #��z��6)   " 5R     �� 5S     $��* �6/   �� 5T     $
��Auto �6/    5V     �� 5W     $��* �6,   �� 5X     $
��Auto �6,   .+�5Z     �� $' Corner��6$   $��1��64   
������ $��1��6W   �� 5[     $!( PART_ScrollContentPresenter��61   #) z��6V   * .  + .  #��z��6�   #��z��6�   #��z��6  $��0��6A  �� 5\     $��2��6!   #��z��65   $��0��6X   , .  6e   �� 5]     $- PART_VerticalScrollBar��6$   8��  $. VerticalScrollBar��6r   $/ Arrow��6�   $��1��6�   #1 z0 6�   $2 0Q�6�   $��0��6   #y�z3 6  #5 z4 6�  6 6P  �� *VerticalOffset+$7 OneWay=�8 �� *TemplatedParent+6�  �� 5^     $- PART_HorizontalScrollBar��6$   8�� $. HorizontalScrollBar��6t   $/ Arrow��6�   $��0��6�   #1 z9 6�   $2 0Q�6�   $: 
Horizontal=�6  $��1��6  #y�z; 6)  #5 z< 6�  6 6n  �� *HorizontalOffset+$7 OneWay=�8 �� *TemplatedParent+6�  5_     5`     5b     5c     �� 5d     "  ��6#   .�5e  
   ��  � �� 5f     �� 5g     " ��6"   .R�5h     � ���	#00000000 5i       5j     CommonStates��= 5k  "     5l  &   $> 0:0:0.15D�67   $? 	MouseOver��6T   .@ 5m  *   �� .��5n  .   W� $	 0:0:0.15D�6>   $ 1X�6R   $ (UIElement.Opacity)�6Y   $ contentPresenter��6�   5o  +   5p  '     5q  &   $A 	MouseOver��67   $> 0D�6H   $? Normal��6^   .@ 5r  *   �� .��5s  .   W� $	 0:0:0.3D�6>   $
 0.75X�6Q   $ (UIElement.Opacity)�6[   $ contentPresenter��6�   5t  +   5u  '   . 5w  "     
Normal��6.     5x  "   	MouseOver��. 5y  &   �� .��5z  *   U� $ (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5{  .     $B 0��6C   $C 1Q�6O   5|  +   5}  '   5~  #     5  "   Pressed��. 5�  &   �� .��5�  *   U� $ (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5�  .     $B 0��6C   $
C 0.35Q�6O   5�  +   5�  '   5�  #     5�  "   Disabled��. 5�  &   �� .��5�  *   U� $ (UIElement.Opacity)�6H   $ contentPresenter��6x   .\�5�  .     $B 0��6C   $
C 0.15Q�6O   5�  +   5�  '   5�  #   5�     .+�5�     �� $D contentPresenter��6+   #��z��6E   #��z��6�   $
E 0.75Q�6�   5�     5�     5�     5�     �� 5�     "   6   .�5�  
   ��  � �� 5�     �� 5�     "  6"   .R�5�     � $ Panels��6   $��Right=�6+   $��8q�6G   $	��297��6R   $y�Visible=�6^    5�     �� 5�     $
��Auto �6,   �� 5�     $��* �6,    5�       5�     VisibleStates��. 5�  "     Visible��. 5�  &   �� .��5�  *   U� $ Panels��6H   $ (UIElement.Opacity)�6g   .\�5�  .     $B 
00:00:00.5��6C   $C 1Q�6X   5�  +   T� 5�  *   $ Panels��6H   $ (UIElement.Visibility)�6g   ..�5�  .   i� $ 00:00:00��6E    5�  2    5�  6   Visible=�6J   5�  /   5�  +   5�  '   5�  #     5�  "   	Invisible��. 5�  &   �� .��5�  *   U� $ Panels��6H   $ (UIElement.Opacity)�6g   .\�5�  .     $B 
00:00:00.5��6C   $C 0Q�6X   5�  +   T� 5�  *   $ Panels��6H   $ (UIElement.Visibility)�6g   ..�5�  .   i� $ 
00:00:00.5��6E    5�  2    5�  6   	Collapsed=�6L   5�  /   5�  +   5�  '   5�  #   5�     .+�5�     � $ TopPanel��6   .+�5�     �� 
���   �$
E 0.75Q�68   �� 5�     $��10q�6)   .+�5�  "   �� $
��Wrap=�6,   $��23(�6�   
�������$��Center=�6�   $��0,0,0,10q�6�   ��6@   �� *PlaceInfo.CityName+8 �� *TemplatedParent+6�   �� 5�  "   $
��None=�6�   ��6(   �� *PlaceInfo.ImageSource+8 �� *TemplatedParent+6�   � 5�  "   " 5�  &   �� 5�  *   $
��Auto �6;   �� 5�  *   $��* �6;   �� 5�  *   $
��Auto �6;   .+�5�  &   �� $F 
PrevButton��6-   8��  $��Bottom=�6i   ��5�  -   �� $G ChangePlaceCommand�8 �� *TemplatedParent+��5�  *   	 5�  .   Previous=�6K   .��5�  *   �� $
��None=�6�   ��60   �� *!PlaceInfoControlPrevImageSource+8H  6�   5�  '   �� 5�  &   $��1��60   $
��Wrap=�6@   $��16(�6�   
�������$��Center=�6�   $��0,8,0,0q�6�   ��6T   �� *PlaceInfo.Name+8 �� *TemplatedParent+6�   �� 5�  &   $F 
NextButton��6=   $��2��6-   8�� $��Bottom=�6y   ��5�  -   �� $G ChangePlaceCommand�8 �� *TemplatedParent+��5�  *   	 5�  .   Next=�6G   .��5�  *   �� $
��None=�6�   ��60   �� *!PlaceInfoControlNextImageSource+8H  6�   5�  '   5�  #   5�     5�     � 5�     $ BottomPanel��6   $��1��64   $��	0,10,0,10q�6A   .+�5�     �� 
���   �$
E 0.75Q�68   � 5�     $��20,20,10,20q�6#   .+�5�  "   �� 8�� .��5�  &   �� 
�������$
��Left=�6�   $
��Wrap=�6�   ��60   �� *PlaceInfo.Description+8 �� *TemplatedParent+6�   5�  #   5�     5�     5�     5�     5�     5�     5�     