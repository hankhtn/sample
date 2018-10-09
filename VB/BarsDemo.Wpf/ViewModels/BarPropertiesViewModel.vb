Imports System.Diagnostics

Namespace BarsDemo.ViewModels
    Public Class BarPropertiesViewModel
        Public Overridable ReadOnly Property ShowAboutDialogService() As IShowAboutDialogService
            Get
                Return Nothing
            End Get
        End Property

        Public Sub ShowAboutDialog()
            ShowAboutDialogService.Show()
        End Sub
        Public Sub NavigateToHomePage()
            Process.Start("http://www.devexpress.com")
        End Sub
    End Class
End Namespace
