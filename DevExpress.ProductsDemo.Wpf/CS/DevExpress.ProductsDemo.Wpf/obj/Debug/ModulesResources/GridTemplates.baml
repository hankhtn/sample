   M S B A M L   `   `   `  ����W  SDevExpress.ProductsDemo.Wpf, Version=18.1.4.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ProductsDemo.ModulesProductsDemo.Modules  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      a ]DevExpress.Xpf.Core.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35f bDevExpress.Xpf.Grid.v18.1.Core, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ac _DevExpress.Xpf.Ribbon.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aj	 fDevExpress.Xpf.LayoutControl.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]
 YDevExpress.Mvvm.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ae aDevExpress.Xpf.Printing.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v18.1.Extensions, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891   +DevExpress.Mvvm.UI.ObjectToBooleanConverter   ObjectToBooleanConverter.   (ProductsDemo.Modules.ColumnIconConverter  ColumnIconConverter2   ,ProductsDemo.Modules.ColumnPriorityConverter  ColumnPriorityConverter7   1ProductsDemo.Modules.ColumnCategoryImageConverter   ColumnCategoryImageConverter9   3ProductsDemo.Modules.ColumnFlagStatusImageConverter " ColumnFlagStatusImageConverter/   )ProductsDemo.Modules.SplitStringConverter  SplitStringConverter0  *DevExpress.Mvvm.UI.ObjectToObjectConverter  IsGroupedConverter    DefaultSource
   Map   DevExpress.Mvvm.UI.MapItem   Source   Target7   1ProductsDemo.Modules.TaskStatusCompletedConverter   TaskStatusCompletedConverter.	  (DevExpress.Mvvm.UI.FormatStringConverter  IntToItemsConverter 	  FormatString7
   1ProductsDemo.Modules.FieldNameToTemplateConverter #	 ColumnToHeaderTemplateConverter 
  TargetTemplateName !
 ColumnToCellTemplateConverter $  ColumnToDisplayTemplateConverter ' #ColumnToGroupValueTemplateConverter # ColumnToPrintCellStyleConverter + 'ColumnToPrintColumnHeaderStyleConverter/  )DevExpress.Xpf.Core.EnumToObjectConverter   PrefixToImageSourceConverter   Values�  �System.Collections.Generic.Dictionary`2[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]],  &DevExpress.Xpf.Core.EnumObjectProvider  Key   Value:   4ProductsDemo.Modules.MaskToTextEditSettingsConverter # MaskToTextEditSettingsConverter  columnGeneratorStyle �� 
TargetType$  DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.ColumnBase	 �� Property
 �� Value$  DevExpress.Xpf.Grid.BaseColumn �� Mode �� 	Converter �� RelativeSource �� 	Resources,  &DevExpress.Xpf.Editors.EnumItemsSource   EnumType%   ProductsDemo.Modules.TaskStatus!  DevExpress.Xpf.Core.DXImage*  $DevExpress.Xpf.Core.DXImageExtension   Image1  +DevExpress.Xpf.Core.Native.DXImageConverter,  &DevExpress.Xpf.Editors.ProgressBarEdit*  $DevExpress.Xpf.Editors.RangeBaseEdit   Minimum   Maximum   ContentDisplayMode   	IsPercent%  DevExpress.Xpf.Editors.BaseEdit   EditMode   	EditValue   DisplayFormatString)  #DevExpress.Xpf.Editors.ComboBoxEdit  Name'  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable+  %DevExpress.Xpf.Editors.LookUpEditBase&   ApplyItemTemplateToSelectedItem   ValueMember   ItemsSource'   !ProductsDemo.Modules.TaskPriority   DisplayTemplate   ItemTemplate �� Orientation  J� IsHitTestVisible%  DevExpress.Xpf.Editors.TextEdit!  Name"   DisplayTextConverter  EnumItemsSource# �� Source)  #DevExpress.Xpf.Editors.TrackBarEdit$  Name%   DevExpress.Xpf.Editors.DateEdit%   Name&   NullText*!  $DevExpress.Xpf.Editors.SparklineEdit' ! Name(   StyleSettings7"  1DevExpress.Xpf.Editors.LineSparklineStyleSettings3#  -DevExpress.Xpf.Editors.SparklineStyleSettings) #  HighlightMaxPoint* #  HighlightMinPoint+ #  MaxPointBrush, #  MinPointBrush- �� BasedOn;$  5DevExpress.Xpf.Grid.Themes.TableViewThemeKeyExtension�%  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Grid.Themes.TableViewThemeKeys, DevExpress.Xpf.Grid.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]. %  ResourceKey,&  &DevExpress.Xpf.Printing.ExportSettings/ &  
