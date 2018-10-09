Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core.Native

Namespace ControlsDemo
    <POCOViewModel(ImplementIDataErrorInfo := True)>
    Public Class NewJumpTaskWindowViewModel
        Public Sub New()
            Icons = New NamedIcon() {
                New NamedIcon() With {.Caption = "Moon", .Icon = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(GetType(NewJumpTaskWindowViewModel).Assembly, "Images/Moon.png"))},
                New NamedIcon() With {.Caption = "Sun", .Icon = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(GetType(NewJumpTaskWindowViewModel).Assembly, "Images/Sun.png"))}
            }
            CustomCategory = ""
            Title = "Title"
            Description = ""
            MessageText = "Message"
            Icon = Icons.ElementAt(0)
        End Sub
        Public Property Icons() As IEnumerable(Of NamedIcon)
        Public Overridable Property CustomCategory() As String
        <Required>
        Public Overridable Property Title() As String
        Public Overridable Property Icon() As NamedIcon
        Public Overridable Property Description() As String
        Public Overridable Property MessageText() As String
    End Class
End Namespace
