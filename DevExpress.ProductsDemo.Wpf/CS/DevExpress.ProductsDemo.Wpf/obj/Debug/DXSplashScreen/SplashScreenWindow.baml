   M S B A M L   `   `   `  ����  DevExpress.ProductsDemo.Wpf%    ProductsDemo.SplashScreenWindow   5      a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5          �Name$  splashWindow��5       � WindowStyle$
 None=�5       � 
ResizeMode$ NoResize=�6   $	��800��6.   $	��600��6:    � AllowsTransparency . 
������  � ShowInTaskbar .   � Topmost .  � 	Resources 5
         
storyboard �� 
TargetName �� TargetProperty	 W� From	
 W� To g� Duration W� EasingFunction2  ,System.Windows.Media.Animation.QuadraticEase��%�   &	        �� 5   
   .��5      W� $ splashWindow��6   $ Opacity�6C   $		 1.0X�6g   $
 0X�6r   $ 0:0:2.5D�6y    5        5      5      5      5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5   
   � -   5        $ Splash��5      $	��450��6   $��Center=�6+   $	��Top=�6H   $��	0,100,0,0q�6`   .+�5      � -   5        $
 Back��5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 1Q�6'      	Direction$	 -90Q�67      
BlurRadius$ 10Q�6G      Opacity$
 0.25Q�6W   .+�5      �� 
���   � �� CornerRadius$ 3��6,    J� Opacity$
 0.15Q�6=   �� 5      $ 2��6   $��1q�6*   
�������65   5      � 5      -   5        $ Content_Area��5      $��12q�6)    � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5       $��* �6$   �� 5!      $
��Auto �6$   .+�5#      �� -   5         ��Name$ Image��5#      $��	Image.png��6'   $
��None=�6:   �� 5$      -   5         ��Name$
 Info��5$      $
��Wrap=�6*   $��
Loading...��6>   $��1��6P   $��
12,12,12,0q�6]   ���	#FF2D2D2D6q   � 5%      -   5         �Name$ progressBar��5%      $��12��63   $��2��6?   $��12q�6L   ]� 5&      -	   5         ]�Name$ Footer��5&      $��3��6,   $��
12,20,12,4q�69   .+�5'      �� -
   5        $ Footer_Text��5'       $
��Wrap=�65   $	��150��6�   $	 0.5Q�6�   ���	#FF2D2D2D$
��Left=�6�   $��Center=�6�   ��6I   ��  �� ElementName$ splashWindow�� �� Path$ 	Copyright�6�   �� 5(      -   5        $ Logotype��5(      $��Right=�6.   $��Logo.png��6E   $
��None=�6W   $��Right=�6f   5)      5*      5+      5,      5-      