TargetType0 �� Path2'  ,DevExpress.Mvvm.UI.Interactivity.Interaction1 '  Triggers'(  !DevExpress.Mvvm.UI.EventToCommand�)  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]2 )  	EventName+*  %DevExpress.Mvvm.UI.EventToCommandBase3 *  Command&+   DevExpress.Xpf.Editors.ImageEdit4   IsPrintingMode5 +  Source),  #DevExpress.Xpf.Editors.TextEditBase6 ,  Text�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     ,local"clr-namespace:ProductsDemo.Modules  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      >dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      >dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  5      Fdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 	 5      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 
  5      Bdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing   5	      Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  5
      Jdxet?http://schemas.devexpress.com/winfx/2008/xaml/editors/themekeys  5      Fdxt<http://schemas.devexpress.com/winfx/2008/xaml/core/themekeys  5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      .sys&clr-namespace:System;assembly=mscorlib  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %.-  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �    &	 �    &		 �    &	
     &	 *    &	 S    &	     &	 �    &	 �    &	 H    &	 [    7  7 	 7 
 7  7  7  7  7  7  7  7  7  7  7  7  7  0�� 5�   N   *$  $. DefaultPrintCellStyle=�+17  7  7  7  7  0�� 5�   L   *$  $. DefaultPrintCellStyle=�+17  0�� 5�   J   *$  $. DefaultPrintCellStyle=�+17  0�� 5�   N   *$  $. DefaultPrintCellStyle=�+17  0�� 5�   U   *$  $. DefaultPrintCellStyle=�+10�� 5  N   *$  $. DefaultPrintCellStyle=�+17  7  0�� 5  P   *$  $. DefaultPrintCellStyle=�+17  0�� 5  V   *$  $. DefaultPrintHeaderStyle=�+10�� 5$  R   *$  $. DefaultPrintHeaderStyle=�+10�� 5.  V   *$  $. DefaultPrintHeaderStyle=�+10�� 58  X   *$  $. DefaultPrintHeaderStyle=�+10�� 5B  T   *$  $. DefaultPrintHeaderStyle=�+1   5      6&     5      6      5      6$     5      6)     5      6+     5      6!     5        5   
   ��5      True��6    . 5   
      5      ��5      	False��6%    5      ��5      -1��6    5!        5"   
    5#      ��5$      True��6$    5&      ��5'      0��6   5)      5*        5+      6)   	  5,       5-   
   ��5.      
ITEMS: {0}��6%   50      
  52      $ HeaderTemplate��6Q   
  53      $ CellTemplate��6O   
  54      $ DisplayTemplate��6R   
  55      $ GroupValueTemplate��6U   
  56      $ PrintCellStyle��6Q   
  57      $ PrintColumnHeaderStyle��6Y     59       5:   
      5;      Dr $Z Tpack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Doctor.png��6/     5<      Mr $V Ppack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Mr.png��6/     5=      Ms $V Ppack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Ms.png��6/     5>      Miss $X Rpack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Miss.png��61     5?      Mrs $W Qpack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Mrs.png��60   5@      5A        5B      6,   �� 5C      "  6)   .�5D   
   �� 	 �@ 	FieldName
 6&   �� *Name+�� 5E   
   	 �@ Header
 6#   �� *DisplayName+�� 5F   
   	 �@ 
GroupIndex
 6'   �� *	IsGrouped+$ TwoWay=�8   �� 5G   
   (	 �@  HorizontalHeaderContentAlignment
 6=   �� *" HorizontalHeaderContentAlignment+�� 5H   
   	 �@ AllowColumnFiltering
 61   �� *AllowFiltering+�� 5I   
   	 �@ AllowSorting
 6)   �� *AllowSorting+$ OneWay=��� 5J   
   	 �@ AllowEditing
 6)   �� *AllowEditing+$ OneWay=��� 5K   
   	 �@ GroupInterval
 6*   �� *GroupInterval+�� 5L   
   	 �@ 	SortOrder
 6&   �� *	SortOrder+$ TwoWay=��� 5M   
   	 �@ VisibleIndex
 6)   �� *Index+$ TwoWay=��� 5N   
   	 �@ Visible
 6$   �� *	IsVisible+$ OneWay=��� 5O   
   	 �@ HeaderTemplate
 6+   ��  �� *Self+8  �� 5P   
   	 �@ CellTemplate
 6)   ��  �� *Self+8  �� 5Q   
   	 �@ DisplayTemplate
 6,   ��  �� *Self+8  �� 5R   
   	 �@ GroupValueTemplate
 6/   ��  �� *Self+8  �� 5S   
   	 �@ PrintCellStyle
 6+   ��  �� *Self+8  �� 5T   
   	 �@ PrintColumnHeaderStyle
 63   ��  �� *Self+8  �� 5U   
   	 �@ Width
 6"   �� *Width+$ TwoWay=��� 5V   
   	 �@ 
