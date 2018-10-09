Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.PropertyGrid
Imports DevExpress.Mvvm.Native

Namespace PropertyGridDemo



    Partial Public Class CollectionEditingModule
        Inherits PropertyGridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim itemInitializer = CType(Resources("itemInitializer"), VisualizerItemInitializer)
        End Sub
        Private Sub OnVisualizerPanelLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.ItemsSource = New List(Of Object) From {root}
        End Sub

        Private Sub OnVisualizerPanelChanged(ByVal sender As Object, ByVal e As EventArgs)
            If grid IsNot Nothing Then
                grid.RefreshData()
            End If
        End Sub

        Private Sub pGrid_CustomExpand(ByVal sender As Object, ByVal args As CustomExpandEventArgs)
            If args.IsInitializing Then
                args.Row.Children.FirstOrDefault()
            End If
        End Sub
    End Class

    Public Class VisualItemsSelector
        Implements IChildNodesSelector

        Private ReadOnly initializer As New VisualizerItemInitializer()
        Private Function IChildNodesSelector_SelectChildren(ByVal item As Object) As IEnumerable Implements IChildNodesSelector.SelectChildren
            Dim list As New List(Of FrameworkElement)()
            Dim fe As FrameworkElement = DirectCast(item, FrameworkElement)
            If DirectCast(initializer, IInstanceInitializer).Types.Any(Function(typeInfo) item.GetType() Is typeInfo.Type) Then
                Return Nothing
            End If
            For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(fe) - 1
                list.Add(CType(VisualTreeHelper.GetChild(fe, i), FrameworkElement))
            Next i
            Return list
        End Function
    End Class
    Public Class ItemsSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return New List(Of Object) From {value}
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class GetNameConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value IsNot Nothing, value.GetType().Name, String.Empty)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class VisualizerItemInitializer
        Implements IInstanceInitializer

        Public Sub New()
        End Sub
        Private Function IInstanceInitializer_CreateInstance(ByVal type As TypeInfo) As Object Implements IInstanceInitializer.CreateInstance
            Dim element As FrameworkElement = Nothing
            If type.Type Is GetType(Button) Then
                element = New Button() With {.Content = type.Name}
            End If
            If type.Type Is GetType(CheckBox) Then
                element = New CheckBox() With {.Content = type.Name}
            End If
            If type.Type Is GetType(TextBox) Then
                element = New TextBox() With {.Text = type.Name}
            End If
            element.HorizontalAlignment = HorizontalAlignment.Center
            element.Margin = New Thickness(5)
            Return element
        End Function
        Private ReadOnly Property IInstanceInitializer_Types() As IEnumerable(Of TypeInfo) Implements IInstanceInitializer.Types
            Get
                Dim result = New List(Of TypeInfo)()
                result.Add(New TypeInfo(GetType(Button), "New Button"))
                result.Add(New TypeInfo(GetType(TextBox), "New Text Editor"))
                result.Add(New TypeInfo(GetType(CheckBox), "New CheckBox"))
                Return result
            End Get
        End Property
    End Class
End Namespace
