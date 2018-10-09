Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Editors
Imports System.Windows.Media.Animation

Namespace EditorsDemo



    Partial Public Class Safe
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            DigitTemplatesHelper.SetTemplates(DirectCast(Resources("digits"), ObjectList))
            firstDigit.EditValue = 0R
            secondDigit.EditValue = 0R
            thirdDigit.EditValue = 0R
        End Sub
        Private Sub trackBarButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button As Button = TryCast(sender, Button)
            Dim trackBar As TrackBarEdit = TryCast((button.GetValue(BaseEdit.OwnerEditProperty)), TrackBarEdit)
            StartAnimation(Convert.ToDouble(button.Tag), trackBar)
        End Sub
        Private Sub StartAnimation(ByVal value As Double, ByVal trackBar As TrackBarEdit)
            Dim animation As New DoubleAnimation() With {.From = trackBar.Value, .To = value, .Duration = New Duration(New TimeSpan(0, 0, 0, 0, 500)), .AccelerationRatio = 0.5, .DecelerationRatio = 0.5, .FillBehavior = FillBehavior.HoldEnd}
            Dim storyboard As New Storyboard()
            storyboard.SetValue(System.Windows.Media.Animation.Storyboard.TargetProperty, trackBar)
            storyboard.SetValue(System.Windows.Media.Animation.Storyboard.TargetPropertyProperty, New PropertyPath("Value"))
            storyboard.Children.Add(animation)
            storyboard.Begin(Me, True)
        End Sub

    End Class
    Public Class ToDigitConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            If TryCast(value, String) Is Nothing Then
                Return DigitTemplatesHelper.GetTemplate(CInt((DirectCast(value, Double))))
            Else
                Return DigitTemplatesHelper.GetTemplate(CInt((Convert.ToDouble(DirectCast(value, String)))))
            End If
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
    Public NotInheritable Class DigitTemplatesHelper

        Private Sub New()
        End Sub
        Private Shared templates As WeakReference

        Public Shared Function GetTemplate(ByVal digit As Integer) As ControlTemplate
            Return CType(DirectCast(DigitTemplatesHelper.templates.Target, ObjectList)(digit), ControlTemplate)
        End Function

        Public Shared Sub SetTemplates(ByVal templates As ObjectList)
            DigitTemplatesHelper.templates = New WeakReference(templates)
        End Sub
    End Class
End Namespace
