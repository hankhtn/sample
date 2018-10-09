Imports DevExpress.Mvvm.UI
Imports System.Windows
Imports System.Windows.Documents
Namespace RibbonDemo
    Public Interface IInlineImageService
        Sub InsertImage(ByVal imageViewModel As InlineImageViewModel)
        Function GetViewModelFromContainer() As InlineImageViewModel
    End Interface
    Public Class RibbonSimplePadImageService
        Inherits ServiceBase
        Implements IInlineImageService

        Protected ReadOnly Property RichControl() As DemoRichControl
            Get
                Return TryCast(AssociatedObject, DemoRichControl)
            End Get
        End Property
        Public Sub InsertImage(ByVal imageViewModel As InlineImageViewModel) Implements IInlineImageService.InsertImage
            RichControl.InsertContainer(New InlineUIContainer() With {.Child = New InlineImage(imageViewModel)})
        End Sub
        Public Function GetViewModelFromContainer() As InlineImageViewModel Implements IInlineImageService.GetViewModelFromContainer
            If RichControl.Container IsNot Nothing Then
                Return CType(CType(RichControl.Container, InlineUIContainer).Child, InlineImage).InlineImageViewModel
            End If
            Return Nothing
        End Function
    End Class
End Namespace
