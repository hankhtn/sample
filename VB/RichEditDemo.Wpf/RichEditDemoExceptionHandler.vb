Imports System
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit

Namespace RichEditDemo
    Public Class RichEditDemoExceptionsHandler
        Private ReadOnly control As RichEditControl
        Public Sub New(ByVal control As RichEditControl)
            Me.control = control
        End Sub
        Public Sub Install()
            If control IsNot Nothing Then
                AddHandler control.UnhandledException, AddressOf OnRichEditControlUnhandledException
            End If
        End Sub

        Private Sub OnRichEditControlUnhandledException(ByVal sender As Object, ByVal e As RichEditUnhandledExceptionEventArgs)
            Try
                If e.Exception IsNot Nothing Then
                    Throw e.Exception
                End If
            Catch ex As RichEditUnsupportedFormatException
                ShowMessage(ex.Message)
                e.Handled = True
            Catch ex As System.Runtime.InteropServices.ExternalException
                ShowMessage(ex.Message)
                e.Handled = True
            Catch ex As System.IO.IOException
                ShowMessage(ex.Message)
                e.Handled = True
            Catch ex As System.InvalidOperationException
                If ex.Message = DevExpress.XtraRichEdit.Localization.XtraRichEditLocalizer.GetString(DevExpress.XtraRichEdit.Localization.XtraRichEditStringId.Msg_MagicNumberNotFound) OrElse ex.Message = DevExpress.XtraRichEdit.Localization.XtraRichEditLocalizer.GetString(DevExpress.XtraRichEdit.Localization.XtraRichEditStringId.Msg_UnsupportedDocVersion) Then
                    ShowMessage(ex.Message)
                    e.Handled = True
                Else
                    Throw ex
                End If
            Catch ex As SystemException
                ShowMessage(ex.Message)
                e.Handled = True
            End Try
        End Sub
        Private Sub ShowMessage(ByVal message As String)
            DXMessageBox.Show(message, System.Windows.Forms.Application.ProductName, MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub
    End Class
End Namespace
