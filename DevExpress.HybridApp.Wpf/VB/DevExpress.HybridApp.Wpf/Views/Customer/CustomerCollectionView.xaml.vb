Imports System.Windows.Controls
Imports System.Windows
Imports DevExpress.DevAV
Imports System.Windows.Media
Imports System.Reflection

Namespace DevExpress.DevAV.Views
    Partial Public Class CustomerCollectionView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class SlideViewTemplateSelector
        Inherits DataTemplateSelector

        Public Property ContactsTemplate() As DataTemplate
        Public Property StoresTemplate() As DataTemplate
        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            If TypeOf item Is CustomerEmployee Then
                Return ContactsTemplate
            End If
            If TypeOf item Is CustomerStore Then
                Return StoresTemplate
            End If
            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class
End Namespace
Namespace DevExpress.DevAV.Common.View
    Public Class DpiResizingPanel
        Inherits ContentControl

        Private Const defaultDpi As Double = 96R
        Public Sub New()
            ResizeByDpi()
        End Sub
        Private Shared Function GetDpiXFactor() As Double
            Return GetDpiFactor("DpiX")
        End Function
        Private Shared Function GetDpiYFactor() As Double
            Return GetDpiFactor("Dpi")
        End Function
        Private Shared Function GetDpiFactor(ByVal propName As String) As Double
            Dim dpiProperty = GetType(SystemParameters).GetProperty(propName, BindingFlags.NonPublic Or BindingFlags.Static)
            Dim dpi = CInt((dpiProperty.GetValue(Nothing, Nothing)))
            Return dpi / defaultDpi
        End Function
        Private Shared Function CorrectDpiFactor(ByVal factor As Double) As Double
            Return If(factor > 1.5, 1.5, factor)
        End Function
        Private Sub ResizeByDpi()
            If SystemParameters.PrimaryScreenHeight > 1500 AndAlso SystemParameters.PrimaryScreenWidth > 2000 Then
                Return
            End If
            Dim dpiXFactor = CorrectDpiFactor(GetDpiXFactor())
            Dim dpiYFactor = CorrectDpiFactor(GetDpiYFactor())
            LayoutTransform = New ScaleTransform(1 / dpiXFactor, 1 / dpiYFactor)

            Dim touchScaleFactor As Single = Nothing, fontSize_Renamed As Single = Nothing
            DevExpress.DevAV.Common.Utils.DeviceDetector.SuggestHybridDemoParameters(touchScaleFactor, fontSize_Renamed)
            FontSize = 12 * dpiXFactor
        End Sub
    End Class
End Namespace
