Imports System
Imports System.Windows.Data
Imports System.Windows
Imports DevExpress.Xpf.DemoBase

Namespace DockingDemo
    <CodeFile("ViewModels/MDIMenuMergingViewModel.(cs)")>
    Partial Public Class MDIMenuMerging
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides Sub Hide()
            mainRibbon.CloseApplicationMenu()
            MyBase.Hide()
        End Sub
    End Class
    Public Class PointToStringConverter
        Implements IValueConverter

        Public Property FormatString() As String
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is Point) Then
                Return Nothing
            End If
            Dim p As Point = DirectCast(value, Point)
            Return String.Format(If(FormatString, "{0},{1}"),If(p.X <> -1, Math.Round(p.X).ToString(), ""),If(p.X <> -1, Math.Round(p.Y).ToString(), ""))
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
