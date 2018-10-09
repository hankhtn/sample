Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf
Imports DevExpress.Xpf.Core
Namespace BarsDemo
    Public Interface IShowAboutDialogService
        Sub Show()
    End Interface
    Public Class ShowAboutDialogService
        Inherits ServiceBase
        Implements IShowAboutDialogService

        Public Sub Show() Implements IShowAboutDialogService.Show
            Dim platformName As String = "WPF"
            Dim ati As New AboutToolInfo()
            ati.LicenseState = LicenseState.Licensed
            ati.ProductDescriptionLine1 = "A demo application that demonstrates the Toolbar and Menu system providing commands for a simple text editor."
            ati.ProductDescriptionLine2 = "In this demo, the DevExpress Toolbar and Menu library is used to implement text editing commands in a simple text editor."
            ati.ProductDescriptionLine3 = "Practice using bar commands to control the appearance of the editor's text. To learn more about DevExpress visit:"
            ati.ProductEULA = "DevExpress"
            ati.ProductEULAUri = "http://www.devexpress.com/"
            ati.ProductName = "DXBars for " & platformName
            ati.Version = AssemblyInfo.MarketingVersion

            Dim tAbout As DevExpress.Xpf.ToolAbout = New ToolAbout(ati)
            Dim aWindow As New AboutWindow()
            aWindow.Content = tAbout
            aWindow.ShowDialog()
        End Sub
    End Class
End Namespace
