Imports System
Imports System.Windows
Imports System.Xml.Linq
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts.RangeControlClient
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo
    Partial Public Class ChartClientForRangeControlModule
        Inherits EditorsDemoModule

        Private Const dataCount As Integer = 100
        Public Property ChartClientModel() As ChartClientModel

        Public Sub New()
            InitializeComponent()
            lbGridAlignment.SelectedIndex = 0
            ChartClientModel = New ChartClientModel()
            ChartClientModel.NumericItemsSource = GenerateNumericData()
            ChartClientModel.DateTimeItemsSource = GenerateDateTimeData()
            Me.DataContext = Me
        End Sub
        Private Function GenerateNumericData() As Double()
            Dim data(dataCount - 1) As Double
            Dim rand As New Random()
            Dim value As Double = 0
            For i As Integer = 0 To dataCount - 1
                value += (rand.NextDouble() - 0.5)
                data(i) = value
            Next i
            Return data
        End Function
        Private Function GenerateDateTimeData() As List(Of DateTimeItem)
            Dim data As New List(Of DateTimeItem)()
            Dim now As Date = Date.Now.Date
            Dim rand As New Random()
            Dim value As Double = 0
            For i As Integer = 0 To dataCount - 1
                now = now.AddDays(1)
                value += (rand.NextDouble() - 0.5)
                data.Add(New DateTimeItem() With {.Argument = now, .Value = value + Math.Sin(i * 0.6)})
            Next i
            Return data
        End Function
    End Class
    Public Class ChartClientModel
        Inherits FrameworkElement

        Public Shared ReadOnly NumericItemsSourceProperty As DependencyProperty = DependencyProperty.Register("NumericItemsSource", GetType(Object), GetType(ChartClientModel), New PropertyMetadata(Nothing))
        Public Shared ReadOnly DateTimeItemsSourceProperty As DependencyProperty = DependencyProperty.Register("DateTimeItemsSource", GetType(Object), GetType(ChartClientModel), New PropertyMetadata(Nothing))
        Public Shared ReadOnly MinimumGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MinimumGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(1.0))
        Public Shared ReadOnly MiddleGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MiddleGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(3.0))
        Public Shared ReadOnly MaximumGridSpacingProperty As DependencyProperty = DependencyProperty.Register("MaximumGridSpacing", GetType(Double), GetType(ChartClientModel), New PropertyMetadata(5.0))
        Public Shared ReadOnly DateTimeGridAlignmentProperty As DependencyProperty
        Public Shared ReadOnly GridSpacingVisibilityProperty As DependencyProperty = DependencyProperty.Register("GridSpacingVisibility", GetType(Visibility), GetType(ChartClientModel), New PropertyMetadata(Visibility.Collapsed))

        Protected Shared Sub DateTimeGridAlignmentChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim model As ChartClientModel = TryCast(d, ChartClientModel)
            If model IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
                Dim gridAlignment As DateTimeGridAlignment = CType(DirectCast(e.NewValue, ListBoxEditItem).Tag, DateTimeGridAlignment)
                model.GridSpacingVisibility = If(gridAlignment.Equals(DateTimeGridAlignment.Auto), Visibility.Collapsed, Visibility.Visible)
                model.UpdateGridSpacing(gridAlignment)
            End If
        End Sub

        Shared Sub New()
            DateTimeGridAlignmentProperty = DependencyProperty.Register("DateTimeGridAlignment", GetType(Object), GetType(ChartClientModel), New PropertyMetadata(DateTimeGridAlignment.Day, AddressOf DateTimeGridAlignmentChanged))
        End Sub

        Public Property MinimumGridSpacing() As Double
            Get
                Return DirectCast(GetValue(MinimumGridSpacingProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(MinimumGridSpacingProperty, value)
            End Set
        End Property
        Public Property MiddleGridSpacing() As Double
            Get
                Return DirectCast(GetValue(MiddleGridSpacingProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(MiddleGridSpacingProperty, value)
            End Set
        End Property
        Public Property MaximumGridSpacing() As Double
            Get
                Return DirectCast(GetValue(MaximumGridSpacingProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(MaximumGridSpacingProperty, value)
            End Set
        End Property
        Public Property NumericItemsSource() As Object
            Get
                Return GetValue(NumericItemsSourceProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(NumericItemsSourceProperty, value)
            End Set
        End Property
        Public Property DateTimeItemsSource() As Object
            Get
                Return GetValue(DateTimeItemsSourceProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(DateTimeItemsSourceProperty, value)
            End Set
        End Property
        Public Property GridSpacingVisibility() As Visibility
            Get
                Return DirectCast(GetValue(GridSpacingVisibilityProperty), Visibility)
            End Get
            Set(ByVal value As Visibility)
                SetValue(GridSpacingVisibilityProperty, value)
            End Set
        End Property

        Private Sub UpdateGridSpacing(ByVal gridAlignment As DateTimeGridAlignment)
            MinimumGridSpacing = GetMinimumGridSpacing(gridAlignment)
            MaximumGridSpacing = GetMaximumGridSpacing(gridAlignment)
            MiddleGridSpacing = (MinimumGridSpacing + MaximumGridSpacing) / 2
        End Sub
        Private Function GetMaximumGridSpacing(ByVal gridAlignment As DateTimeGridAlignment) As Double
            Select Case gridAlignment
                Case DateTimeGridAlignment.Day
                    Return 15
                Case DateTimeGridAlignment.Month
                    Return 3
                Case DateTimeGridAlignment.Week
                    Return 6
                Case Else
                    Return 5
            End Select
        End Function
        Private Function GetMinimumGridSpacing(ByVal gridAlignment As DateTimeGridAlignment) As Double
            Select Case gridAlignment
                Case DateTimeGridAlignment.Day
                    Return 5
                Case DateTimeGridAlignment.Month
                    Return 1
                Case DateTimeGridAlignment.Week
                    Return 2
                Case Else
                    Return 1
            End Select
        End Function
    End Class
    Public Class DateTimeItem
        Public Property Argument() As Object
        Public Property Value() As Object
    End Class
    Public Enum ChartViewType
        Area
        Bar
        Line
    End Enum
    Public Class ChartViewTypeConverter
        Implements IValueConverter

        Private Function Parse(ByVal type As String) As RangeControlClientView
            If type = "Area" Then
                Return New RangeControlClientAreaView()
            End If
            If type = "Bar" Then
                Return New RangeControlClientBarView()
            End If
            If type = "Line" Then
                Return New RangeControlClientLineView()
            End If
            Return Nothing
        End Function
        Private Function Parse(ByVal type As ChartViewType) As RangeControlClientView
            If type = ChartViewType.Area Then
                Return New RangeControlClientAreaView()
            End If
            If type = ChartViewType.Bar Then
                Return New RangeControlClientBarView()
            End If
            If type = ChartViewType.Line Then
                Return New RangeControlClientLineView()
            End If
            Return Nothing
        End Function
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is String Then
                Return Parse(TryCast(value, String))
            End If
            If TypeOf value Is ChartViewType Then
                Return Parse(DirectCast(value, ChartViewType))
            End If
            Return Nothing
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf parameter Is RangeControlClientAreaView Then
                Return ChartViewType.Area
            End If
            If TypeOf parameter Is RangeControlClientBarView Then
                Return ChartViewType.Bar
            End If
            If TypeOf parameter Is RangeControlClientLineView Then
                Return ChartViewType.Line
            End If
            Return String.Empty
        End Function
    End Class
End Namespace