FixedWidth
 6'   �� *
FixedWidth+�� 5W   
   	 �@ EditSettings
 6)   �� *Mask+8   5X   
     5Y      EnumItemsSource "  6:   �� 5[      Complete_HeaderTemplate .R�5\        $��16��6I   ��6     $ Apply_16x16.png 6I   5]      �� 5^      Icon_HeaderTemplate .R�5_        $_��Ypack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Column_Icon.png��6   $��16��6�   5`      �� 5a      Priority_HeaderTemplate .R�5b        $c��]pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Column_Priority.png��6   $��16��6�   5c      �� 5d      FlagStatus_HeaderTemplate .R�5e        $e��_pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Column_FlagStatus.png��6   $��16��6�   5f      �� 5h      #PercentComplete_DisplayTemplate .R�5i        $ 0Q�6X   $	 100Q�6d   $ Value=�6r   $��Center=�6�   $��Center=�6�    . $ InplaceInactive=�6   6&   �� *RowData.Row.PercentComplete+��6�   Z� ��6�   Z� $ {0:P0}��5j   n   5k      5l      �� 5n      Icon_CellTemplate .R�5o        $��16��6f   ��6   �� *Value+8  6f   5p      �� 5q      Priority_CellTemplate .R�5r        $ PART_Editor��6#   $ FalseX�68   $��Center=�6O    . $ Id��6�    5s        5t      "  6.    5v      �� 5w      .R�5x        $��16��6�   ��6)   �� *RowData.Row.Priority+8 	 6�   5y       5{      �� 5|      .R�5}      �� $ 
Horizontal=�6)     .  .+�5~   "     $��16��6w   ��6-   �� *Id+8 
 6w   �� 5   "   $��5,0,0,0q�6@   $��Center=�6Q   ��6,   �� *Id+6Q   5�      5�      5�      5�      �� 5�      Subject_CellTemplate .R�5�        $! PART_Editor��6   5�      �� 5�      Status_CellTemplate .R�5�        $ PART_Editor��6#   $ Id��6x   $ FalseX�6�   8"   68   �� 8#   5�      �� 5�      .R�5�      �� ��6(   �� *Id+8  5�      5�      5�      �� 5�       PercentComplete_CellTemplate .R�5�        $$ PART_Editor��6#   $ 0Q�68   $	 100Q�6D   $��Center=�6R   $ {0:P0}��5�   h   5�      5�      �� 5�      CreatedDate_CellTemplate .R�5�         $% PART_Editor��6   $
