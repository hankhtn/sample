   M S B A M L   `   `   `  ����  DevExpress.ProductsDemo.Wpf!    ProductsDemo.ProgressWindow   5   	   a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      l hDevExpress.Xpf.Core.v18.1.Extensions, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      -   5          �Name$  progressWindow��5	   .    � WindowStyle$
 None=�5       � 
ResizeMode$ NoResize=�6)    � AllowsTransparency . 
������  � ShowInTaskbar .   � Topmost .  � 	Resources 5
         
storyboard �� 
TargetName �� TargetProperty	 W� From	
 W� To g� Duration W� EasingFunction2  ,System.Windows.Media.Animation.QuadraticEase��%�   &	        �� 5   
   .��5      W� $ progressWindow��6   $ Opacity�6E   $		 1.0X�6i   $
 0X�6t   $ 0:0:2.5D�6{    5        5      5      5      5      .��5      � $��Center=�6   $��Center=�6(   .+�5   
   �� $��1q�6   
���   �$��76��69   ._�5      )  #DevExpress.Xpf.Core.BackgroundPanel  .��5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6+   �� 5      $
��Auto �6+   .+�5      � -   5         �Name$
 Ring��5       J� RenderTransformOrigin$ 0.5,0.5&�6)   $��Center=�6I   $��27,0,7,0q�6d   $��32��6v   $��32��6�   z�5      �� 5       � Triggers 5      :� 5        :� RoutedEvent$ Grid.Loaded��6+   [�5!   "   �� 5"   &   .��5#   *   �� $4 .(Grid.RenderTransform).(RotateTransform.Angle)�65    �� Target 6�   ��  �� ElementName$
 Ring��.��5$   .   W�  g� RepeatBehavior$ Forever��6>   $	 0X�6W   $	
 360X�6`   $ 0:0:1D�6i   5%   +   5&   '   5(      .+�5*      F�  �� Stretch$
 Fill=�6   ���#f78a09 J� Opacity$ 1Q�6=   \����p�K@��-�и�K@��-�`6�K���-@�K0�. %�K�:+.���]K�-Ӏ/sK`��-0�K�޻-p�K@��-$��Right=�6�   $��9.585��6�   $��0,0,0,5.415q�6  $��Bottom=�6  $��7.516��68  F� 5+      $
 Fill=�6   ���#f78a09$
 0.85Q�6=   \����и�K�tz-�p�K�tz-�0�K(�W-�/sK��7-��]K�N-� %�KX3�,�@�K��-`6�Kp�B-и�K�tz-$��Right=�6�   $��9.584��6�   $��0,5.384,0,0q�6  $	��Top=�6   $��7.516��68  F� 5,      $
 Fill=�6   ���#f78a09$	 0.7Q�6=   \����@L�J��,�@L�J�,�0L$K�~�, WKƯ, z~K��,� vHKx�-Ӑ�.Kȅ�,�=K���,@L�J��,$��Right=�6�   $��7.517��6�   $��0,0,5.381,0q�6  $	��Top=�6"  $��9.585��6:  F� 5-      $
 Fill=�6   ���#f78a09$
 0.55Q�6=   \�������J�,����J��,�p֪J���,p�Jȅ�, �qJx�-��r;J��,� �bJƯ,�ǕJ�~�,���J�,$
��Left=�6�   $��7.516��6�   $��
5.41,0,0,0q�6  $	��Top=�6!  $��9.585��69  F� 5.      $
 Fill=�6   ���#f78a09$	 0.4Q�6=   \���� �%J@7�,�0\J�N-Ӏ�FJ��7-�9J(�W-��5J�tz-�0[�I�tz-Ӡ��Ip�B-�$J��- �%J@7�,$
��Left=�6�   $��9.584��6�   $��0.03,5.385,0,0q�6
  $	��Top=�6"  $��7.516��6:  F� 5/      $
 Fill=�6   ���#f78a09$
 0.25Q�6=   \����PX5J@��-� ��I@��-�ph�I���-��JH�.�y%J�:+.� �[J�-�PoFJ`��-��8J�޻-PX5J@��-$
��Left=�6�   $��9.585��6�   $��0,0,0,5.415q�6
  $��Bottom=�6  $��7.516��6:  F� 50      $
 Fill=�6   ���#f78a09$	 0.1Q�6=   \�������J �}.����JY1.�p֪J`4..p�JPa . �qJ�+.��r;JHCA.� �bJ!d.�ǕJ0hz.���J �}.$
��Left=�6�   $��7.516��6�   $��
5.41,0,0,0q�6  $��Bottom=�6  $��9.585��62  51      �� 52      $��1��6!   $��Center=�61   $��	12,0,27,0q�6L   .+�53      �� $��Please Wait��6$   $��16(�67   �� 54      $��
Loading...��6$   $��14(�66   55      56      57      58      59      5:      