Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Ribbon
Imports System.Windows.Media
Imports DevExpress.Mvvm.DataAnnotations

Namespace RibbonDemo
    <POCOViewModel>
    Public Class RibbonSimplePadOptionsViewModel
        Public Overridable Property TitleBarVisibility() As RibbonTitleBarVisibility
        Public Overridable Property PageCategoryColor() As Color
        Public Overridable Property ToolbarShowMode() As RibbonQuickAccessToolbarShowMode
        Public Overridable Property PageCategoryAlignment() As RibbonPageCategoryCaptionAlignment
        Public Overridable Property RibbonStyle() As RibbonStyle

        Protected Sub New()
        End Sub
        Public Shared Function Create() As RibbonSimplePadOptionsViewModel
            Dim options As RibbonSimplePadOptionsViewModel = ViewModelSource.Create(Function() New RibbonSimplePadOptionsViewModel())
            options.TitleBarVisibility = RibbonTitleBarVisibility.Auto
            options.PageCategoryColor = Colors.Orange
            options.ToolbarShowMode = RibbonQuickAccessToolbarShowMode.ShowAbove
            options.PageCategoryAlignment = RibbonPageCategoryCaptionAlignment.Left
            options.RibbonStyle = RibbonStyle.Office2010
            Return options
        End Function
    End Class
End Namespace
