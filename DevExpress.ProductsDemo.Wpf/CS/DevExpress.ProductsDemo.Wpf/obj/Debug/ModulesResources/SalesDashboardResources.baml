   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      W SDevExpress.ProductsDemo.Wpf, Version=18.1.4.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ProductsDemoProductsDemo 5	      a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089l hDevExpress.Xpf.Core.v18.1.Extensions, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a] YDevExpress.Mvvm.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ac	 _DevExpress.Xpf.Charts.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ac
 _DevExpress.Xpf.Gauges.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a)   #ProductsDemo.ResourceImageConverter   ResourceImageConverter " navigationTileNormalBackground  � Color % !navigationTileMouseOverBackground $  navigationTileSelectedBackground  navigationTileTemplate � RowDefinitions �� Orientation   loginButtonNormalBorderBrush �� Color # loginButtonMouseOverBorderBrush ! loginButtonPressedBorderBrush  loginButtonForeground 	 
powderBlue 
 	redViolet  blue  headerBrush  	textBrush  separatorBrush 
 yellow  backgroundBrush) 	 #DevExpress.Xpf.Charts.CustomPalette  palette   Colors  chartLabelFontSize  salesVolumesCaptionFontSize  salesVoluemesFontSize  salesVolumesFontWeight  doughnutSeriesLabelTemplate �� Path  barSeriesLabelTemplate6 0ProductsDemo.BillionStringToShortStringConverter ' #billionStringToShortStringConverter  axisYLabelTemplate �� 	Converter �� 
TargetType  topTextBlockStyle �� Property	 �� Value  bottomTextBlockStyle/  )ProductsDemo.DecimalToRangeValueConverter   decimalToRangeValueConverter  gaugeLabelTemplate
 J� RenderTransformOrigin0 
 *DevExpress.Xpf.Gauges.CircularGaugeControl  circularGaugeStyle2 
 ,DevExpress.Xpf.Gauges.CircularFlatLightModel, 
 &DevExpress.Xpf.Gauges.GaugeControlBase$ 
 DevExpress.Xpf.Gauges.ArcScale  arcScaleStyle! 
 DevExpress.Xpf.Gauges.Scale] YDevExpress.Data.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5	  /DevExpress.Utils.Design.DefaultBooleanConverter0
 
 *DevExpress.Xpf.Gauges.ArcScaleLabelOptions- 
 'DevExpress.Xpf.Gauges.ScaleLabelOptions   FormatString   Offset�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      >dxc4http://schemas.devexpress.com/winfx/2008/xaml/charts 	 5      ?dxga4http://schemas.devexpress.com/winfx/2008/xaml/gauges 
 5      .sys&clr-namespace:System;assembly=mscorlib  5      #democlr-namespace:ProductsDemo  5	      %�  &	        &	      &	 P     &	 �     &	 �     &	 �    &	 �    &	 �    &	     &		 =    &	
 b    &	 �    &	 �    &	 �    &	 �    &	     &	 @    &	 c    &	 `    &	 �    &	 �    &	 �    &	 �    7  &	 �    7  &	 J    &	 ]    7  7  7  &	 �    7  7  &	 -    7  7  &	 �    &	 �    7  7  &	 L	    &	 �	       5      6"   �� 5      .��5   
   � $  	#FF469EA5��6   5      �� 5      .��5   
   � $  	#FF4668A5��6   5      �� 5      .��5   
   � $  	#FF4668A5��6   5      �� 5      .R�5   
   � $��10,4,4,4q�5       5      �� 5      $��* �5      �� 5      $
��Auto �5      .+�5       �� $ Vertical=�5!      .+�5"      �� $��22(�5$      
���������5#      �� *	Caption+5%      �� 5&      $��12(�5(      
���������5'      �� *Description+5)      5*      �� 5+      $��1��5,      $��Right=�5-      $
��None=�5/      ��5.      �� *Glyph+5/      50      51      �� 53      $ #a6a6a6��55   	   �� 56      $ #7eb4ea��58   	   �� 59      $ #7eb4ea��5;   	   �� 5<      $ Black��5>   	   �� 5?      $ #469ea5��5A   	   �� 5B      $ #a54671��5D   	   �� 5E      $ #4668a5��5G   	   �� 5H      $ #141414��5J   	   �� 5K      $ #6F7170��5M   	   �� 5N      $ #E3E3E3��5P   	   �� 5Q      $ #fcc653��5S   	   �� 5T      $ White��5V   	     5X       5Z   
   ��5[      #4668a5��6   ��5\      #a54671��6   ��5]      #49a4be��6   ��5^      #469ea5��6   ��5_      #5848a5��6   ��5`      #9462ae��6   ��5a      #fcc653��6   5c      X�5e      10.0Q�5h      X�5i      13.0Q�5l      X�5m      24.0Q�5p      #�5q      	Light"�5t      �� 5v      .R�5x   
   �� ��5y      Z� ._�5z      �� 8��  ��5{      �� $
 Text���5}      �� 5~       5      �� $ Color�5�      5�      5�      �� 5�      .R�5�   
   �� ��5�      �� 5�       5�      �� $ Color�._�5�      �� 8��  
���������5�      �� $
 Text�5�      5�      5�        5�      6/   �� 5�      .R�5�   
   �� 8��  8�� ��5�      �� $ Content�8  5�      5�      �� 5�      " ��5�   	   .�5�   
   ��  � ��8	   5�      �� 5�   
    � ��8	  5�      5�      �� 5�      " ��5�   	   .�5�   
   ��  � ��8	   5�      �� 5�   
    � ��8	  5�      �� 5�   
    � ��$	 0,-5,0,0q�5�      5�        5�      6(   �� 5�      .R�5�   
   �� $
 0.5, 0.5&�5�      8��  8�� ��5�      �� *Text+5�      5�      �� 5�      "  5�   	   .�5�   
   ��  �@ Model	 5�        5�      5�      �� 5�   
    �@ EnableAnimation	 . 5�      5�      �� 5�      "  5�   	   .�5�   
   ��  �@ 
LayoutMode$	 Circle=�5�      �� 5�   
    �@ 
ShowLabels$
	 True	 5�      �� 5�   
    �@ ShowLine$	 False	 5�      �� 5�   
    �@ ShowMajorTickmarks$
	 True	 5�      �� 5�   
    �@ ShowMinorTickmarks$	 False	 5�      �� 5�   
    �@ 
StartAngle$
	 -210Q�5�      �� 5�   
    �@ EndAngle$	 30Q�5�      �� 5�   
    �@ 
StartValue$	 0Q�5�      �� 5�   
    �@ MajorIntervalCount$	 3��5�      �� 5�   
    �@ LabelOptions	 5�      
  5�      $ 	{0:$0,,M}��5�      $ -6Q�5�      5�      5�      5�      