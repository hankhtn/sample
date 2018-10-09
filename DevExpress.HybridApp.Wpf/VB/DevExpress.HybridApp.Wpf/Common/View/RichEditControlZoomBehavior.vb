Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.RichEdit
Imports System.Collections
Imports System.IO
Imports DevExpress.XtraRichEdit
Imports System.Windows.Input
Imports DevExpress.Mvvm

Namespace DevExpress.DevAV
    Public Class RichEditControlZoomBehavior
        Inherits Behavior(Of RichEditControl)

        Private Shared minZoomFactor As Single = 0.3F
        Private Shared maxZoomFactor As Single = 1.7F
        Private Shared stepZoomFactor As Single = 0.1F

        Private privateZoomInCommand As ICommand
        Public Property ZoomInCommand() As ICommand
            Get
                Return privateZoomInCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateZoomInCommand = value
            End Set
        End Property
        Private privateZoomOutCommand As ICommand
        Public Property ZoomOutCommand() As ICommand
            Get
                Return privateZoomOutCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateZoomOutCommand = value
            End Set
        End Property

        Public Sub New()
            ZoomInCommand = New DelegateCommand(Sub() AssociatedObject.ActiveView.ZoomFactor += stepZoomFactor, Function() AssociatedObject IsNot Nothing AndAlso AssociatedObject.ActiveView IsNot Nothing AndAlso AssociatedObject.ActiveView.ZoomFactor + stepZoomFactor < maxZoomFactor)
            ZoomOutCommand = New DelegateCommand(Sub() AssociatedObject.ActiveView.ZoomFactor -= stepZoomFactor, Function() AssociatedObject IsNot Nothing AndAlso AssociatedObject.ActiveView IsNot Nothing AndAlso AssociatedObject.ActiveView.ZoomFactor - stepZoomFactor > minZoomFactor)
        End Sub










    End Class
End Namespace
