Imports DevExpress.Office
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.Commands
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo
    Public Class CustomsRichEditCommandFactoryService
        Implements IRichEditCommandFactoryService

        Private _service As IRichEditCommandFactoryService
        Private _control As RichEditControl
        Private _searchTextBox As ButtonEdit

        Public Sub New(ByVal richEditControl As RichEditControl, ByVal richEditCommandFactoryService As IRichEditCommandFactoryService, ByVal searchTextBox As ButtonEdit)
            Me._control = richEditControl
            Me._service = richEditCommandFactoryService
            Me._searchTextBox = searchTextBox
        End Sub

        Private Function IRichEditCommandFactoryService_CreateCommand(ByVal id As RichEditCommandId) As RichEditCommand Implements IRichEditCommandFactoryService.CreateCommand
            If id.Equals(RichEditCommandId.Find) Then
                Return New CustomFindCommand(Me._control, Me._searchTextBox)
            End If
            Return Me._service.CreateCommand(id)
        End Function
    End Class
    Public Class CustomFindCommand
        Inherits FindCommand

        Private _searchTextBox As ButtonEdit
        Private _separators() As Char = { Characters.ParagraphMark, Characters.PageBreak, Characters.TabMark }

        Public Sub New(ByVal richEditControl As IRichEditControl, ByVal searchTextBox As ButtonEdit)
            MyBase.New(richEditControl)
            Me._searchTextBox = searchTextBox
        End Sub

        Protected Overrides Sub ShowForm(ByVal searchString As String)
            Me._searchTextBox.Focus()
            Dim selectedText As String = Control.Document.GetText(Control.Document.Selection)
            If String.IsNullOrEmpty(selectedText) OrElse selectedText.IndexOfAny(Me._separators) >= 0 Then
                Return
            End If
            Me._searchTextBox.Text = selectedText.Trim()
        End Sub
    End Class
End Namespace
