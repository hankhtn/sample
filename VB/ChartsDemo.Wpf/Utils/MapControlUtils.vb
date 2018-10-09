Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Map
Imports System
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media

Namespace ChartsDemo
    Public Class SelectedItemNameBindingBehavior
        Inherits Behavior(Of VectorLayer)

        Public Shared ReadOnly SelectedItemNameProperty As DependencyProperty = DependencyProperty.Register("SelectedItemName", GetType(String), GetType(SelectedItemNameBindingBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, SelectedItemNameBindingBehavior).OnSelectedItemNameChanged()))

        Public Property SelectedItemName() As String
            Get
                Return CStr(GetValue(SelectedItemNameProperty))
            End Get
            Set(ByVal value As String)
                SetValue(SelectedItemNameProperty, value)
            End Set
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.Map.SelectionChanged, AddressOf MapSelectionChanged
            AddHandler AssociatedObject.DataLoaded, Sub(_d, _e) OnSelectedItemNameChanged()
        End Sub

        Private Shared Function GetName(ByVal mapItem As MapItem) As String
            Return If(mapItem IsNot Nothing AndAlso mapItem.Attributes("NAME") IsNot Nothing, CStr(mapItem.Attributes("NAME").Value), String.Empty)
        End Function
        Private Sub MapSelectionChanged(ByVal sender As Object, ByVal e As MapItemSelectionChangedEventArgs)
            SelectedItemName = GetName(CType(e.Selection.FirstOrDefault(), MapItem))
        End Sub
        Private Sub OnSelectedItemNameChanged()
            If AssociatedObject IsNot Nothing AndAlso AssociatedObject.Data IsNot Nothing AndAlso AssociatedObject.Data.DisplayItems.Any() Then
                AssociatedObject.SelectedItem = AssociatedObject.Data.DisplayItems.FirstOrDefault(Function(x) GetName(x) = SelectedItemName)
            End If
        End Sub
    End Class

    Public Class ChartPaletteToMapColorsConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim chartColors As Palette = DirectCast(value, Palette)
            Dim rangeStops As DoubleCollection = DirectCast(parameter, DoubleCollection)
            Dim colorsCount As Integer = CInt((rangeStops(rangeStops.Count - 1) - rangeStops(0))) + 1
            Dim mapColors As New DevExpress.Xpf.Map.ColorCollection()
            For i As Integer = 0 To colorsCount - 1
                mapColors.Add(chartColors(i))
            Next i
            Return mapColors
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
End Namespace
