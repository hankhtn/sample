Imports System
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Printing

Namespace DevExpress.DevAV.ViewModels
    Public Class PrintableControlPreviewViewModel
        Implements IDocumentContent

        Public Shared Function Create(ByVal link As PrintableControlLink) As PrintableControlPreviewViewModel
            Return ViewModelSource.Create(Function() New PrintableControlPreviewViewModel(link))
        End Function

        Protected Sub New(ByVal link As PrintableControlLink)
            Me.Link = link
        End Sub
        Public Overridable Property Link() As PrintableControlLink

        Public Sub Close()
            If DocumentOwner IsNot Nothing Then
                DocumentOwner.Close(Me)
            End If
        End Sub
        Private privateDocumentOwner As IDocumentOwner
        Protected Property DocumentOwner() As IDocumentOwner
            Get
                Return privateDocumentOwner
            End Get
            Private Set(ByVal value As IDocumentOwner)
                privateDocumentOwner = value
            End Set
        End Property
        #Region "IDocumentContent"
        Private Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
        End Sub
        Private Sub IDocumentContent_OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub
        Private Property IDocumentContent_DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner
            Get
                Return DocumentOwner
            End Get
            Set(ByVal value As IDocumentOwner)
                DocumentOwner = value
            End Set
        End Property
        Private ReadOnly Property IDocumentContent_Title() As Object Implements IDocumentContent.Title
            Get
                Return Nothing
            End Get
        End Property
        #End Region
    End Class
End Namespace
