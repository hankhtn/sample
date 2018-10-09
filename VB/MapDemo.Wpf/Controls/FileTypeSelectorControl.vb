Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Map
Imports Microsoft.Win32
Imports System.Windows
Imports System.Windows.Data

Namespace MapDemo
    Public Class ShapesExporterControl
        Inherits VisibleControl

        Private ReadOnly viewModel As ShapesExporterViewModel

        Public Shared ReadOnly ExportingLayerProperty As DependencyProperty = DependencyProperty.Register("ExportingLayer", GetType(VectorLayer), GetType(ShapesExporterControl), New PropertyMetadata(Nothing))
        Public Shared ReadOnly PressedProperty As DependencyProperty = DependencyProperty.Register("Pressed", GetType(Boolean), GetType(ShapesExporterControl), New PropertyMetadata(AddressOf OnButtonPressed))

        Private Shared Sub OnButtonPressed(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim control As ShapesExporterControl = TryCast(d, ShapesExporterControl)
            If control Is Nothing OrElse (Not control.viewModel.Pressed) Then
                Return
            End If
            Dim fileType As String = DirectCast(control.viewModel.SelectedFileType, String)
            Dim format As String = GetFileFormat(fileType)
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.FileName = String.Empty
            saveFileDialog.Filter = String.Format("{0} files (*.{1})|*.{1}", format.ToUpper(), format)
            Dim dialogResult? As Boolean = saveFileDialog.ShowDialog()
            If (Not dialogResult.HasValue) OrElse (Not dialogResult.Value) Then
                Return
            End If
            Select Case fileType
                Case ".shp-file"
                    control.ExportingLayer.ExportToShp(saveFileDialog.FileName, New ShpExportOptions() With {.ExportToDbf = True, .ShapeType = ShapeType.Polygon})
                Case ".kml-file"
                    control.ExportingLayer.ExportToKml(saveFileDialog.FileName)
                Case ".svg-file"
                    control.ExportingLayer.ExportToSvg(saveFileDialog.FileName)
            End Select
            DXMessageBox.Show(String.Format("Shapes succesfully exported to {0} file", saveFileDialog.FileName), "Info", MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub
        Private Shared Function GetFileFormat(ByVal fileType As String) As String
            Select Case fileType
                Case ".shp-file"
                    Return "shp"
                Case ".kml-file"
                    Return "kml"
                Case ".svg-file"
                    Return "svg"
            End Select
            Return String.Empty
        End Function

        Public Property ExportingLayer() As VectorLayer
            Get
                Return DirectCast(GetValue(ExportingLayerProperty), VectorLayer)
            End Get
            Set(ByVal value As VectorLayer)
                SetValue(ExportingLayerProperty, value)
            End Set
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(ShapesExporterControl)
            Me.viewModel = New ShapesExporterViewModel()
            DataContext = viewModel
            BindingOperations.SetBinding(Me, PressedProperty, New Binding("Pressed"))
        End Sub
    End Class

    Public Class ShapesExporterViewModel
        Inherits DependencyObject

        Private Const DefaultFileType As String = ".shp-file"

        Public Shared ReadOnly PressedProperty As DependencyProperty = DependencyProperty.Register("Pressed", GetType(Boolean), GetType(ShapesExporterViewModel), New PropertyMetadata(False))
        Public Shared ReadOnly PopupOpenedProperty As DependencyProperty = DependencyProperty.Register("PopupOpened", GetType(Boolean), GetType(ShapesExporterViewModel), New PropertyMetadata(False))
        Public Shared ReadOnly SelectedFileTypeProperty As DependencyProperty = DependencyProperty.Register("SelectedFileType", GetType(Object), GetType(ShapesExporterViewModel), New PropertyMetadata(DefaultFileType, Nothing, AddressOf CoerceFileTypeChanged))

        Private Shared Function CoerceFileTypeChanged(ByVal d As DependencyObject, ByVal baseValue As Object) As Object
            Dim viewmodel As ShapesExporterViewModel = TryCast(d, ShapesExporterViewModel)
            If viewmodel Is Nothing Then
                Return DefaultFileType
            End If
            Dim item As ListBoxEditItem = TryCast(baseValue, ListBoxEditItem)
            Return If(item Is Nothing, viewmodel.SelectedFileType, item.Content)
        End Function

        Public Property Pressed() As Boolean
            Get
                Return DirectCast(GetValue(PressedProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(PressedProperty, value)
            End Set
        End Property
        Public Property PopupOpened() As Boolean
            Get
                Return DirectCast(GetValue(PopupOpenedProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(PopupOpenedProperty, value)
            End Set
        End Property
        Public Property SelectedFileType() As Object
            Get
                Return DirectCast(GetValue(SelectedFileTypeProperty), Object)
            End Get
            Set(ByVal value As Object)
                SetValue(SelectedFileTypeProperty, value)
            End Set
        End Property
    End Class
End Namespace
