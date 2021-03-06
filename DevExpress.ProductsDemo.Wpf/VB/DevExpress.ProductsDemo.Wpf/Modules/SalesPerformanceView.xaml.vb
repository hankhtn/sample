Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace ProductsDemo.Modules



    Partial Public Class SalesPerformanceView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
        #Region "Dependency properties"
        Public Shared ReadOnly DateBorderMarginProperty As DependencyProperty = DependencyProperty.Register("DateBorderMargin", GetType(Thickness), GetType(SalesPerformanceView), New PropertyMetadata(New Thickness()))
        Public Shared ReadOnly SalesVolumesMarginProperty As DependencyProperty = DependencyProperty.Register("SalesVolumesMargin", GetType(Thickness), GetType(SalesPerformanceView), New PropertyMetadata(New Thickness()))
        Public Shared ReadOnly AreaAndSalesVolumesBrushProperty As DependencyProperty = DependencyProperty.Register("AreaAndSalesVolumesBrush", GetType(SolidColorBrush), GetType(SalesPerformanceView), New PropertyMetadata(Brushes.Red))
        Public Shared ReadOnly ButtonsGridMarginProperty As DependencyProperty = DependencyProperty.Register("ButtonsGridMargin", GetType(Thickness), GetType(SalesPerformanceView), New PropertyMetadata(New Thickness(0)))
        #End Region

        Public Property DateBorderMargin() As Thickness
            Get
                Return DirectCast(GetValue(DateBorderMarginProperty), Thickness)
            End Get
            Set(ByVal value As Thickness)
                SetValue(DateBorderMarginProperty, value)
            End Set
        End Property
        Public Property SalesVolumesMargin() As Thickness
            Get
                Return DirectCast(GetValue(SalesVolumesMarginProperty), Thickness)
            End Get
            Set(ByVal value As Thickness)
                SetValue(DateBorderMarginProperty, value)
            End Set
        End Property
        Public Property AreaAndSalesVolumesBrush() As SolidColorBrush
            Get
                Return DirectCast(GetValue(AreaAndSalesVolumesBrushProperty), SolidColorBrush)
            End Get
            Set(ByVal value As SolidColorBrush)
                SetValue(AreaAndSalesVolumesBrushProperty, value)
            End Set
        End Property
        Public Property ButtonsGridMargin() As Thickness
            Get
                Return DirectCast(GetValue(ButtonsGridMarginProperty), Thickness)
            End Get
            Set(ByVal value As Thickness)
                SetValue(ButtonsGridMarginProperty, value)
            End Set
        End Property
    End Class
End Namespace