& None��64   5�      �� 5�      StartDate_CellTemplate .R�5�         $% PART_Editor��6   $
& None��64   5�      �� 5�      DueDate_CellTemplate .R�5�         $% PART_Editor��6   $
& None��64   5�      �� 5�      CompletedDate_CellTemplate .R�5�         $% PART_Editor��6   $
& None��64   5�      �� 5�      Category_CellTemplate .R�5�      �� $ 
Horizontal=�6   .+�5�        $��16��6s   $	��2,0q�6~   ��6!   �� *Value+8  6~   �� 5�      $��5,0,0,0q�6h   $��Center=�6y   ��6    �� *Value+8  6y   5�      5�      �� 5�      Activity_CellTemplate .R�5�      !  $' PART_Editor��6$   ( 5�      "  5�      ) . * . $	+ Red��6k   $
, Blue��6   5�      5�      �� 5�      Activity_PrintCellStyle "  64   - 6N   2 .�5�      �� 	 �@& 
TargetType$
 Panel=�6B   �� 5�      	 �@ DisplayTemplate
 5�      �� 5�      .R�5�      !  $/ Image=�6`    60   �� $0 RowData.Row.Activity�( 5�   "   "  5�   &   ) . * . $	+ Red��6w   $
, Blue��6�   5�      5�      5�      5�      �� 5�      FlagStatus_CellTemplate .R�5�        $��16��6q   ��6   �� *Value+8  1 5�      (  5�      $2 MouseLeftButtonUp��60   3 6N   �� *#!RowData.Row.ChangeCompleteCommand+5�      5�      �� 5�      Priority_GroupValueTemplate .R�5�      �� $ 
Horizontal=�6   .+�5�      �� $��	Priority:��6      5�      $��16��6}   $	��4,0q�6�   ��6!   �� *RowData.Row.Priority+8  6�   �� 5�      ��6    �� *RowData.Row.Priority+5�      5�      �� 5�      Category_GroupValueTemplate .R�5�      �� $ 
Horizontal=�6   .+�5�      �� $��	Category:��6      5�      $��16��6�   $	��4,0q�6�   ��6!   �� *RowData.Row.Category+8  6�   �� 5�      ��6    �� *RowData.Row.Category+8  5�      5�      �� 5�      Prefix_CellTemplate .R�5�        $��16��6o   ��6   �� *Value+8  6o   5�      �� 5�      Prefix_PrintCellStyle "  62   - 6L   2 .�5�      �� 	 �@& 
TargetType$
 Panel=�6B   �� 5�      	 �@ DisplayTemplate
 5�      �� 5�      .R�5�      +  4 . $��16��6�   5 6B   �� *Value+8  6�   5�      5�      5�      �� 5�      Icon_PrintCellStyle "  60   - 6J   2 .�5�      �� 	 �@& 
TargetType$
 Panel=�6B   �� 5�      	 �@ DisplayTemplate
 5�      �� 5�      .R�5�      +  4 . $��16��6�   5 6B   �� *Value+8  6�   5�      5�      5�      �� 5�      Priority_PrintCellStyle "  64   - 6N   2 .�5�      �� 	 �@& 
TargetType$
 Panel=�6B   �� 5�      	 �@ DisplayTemplate
 5�      �� 5�      .R�5�      +  4 . $��16��6�   5 6B   �� *Value+8  6�   5�      5�      5�      �� 5�      "PercentComplete_PrintCellStyle "  6;   - 6U   2 .�5�      �� 	 �@& 
TargetType$
 Panel=�6B   �� 5�      	 �@ DisplayTemplate
 5�      �� 5�      .R�5�        4 . $��Center=�6]    6A   �� *Value+$
 {0}%��5�   j   5�      5�      5      5     �� 5     Category_PrintCellStyle "  64   - 6N   2 .�5     �� 	 �@& 
TargetType$
 Panel=�6B   �� 5     	 �@ DisplayTemplate
 5     �� 5     .R�5     �� $ 
Horizontal=�6)   $/ Panel=�6B   .+�5  "   +  4 . $��16��6�   $	��2,0q�6�   5 6F   �� *Value+8  6�     5	  "   4 . $��5,0,0,0q�6�   $��Center=�6�   6 6E   �� *Value+8  6�   5
     5     5     5     �� 5     FlagStatus_PrintCellStyle "  66   - 6P   2  .�5     �� 	 �@& 
TargetType$
 Panel=�6B   �� 5     	 �@ DisplayTemplate
 5     �� 5     .R�5     +  4 . $��16��6�   5 6B   �� *Value+8 ! 6�   5     5     5     �� 5     #Complete_PrintColumnHeaderStyle "  6<   - 6V   2" .�5     �� 	 �@& 
TargetType$
 Panel=�6B   �� 5     	 �@ DisplayTemplate
 5     �� 5     .R�5     +  4 . $��16��6n   5 6B     $ Apply_16x16.png 6n   5      5"     5#     �� 5$     Icon_PrintColumnHeaderStyle "  68   - 6R   2# .�5%     �� 	 �@& 
TargetType$
 Panel=�6B   �� 5&     	 �@ DisplayTemplate
 5'     �� 5(     .R�5)     +  4 . $_5 Ypack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Column_Icon.png��6B   $��16��6�   5*     5,     5-     �� 5.     #Priority_PrintColumnHeaderStyle "  6<   - 6V   2$ .�5/     �� 	 �@& 
TargetType$
 Panel=�6B   �� 50     	 �@ DisplayTemplate
 51     �� 52     .R�53     +  4 . $a5 [pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Priority_High.png��6B   $��16��6�   54     56     57     �� 58     %!FlagStatus_PrintColumnHeaderStyle "  6>   - 6X   2% .�59     �� 	 �@& 
TargetType$
 Panel=�6B   �� 5:     	 �@ DisplayTemplate
 5;     �� 5<     .R�5=     +  4 . $e5 _pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Column_FlagStatus.png��6B   $��16��6�   5>     5@     5A     �� 5B     !Prefix_PrintColumnHeaderStyle "  6:   - 6T   2& .�5C     �� 	 �@& 
TargetType$
 Panel=�6B   �� 5D     	 �@ DisplayTemplate
 5E     �� 5F     .R�5G       4 . 6+   5H     5J     5K     5M     5N     