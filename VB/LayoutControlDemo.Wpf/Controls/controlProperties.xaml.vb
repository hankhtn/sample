Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo
    Partial Public Class controlProperties
        Inherits UserControl

        #Region "Dependency Properties"

        Public Shared ReadOnly LayoutControlProperty As DependencyProperty = DependencyProperty.Register("LayoutControl", GetType(LayoutControlBase), GetType(controlProperties), New PropertyMetadata(New PropertyChangedCallback(AddressOf OnLayoutControlChanged)))
        Public Shared ReadOnly LayoutControlPropertiesProperty As DependencyProperty = DependencyProperty.Register("LayoutControlProperties", GetType(FrameworkElement), GetType(controlProperties), Nothing)
        Public Shared ReadOnly ItemPropertiesProperty As DependencyProperty = DependencyProperty.Register("ItemProperties", GetType(FrameworkElement), GetType(controlProperties), Nothing)
        Public Shared ReadOnly ItemPropertiesHeaderTemplateProperty As DependencyProperty = DependencyProperty.Register("ItemPropertiesHeaderTemplate", GetType(DataTemplate), GetType(controlProperties), Nothing)
        Public Shared ReadOnly SelectedItemProperty As DependencyProperty = DependencyProperty.Register("SelectedItem", GetType(Object), GetType(controlProperties), New PropertyMetadata(New PropertyChangedCallback(AddressOf OnSelectedItemChanged)))

        Private Shared Sub OnLayoutControlChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, controlProperties).OnLayoutControlChanged()
        End Sub
        Private Shared Sub OnSelectedItemChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, controlProperties).OnSelectedItemChanged(e.OldValue, e.NewValue)
        End Sub

        #End Region ' Dependency Properties

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Property LayoutControl() As LayoutControlBase
            Get
                Return DirectCast(GetValue(LayoutControlProperty), LayoutControlBase)
            End Get
            Set(ByVal value As LayoutControlBase)
                SetValue(LayoutControlProperty, value)
            End Set
        End Property
        Public Property LayoutControlProperties() As FrameworkElement
            Get
                Return DirectCast(GetValue(LayoutControlPropertiesProperty), FrameworkElement)
            End Get
            Set(ByVal value As FrameworkElement)
                SetValue(LayoutControlPropertiesProperty, value)
            End Set
        End Property
        Public Property ItemProperties() As FrameworkElement
            Get
                Return DirectCast(GetValue(ItemPropertiesProperty), FrameworkElement)
            End Get
            Set(ByVal value As FrameworkElement)
                SetValue(ItemPropertiesProperty, value)
            End Set
        End Property
        Public Property ItemPropertiesHeaderTemplate() As DataTemplate
            Get
                Return DirectCast(GetValue(ItemPropertiesHeaderTemplateProperty), DataTemplate)
            End Get
            Set(ByVal value As DataTemplate)
                SetValue(ItemPropertiesHeaderTemplateProperty, value)
            End Set
        End Property
        Public Property SelectedItem() As Object
            Get
                Return DirectCast(GetValue(SelectedItemProperty), Object)
            End Get
            Set(ByVal value As Object)
                SetValue(SelectedItemProperty, value)
            End Set
        End Property

        Private Function GetSelectedTileBackgroundMask() As Brush
            Dim gradientStops = New GradientStopCollection()
            gradientStops.Add(New GradientStop With {.Offset = 0, .Color = Color.FromArgb(70, 0, 0, 0)})
            gradientStops.Add(New GradientStop With {.Offset = 0.4, .Color = Color.FromArgb(0, 0, 0, 0)})
            gradientStops.Add(New GradientStop With {.Offset = 0.6, .Color = Color.FromArgb(0, 0, 0, 0)})
            gradientStops.Add(New GradientStop With {.Offset = 1, .Color = Color.FromArgb(70, 0, 0, 0)})
            Return New LinearGradientBrush(gradientStops, 0)
        End Function
        Private Sub OnInitLayoutControl(ByVal layoutControl As LayoutControlBase)
            For Each child As FrameworkElement In layoutControl.GetLogicalChildren(False)
                If TypeOf child Is SampleLayoutItem Then
                    AddHandler CType(child, SampleLayoutItem).IsSelectedChanged, AddressOf OnItemIsSelectedChanged
                Else
                    If TypeOf child Is Tile Then
                        AddHandler child.MouseLeftButtonDown, AddressOf OnTileMouseLeftButtonDown
                    Else
                        If TypeOf child Is LayoutControlBase Then
                            OnInitLayoutControl(CType(child, LayoutControlBase))
                        End If
                    End If
                End If
            Next child
        End Sub
        Private Sub OnItemIsSelectedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim layoutItem = DirectCast(sender, SampleLayoutItem)
            If layoutItem.IsSelected Then
                SelectedItem = layoutItem
            End If
        End Sub
        Private Sub OnLayoutControlChanged()
            OnInitLayoutControl(LayoutControl)
        End Sub
        Private Sub OnSelectedItemChanged(ByVal oldValue As Object, ByVal newVaue As Object)
            If TypeOf oldValue Is SampleLayoutItem Then
                DirectCast(oldValue, SampleLayoutItem).IsSelected = False
            End If
            If TypeOf oldValue Is Tile Then
                Dim tile = DirectCast(oldValue, Tile)
                tile.SetValue(Tile.CalculatedBackgroundProperty, SelectedTileCalculatedBackground)
                SelectedTileCalculatedBackground = Nothing
                RemoveHandler tile.Loaded, AddressOf OnSelectedTileLoaded
            End If
            If TypeOf SelectedItem Is Tile Then
                Dim tile = DirectCast(SelectedItem, Tile)
                SelectedTileCalculatedBackground = tile.CalculatedBackground
                UpdateSelectedTileBackgroundMask()
                AddHandler tile.Loaded, AddressOf OnSelectedTileLoaded
            End If
        End Sub
        Private Sub OnSelectedTileLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not LayoutControl.Controller.IsDragAndDrop Then
                UpdateSelectedTileBackgroundMask()
            End If
        End Sub
        Private Sub OnTileMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            SelectedItem = sender
        End Sub
        Private Sub UpdateSelectedTileBackgroundMask()
            DirectCast(SelectedItem, Tile).SetValue(Tile.CalculatedBackgroundProperty, GetSelectedTileBackgroundMask())
        End Sub

        Private Property SelectedTileCalculatedBackground() As Brush
    End Class

    Public Class ObjectToStringConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(String.IsNullOrEmpty(DirectCast(value, String)), Nothing, value)
        End Function
    End Class

    Public Class ObjectToTypeNameConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing Then
                Return value.GetType().Name
            Else
                Return Nothing
            End If
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ItemToHeaderConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing Then
                If TypeOf value Is SampleLayoutItem Then
                    Return DirectCast(value, SampleLayoutItem).Caption
                Else
                    If TypeOf value Is Tile Then
                        Return TryCast(DirectCast(value, Tile).Header, String)
                    Else
                        Return value.GetType().Name
                    End If
                End If
            Else
                Return Nothing
            End If
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
